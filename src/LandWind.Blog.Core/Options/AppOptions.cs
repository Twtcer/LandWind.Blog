using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandWind.Blog.Core.Options
{
    /// <summary>
    /// AppOptions
    /// </summary>
    public class AppOptions
    {
        /// <summary>
        /// Swagger
        /// </summary>
        public SwaggerOptions Swagger { get; set; }

        /// <summary>
        /// StorageOptions
        /// </summary>
        public StorageOptions Storage { get; set; }

        /// <summary>
        /// Cors
        /// </summary>
        public CorsOptions Cors { get; set; }

        /// <summary>
        /// Jwt
        /// </summary>
        public JwtOptions Jwt { get; set; }

        /// <summary>
        /// Worker
        /// </summary>
        public WorkerOptions Worker { get; set; }

        /// <summary>
        /// Signature
        /// </summary>
        public SignatureOptions Signature { get; set; }

        /// <summary>
        /// TencentCloud
        /// </summary>
        public TencentCloudOptions TencentCloud { get; set; }

        /// <summary>
        /// Authorize
        /// </summary>
        public AuthorizeOptions Authorize { get; set; }

        /// <summary>
        /// 微信消息推送服务
        /// </summary>
        public FtqqOptions Ftqq { get; set; }
    }
}
