using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core.CQRS;

namespace LoteriaES.Core.Bus
{
    public interface IMessageBus
    {
        void Send<T>(T command) where T : ICommand;
        void Publish<T>(T @event) where T : IEvent;
    }
}
