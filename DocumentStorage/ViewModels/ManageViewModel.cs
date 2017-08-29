using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentStorage.ViewModels
{
    public class ManageViewModel
    {
        [Required(ErrorMessage = "Заповніть будь ласка поле")]
        [Display(Name = "Логін")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Заповніть будь ласка поле")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Виберіть роль зі списку")]
        [Display(Name = "Роль")]
        public string Role { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}