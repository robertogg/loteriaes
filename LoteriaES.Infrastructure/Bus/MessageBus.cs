using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core;
using LoteriaES.Core.Bus;
using LoteriaES.Core.CQRS;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json.Linq;
using Ninject;

namespace LoteriaES.Infrastructure.Bus
{
   

    namespace Beezy.Infrastructure.Azure
    {
        public class MessageBus : IMessageBus
        {
            private readonly IKernel _kernel;
            private readonly CloudQueue _queue;
            public MessageBus(IKernel kernel, string connectionString, string path)
            {
                _kernel = kernel;

                var storageAccount = CloudStorageAccount.Parse(connectionString);
                var cloudQueueClient = storageAccount.CreateCloudQueueClient();

                _queue = cloudQueueClient.GetQueueReference(path);
                _queue.CreateIfNotExists();
            }

            public void Send<T>(T command) where T : ICommand
            {
                var commandHandler =
                    _kernel.GetAll<ICommandHandler<T>>();

                if (commandHandler == null)
                    throw new CommandHandlerNotFoundException(typeof(T));

                commandHandler.First().Execute(command);
            }

            public void Publish<T>(T @event) where T : IEvent
            {
                dynamic message = new JObject();
                message.type = @event.GetType().AssemblyQualifiedName;
                message.content = JObject.FromObject(@event);
                _queue.AddMessage(new CloudQueueMessage(message.ToString()));
            }
        }
    }
}
