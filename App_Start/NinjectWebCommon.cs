using LoteriaES.Core;
using LoteriaES.Core.Bus;
using LoteriaES.Core.Constants;
using LoteriaES.CQRS.CommandHandlers;
using LoteriaES.Infrastructure.Bus.Beezy.Infrastructure.Azure;
using LoteriaES.Infrastructure.EventStore;
using LoteriaES.Infrastructure.Repositories;
using Microsoft.WindowsAzure;
using Ninject.Extensions.Conventions;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LoteriaES.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LoteriaES.App_Start.NinjectWebCommon), "Stop")]

namespace LoteriaES.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
                kernel.Bind(typeof(IEventStore)).To(typeof(EventStore));

                kernel.Bind(
                    x => x.FromAssemblyContaining(typeof(CreateOrderCommandHandler))
                         .SelectAllClasses()
                        .BindAllInterfaces());

                RegisterServices(kernel);

               

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IMessageBus>()
                 .To<MessageBus>()
                 .InSingletonScope()
                 .WithConstructorArgument("kernel", kernel)
                 .WithConstructorArgument("connectionString", CloudConfigurationManager.GetSetting(Azure.Configuration.StorageConnectionString))
                 .WithConstructorArgument("path", CloudConfigurationManager.GetSetting(Azure.Configuration.ServiceBusKbEventsQueue));

        }        
    }
}
