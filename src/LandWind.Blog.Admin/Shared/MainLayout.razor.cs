using System;
using System.IO;
using System.Threading.Tasks;
using AntDesign.Pro.Layout;
using LandWind.Blog.Core.Extensions;

namespace LandWind.Blog.Admin.Shared
{
    public partial class MainLayout
    {
        private MenuDataItem[] MenuData { get; set; }
         
        //protected override async Task OnInitializedAsync()
        //{
        //    await base.OnInitializedAsync();

        //    var json = await File.ReadAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Shared\\menu.json"));

        //    if (string.IsNullOrEmpty(json)) throw new System.Exception("Menu json file not exisit!");

        //    MenuData = json.Deserialize<MenuDataItem[]>();
        //}
    }
}