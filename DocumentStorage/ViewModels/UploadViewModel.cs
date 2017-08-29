using DocumentStorage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentStorage.ViewModels
{
    public class UploadViewModel
    {
        [Required(ErrorMessage = "Заповніть поле \"Опис\"")]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Заповніть поле \"Категорія\"")]
        [Display(Name = "Категорія")]
        public Category Category { get; set; }

        [Display(Name = "Джерело")]
        public string Source { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}