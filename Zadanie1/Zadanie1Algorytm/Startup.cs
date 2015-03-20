using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zadanie1Algorytm.Startup))]
namespace Zadanie1Algorytm
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
