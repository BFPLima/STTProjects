using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(STTProjects.TimeSheet.WebApp.Startup))]
namespace STTProjects.TimeSheet.WebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
