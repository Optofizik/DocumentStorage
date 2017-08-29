using DocumentStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentStorage.ViewModels
{
    public class LoginsViewModel
    {
        public IEnumerable<Login> Users { get; set; }
        public IEnumerable<SimpleRole> Roles { get; set; }
        public string CurrentUserId { get; set; }
    }
}