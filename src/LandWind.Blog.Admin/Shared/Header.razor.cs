using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace LandWind.Blog.Admin.Shared
{
    public partial class Header
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public async Task OnClickCallBackAsync(MenuItem menu)
        {
            switch (menu.Key)
            {
                case "user":
                   // NavigationManager2.NavigateTo("/users");
                    break;
            }
        }
    }
}