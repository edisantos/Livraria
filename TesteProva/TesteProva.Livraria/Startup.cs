using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TesteProva.Livraria.Startup))]
namespace TesteProva.Livraria
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


        }

    }
}
