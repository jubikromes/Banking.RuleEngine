using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BankingRules.Data.Repository;
using BankingRules.Models;
using BankingRules.RuleEngine.Rules;
using System.Web.Http;
using System.Web.Mvc;

namespace BankingRules.Web.App_Start
{
    public class IocConfig
    {
        public static void ConfigureIoc()
        {
            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(IocConfig).Assembly);
            builder.RegisterApiControllers(typeof(IocConfig).Assembly);

            builder.RegisterType<ApplicationDbContext>();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterType(typeof(RuleService));
            builder.RegisterType(typeof(RuleDetailsService));
            var container = builder.Build();

            //web api dependecy resolver
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;


            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}