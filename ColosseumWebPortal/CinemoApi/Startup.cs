using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CinemoApi.Startup))]
namespace CinemoApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
