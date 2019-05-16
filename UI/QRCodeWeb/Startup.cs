using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecureQRCodeWeb.Startup))]
namespace SecureQRCodeWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
