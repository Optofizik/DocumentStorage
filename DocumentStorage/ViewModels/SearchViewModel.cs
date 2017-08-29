using DocumentStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentStorage.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<IGrouping<string, Document>> Documents { get; set; }
        public Dictionary<int, string> Categories { get; set; }
    }
}