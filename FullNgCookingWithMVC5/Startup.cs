using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FullNgCookingWithMVC5.Startup))]
namespace FullNgCookingWithMVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
