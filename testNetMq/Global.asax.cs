using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using testNetMq.Services;

namespace testNetMq
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterFilterProvider();

            var mqConnection = ConfigurationManager.AppSettings["RabbitMQConnectionString"];
            builder.RegisterEasyNetQ(mqConnection);
            builder.RegisterType<QueueSender>().As<IQueueSender>().InstancePerLifetimeScope().PreserveExistingDefaults();
            builder.RegisterType<QueueSender2>().As<IQueueSender>().InstancePerLifetimeScope().PreserveExistingDefaults();
            builder.RegisterType<TestConsumer>().As<ITestConsumer>().InstancePerLifetimeScope();
            
            builder.RegisterType<QueueConsumer>().As<IQueueConsumer>()
                   .InstancePerLifetimeScope()
                   .OnActivated(args =>
                   {
                       args.Instance.Register();
                   })
                   .AutoActivate();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}