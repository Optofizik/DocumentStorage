using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using DocumentStorage.Helpers.Interfaces;
using DocumentStorage.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;
using DocumentStorage.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using DocumentStorage.Controllers;
using DocumentStorage.Models.Repository;

namespace DocumentStorage
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IFileHelper, FileHelper>();
            container.RegisterType<IdentityDbContext<ApplicationUser>, ApplicationDbContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
                  
            container.RegisterType<AccountController>(new InjectionConstructor(container.Resolve<IUnitOfWork>()));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}