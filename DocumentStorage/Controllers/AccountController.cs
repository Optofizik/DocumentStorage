using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DocumentStorage.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using DocumentStorage.ViewModels;
using System.Collections.Generic;
using DocumentStorage.Models.Repository;

namespace DocumentStorage.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IUnitOfWork unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public AccountController(ApplicationSignInManager signInManager)
        {
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
       
        public UserManager<ApplicationUser> UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().Get<UserManager<ApplicationUser>>();
            }
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return roleManager ?? HttpContext.GetOwinContext().Get<RoleManager<IdentityRole>>();
            }
        }
    
        [AllowAnonymous]
        public ActionResult Login()
        {
            string homeUrl = Url.Action("Index", "Home");

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return new RedirectResult(homeUrl);
            }

            ViewBag.ReturnUrl = homeUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Сбои при входе не приводят к блокированию учетной записи
            // Чтобы ошибки при вводе пароля инициировали блокирование учетной записи, замените на shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Невдала спроба входу");
                    return View(model);
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Logins()
        {
            LoginsViewModel viewModel = new LoginsViewModel();

            viewModel.Users = unitOfWork.UserRepo.Get().Select(c => new Login() { Id = c.Id, UserLogin = c.UserName, Role = c.Roles.Select(v => v.RoleId).FirstOrDefault() }).ToList();
            viewModel.Roles = unitOfWork.RoleRepo.Get().Select(c => new SimpleRole() { Id = c.Id, Name = c.Name }).ToList();
            viewModel.CurrentUserId = User.Identity.GetUserId();

            foreach (var user in viewModel.Users)
            {
                user.Role = viewModel.Roles.FirstOrDefault(c => c.Id == user.Role).Name;
            }

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Manage()
        {
            ManageViewModel viewModel = new ManageViewModel();

            viewModel.Roles = RoleManager.Roles.Select(c => new SelectListItem() { Text = c.Name, Value = c.Name }).ToList();

            return View("Manage", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ManageViewModel model)
        {
            var user = new ApplicationUser();
            user.UserName = model.Login;

            var chkUser = UserManager.Create(user, model.Password);

            //Add default User to Role Admin    
            if (chkUser.Succeeded)
            {
                UserManager.AddToRole(user.Id, model.Role);
            }

            return RedirectToAction("Logins");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
           

            return RedirectToAction("Logins");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}