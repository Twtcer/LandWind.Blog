namespace LandWind.Blog.Core.Options
{
    /// <summary>
    /// 跨域配置
    /// </summary>
    public class CorsOptions
    {
        /// <summary>
        /// The name of the policy.
        /// </summary>
        public string PolicyName { get; set; }

        /// <summary>
        /// The origins that are allowed.
        /// </summary>
        public string Origins { get; set; }
    }
}