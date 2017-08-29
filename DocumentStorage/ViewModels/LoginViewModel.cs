using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentStorage.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логін")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати мене")]
        public bool RememberMe { get; set; }
    }
}
