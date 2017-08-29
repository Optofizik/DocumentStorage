using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentStorage.Models
{
    public class Login
    {
        public string Id { get; set; }
        public string UserLogin { get; set; }
        public string Role { get; set; }
    }

    public class SimpleRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}