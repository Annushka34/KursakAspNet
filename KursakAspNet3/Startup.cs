using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KursakAspNet3.Startup))]
namespace KursakAspNet3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
