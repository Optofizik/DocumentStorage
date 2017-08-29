using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocumentStorage.Startup))]
namespace DocumentStorage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUsersAndRoles();
        }
    }
}
