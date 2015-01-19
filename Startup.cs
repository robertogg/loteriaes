using Microsoft.Owin;
using Ninject;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoteriaES.Startup))]
namespace LoteriaES
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
