using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentStorage.ViewModels
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = "Заповніть будь ласка поле")]
        [Display(Name = "Пошук по назві або тексту:")]
        public string Mask { get; set; }
        
        [Display(Name = "Категорія:")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}