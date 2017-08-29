using DocumentStorage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DocumentStorage.Common
{
    public static class StaticInfo
    {
        public static Dictionary<Category, Dictionary<string, string>> CategoryTextRepresenting = new Dictionary<Category, Dictionary<string, string>>()
        {
            { Category.Fiz, new Dictionary<string, string>() { { "class", "success" }, { "text", "Фіз. особи" } } },
        };

        public static string LogFileName = ConfigurationManager.AppSettings["LogFileName"]; 
    }
}