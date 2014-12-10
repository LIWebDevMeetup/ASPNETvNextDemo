using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GlennsGadgets.Startup))]
namespace GlennsGadgets
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //TODO: why do I have to always remember this?
            app.MapSignalR();
        }
    }
}
