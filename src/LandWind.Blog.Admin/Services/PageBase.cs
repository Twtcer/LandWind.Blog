using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AntDesign;
using LandWind.Blog.Core.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LandWind.Blog.Admin.Services
{
    public class PageBase : AntDomComponentBase
    { 
        [Inject]
        IHttpClientFactory HttpClientFactory { get; set; }

        [Inject]
        public MessageService Message { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public virtual async Task<T> GetResultAsync<T>(string url, string json = "", HttpMethod method = null)
        {
            var http = HttpClientFactory.CreateClient("api");

            var token = await Js.InvokeAsync<string>("localStorage.getItem", "token");
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            string response;
            if (method is null || method == HttpMethod.Get)
            {
                response = await http.GetStringAsync(url);
            }
            else
            {
                var content = new ByteArrayContent(Encoding.UTF8.GetBytes(json));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var ret = new HttpResponseMessage();
                if (method == HttpMethod.Post)
                {
                    ret = await http.PostAsync(url, content);
                }
                else if (method == HttpMethod.Put)
                {
                    ret = await http.PutAsync(url, content);
                }
                else if (method == HttpMethod.Delete)
                {
                    ret = await http.DeleteAsync(url);
                }

                response = await ret.Content.ReadAsStringAsync();
            }

            return response.Deserialize<T>();
        }
    }
}
