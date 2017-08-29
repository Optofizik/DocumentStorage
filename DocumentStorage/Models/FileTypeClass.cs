using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentStorage.Models
{
    public class FileTypeClass
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
