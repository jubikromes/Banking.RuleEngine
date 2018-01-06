using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankingRules.Web.Startup))]
namespace BankingRules.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
