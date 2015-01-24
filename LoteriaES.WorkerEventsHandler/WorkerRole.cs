using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LoteriaES.Core;
using LoteriaES.Core.Bus;
using LoteriaES.Core.Constants;
using LoteriaES.Core.CQRS;
using LoteriaES.CQRS.EventHandlers;
using LoteriaES.Infrastructure.Bus.Beezy.Infrastructure.Azure;
using LoteriaES.Infrastructure.Repositories;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json.Linq;
using Ninject;
using Ninject.Extensions.Conventions;

namespace LoteriaES.WorkerEventsHandler
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        private CloudQueue _eventsQueue;
        private IKernel _kernel;
        public override void Run()
        {
            Trace.TraceInformation("LoteriaES.WorkerEventsHandler is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;
            bool result = base.OnStart();
            Trace.TraceInformation("LoteriaES.WorkerEventsHandler has been started");

            ConfigureStorageQueues();
            ConfigureIoC();

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("LoteriaES.WorkerEventsHandler is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("LoteriaES.WorkerEventsHandler has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var brokeredMessage = _eventsQueue.GetMessage();

                    if (brokeredMessage != null)
                    {
                        dynamic dataMessage = JObject.Parse(brokeredMessage.AsString);
                        var typeName = dataMessage.type;
                        var typeArguments = Type.GetType(typeName.ToString());
                        Type type = typeof(IEventHandler<>).MakeGenericType(typeArguments);
                        var eventHandlers = _kernel.GetAll(type);

                        var messageBodyJObject = (dataMessage.content as JObject);

                        if (messageBodyJObject != null)
                        {
                            var messageBody = messageBodyJObject.ToObject(typeArguments);
                            foreach (dynamic handler in eventHandlers)
                            {
                                handler.Handle(messageBody);
                            }
                            _eventsQueue.DeleteMessage(brokeredMessage);
                        }
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception exception)
                {
                    //Gestión de errores
                }
            }
        }

        private void ConfigureStorageQueues()
        {
            var connectionString = CloudConfigurationManager.GetSetting(Azure.Configuration.StorageConnectionString);
            var topicPath = CloudConfigurationManager.GetSetting(Azure.Configuration.ServiceBusKbEventsQueue);

            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var cloudQueueClient = storageAccount.CreateCloudQueueClient();

            _eventsQueue = cloudQueueClient.GetQueueReference(topicPath);

            _eventsQueue.CreateIfNotExists();
        }

        private void ConfigureIoC()
        {
            var sqlAzureConnectionString = CloudConfigurationManager.GetSetting(Azure.Configuration.SqlAzureConnectionString);

            _kernel = new StandardKernel();

            _kernel.Bind<IMessageBus>()
                 .To<MessageBus>()
                 .InSingletonScope()
                 .WithConstructorArgument("kernel", _kernel)
                 .WithConstructorArgument("connectionString", CloudConfigurationManager.GetSetting(Azure.Configuration.StorageConnectionString))
                 .WithConstructorArgument("path", CloudConfigurationManager.GetSetting(Azure.Configuration.ServiceBusKbEventsQueue));

            _kernel.Bind(typeof(IOrderRepository))
                  .To(typeof(OrderRepository));
            _kernel.Bind(typeof(IProductRepository))
                 .To(typeof(ProductRepository));

            _kernel.Bind(
                x => x.FromAssemblyContaining(typeof(OrderItemIsCreated))
                      .SelectAllClasses()
                      .BindAllInterfaces());

          
            //GlobalHost.DependencyResolver = new NinjectSignalRDependencyResolver(_kernel);
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new SignalRUserIdProvider());
            //GlobalHost.DependencyResolver.UseRedis(Settings.GetSetting(Azure.Configuration.RedisConnectionString), int.Parse(Settings.GetSetting(Azure.Configuration.RedisPort))
            //                , Settings.GetSetting(Azure.Configuration.RedisPassword), Settings.GetSetting(Azure.Configuration.RedisAppName));
        }
    }
}
