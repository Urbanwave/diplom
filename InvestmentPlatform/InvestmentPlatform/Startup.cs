using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InvestmentPlatform.Startup))]
namespace InvestmentPlatform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
