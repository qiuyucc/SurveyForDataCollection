using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(chensFyp.Startup))]
namespace chensFyp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
