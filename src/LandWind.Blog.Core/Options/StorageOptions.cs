using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Core.Options
{
    /// <summary>
    /// Db storeage options
    /// </summary>
    public class StorageOptions
    {
        /// <summary>
        /// 启用数据库
        /// </summary>
        public string Enable { get; set; }

        /// <summary>
        /// 数据库连接字符串 
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// redis 启用状态
        /// </summary>
        public bool RedisStatus { get; set; }

        /// <summary>
        /// redis 连接字符串
        /// </summary>
        public string RedisConnection { get; set; }
    }
}
