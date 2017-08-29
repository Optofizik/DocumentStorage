using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using DocumentStorage.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DocumentStorage
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Настройка контекста базы данных, диспетчера пользователей и диспетчера входа для использования одного экземпляра на запрос
            app.CreatePerOwinContext(Models.ApplicationDbContext.GetContext);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Включение использования файла cookie, в котором приложение может хранить информацию для пользователя, выполнившего вход,
            // и использование файла cookie для временного хранения информации о входах пользователя с помощью стороннего поставщика входа
            // Настройка файла cookie для входа
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Позволяет приложению проверять метку безопасности при входе пользователя.
                    // Эта функция безопасности используется, когда вы меняете пароль или добавляете внешнее имя входа в свою учетную запись.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
        }

        private void CreateUsersAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);


                var user = new ApplicationUser();
                user.UserName = "admin";
                string userPWD = "87654321";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, role.Name);
                }
            }

            if (!roleManager.RoleExists("User"))
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);               
            }
        }
    }
}