using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;

namespace LandWind.Blog.Core.Extensions
{
    /// <summary>
    /// YAML 配置扩展
    /// </summary>
    public static class YamlConfigurationExtensions
    {
        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path)
        {
            return AddYamlFile(builder, null, path, false, false);
        }

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional)
        {
            return AddYamlFile(builder, null, path, optional, false);
        }

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            return AddYamlFile(builder, null, path, optional, reloadOnChange);
        }

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("Filep path must be a non-empty string.", nameof(path));

            return builder.AddYamlFile(option =>
            {
                option.FileProvider = provider;
                option.Path = path;
                option.Optional = optional;
                option.ReloadOnChange = reloadOnChange;
                option.ResolveFileProvider();
            });
        }

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, Action<YamlConfigurationSource> configureSource)
        {
            return builder.Add(configureSource);
        }

        public static Dictionary<string, object> ToDictionary(this IConfiguration section, params string[] sectionsToSkip)
        {
            if (sectionsToSkip == null)
                sectionsToSkip = Array.Empty<string>();

            var dic = new Dictionary<string, object>();
            foreach (var value in section.GetChildren())
            {
                if (string.IsNullOrEmpty(value.Key) || sectionsToSkip.Contains(value.Key, StringComparer.OrdinalIgnoreCase))
                    continue;

                if (value.Value != null)
                    dic[value.Key] = value.Value;

                var subDic = ToDictionary(value);
                if (subDic.Count > 0)
                    dic[value.Key] = subDic;
            }

            return dic;
        }
    }

    /// <summary>
    ///  Yaml 配置源
    /// </summary>
    public class YamlConfigurationSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new YamlConfigurationProvider(this);
        }
    }

    public class YamlConfigurationProvider : FileConfigurationProvider
    {
        public YamlConfigurationProvider(YamlConfigurationSource source) : base(source)
        {
        }

        public override void Load(Stream stream)
        {
            var parser = new YamlConfigurationFileParser();
            try
            {
                Data = parser.Parse(stream);
            }
            catch (YamlException ex)
            {
                var error = string.Empty;
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    using var streamReader = new StreamReader(stream);
                    var fileContext = ReadLines(streamReader);
                    error = RetrieveErrorContext(ex, fileContext);
                }

                throw new FormatException(
                "Could not parse the YAML file. " +
                    $"Error on line number '{ex.Start.Line}': '{error}'.", ex);
            }
        }

        private string RetrieveErrorContext(YamlException ex, IEnumerable<string> fileContext)
        {
            string possibleLineContent = fileContext.Skip(ex.Start.Line - 1).FirstOrDefault();
            return possibleLineContent ?? String.Empty;
        }

        private static IEnumerable<string> ReadLines(StreamReader streamReader)
        {
            string line;
            do
            {
                line = streamReader.ReadLine();
                yield return line;
            } while (true);
        }
    }

    internal class YamlConfigurationFileParser
    {
        private readonly Stack<string> _context = new Stack<string>();
        private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private string _currentPath;

        public IDictionary<string, string> Parse(Stream stream)
        {
            _data.Clear();

            var yamlStream = new YamlStream();
            yamlStream.Load(new StreamReader(stream));

            if (!yamlStream.Documents.Any())
                return _data;
            if (!(yamlStream.Documents[0].RootNode is YamlMappingNode mappingNode))
                return _data;

            foreach (var node in mappingNode.Children)
            {
                string context = ((YamlScalarNode)node.Key).Value;
                VisitYamlNode(context, node.Value);
            }

            return _data;
        }

        private void VisitYamlNode(string context, YamlNode node)
        {
            switch (node)
            {
                case YamlScalarNode scalarNode:
                    VisitYamlScalarNode(context, scalarNode);
                    break;
                case YamlMappingNode mappingNode:
                    VisitYamlMappingNode(context, mappingNode);
                    break;
                case YamlSequenceNode sequenceNode:
                    VisitYamlSequenceNode(context, sequenceNode);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(node),
                        $"Unsupported YAML node type '{node.GetType().Name} was found. " +
                        $"Path '{_currentPath}', line {node.Start.Line} position {node.Start.Column}.");
            }
        }

        private void VisitYamlScalarNode(string context, YamlScalarNode scalarNode)
        {
            EnterContext(context);
            string currentKey = _currentPath;

            if (_data.ContainsKey(currentKey))
                throw new FormatException($"A duplicate key '{currentKey}' was found.");

            _data[currentKey] = scalarNode.Value;
            ExitContext();
        }


        private void VisitYamlMappingNode(string context, YamlMappingNode mappingNode)
        {
            EnterContext(context);

            foreach (var nodePair in mappingNode.Children)
            {
                string innerContext = ((YamlScalarNode)nodePair.Key).Value;
                VisitYamlNode(innerContext, nodePair.Value);
            }

            ExitContext();
        }

        private void VisitYamlSequenceNode(string context, YamlSequenceNode sequenceNode)
        {
            EnterContext(context);

            for (int i = 0; i < sequenceNode.Children.Count; ++i)
                VisitYamlNode(i.ToString(), sequenceNode.Children[i]);

            ExitContext();
        }

        private void EnterContext(string context)
        {
            _context.Push(context);
            _currentPath = ConfigurationPath.Combine(_context.Reverse());
        }

        private void ExitContext()
        {
            _context.Pop();
            _currentPath = ConfigurationPath.Combine(_context.Reverse());
        }
    }
}
