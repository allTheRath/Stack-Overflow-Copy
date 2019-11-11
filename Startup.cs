using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QA_Project.Startup))]
namespace QA_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
