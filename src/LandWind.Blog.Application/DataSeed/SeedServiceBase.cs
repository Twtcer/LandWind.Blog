using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LandWind.Blog.Core.Extensions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace LandWind.Blog.Application.DataSeed
{
    public abstract class SeedServiceBase : ITransientDependency
    {
        public abstract Task SeedAsync();

        public string BasePath { get; set; } = $"{Directory.GetCurrentDirectory()}\\Json\\";

        public async Task SeedAsync<TEntity, TKey>(IRepository<TEntity, TKey> repository, string jsonName,string key="") where TEntity   : class, IEntity<TKey>
        {
            if (await repository.GetCountAsync() > 0) return;

            var path = Path.Combine(BasePath, jsonName);

            var data = await path.FromJsonFile<List<TEntity>>(key);
            if (!data.Any()) return;

            await repository.InsertManyAsync(data);

            Console.WriteLine($"Successfully processed {data.Count} data.");
        }
    }
}
