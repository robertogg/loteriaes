using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteriaES.Core.CQRS
{
    public interface IEventHandler<in T> where T : IEvent
    {   
        void Handle(T domainEvent);
    }
}
