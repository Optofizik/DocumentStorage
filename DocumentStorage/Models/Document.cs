using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DocumentStorage.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Path { get; set; }
        public DateTime ModificationDate { get; set; }
        public string ModificationUser { get; set; }
        public string ContentType { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public CategoryClass Category { get; set; }

        [ForeignKey("FileType")]
        public int FileTypeId { get; set; }
        public FileTypeClass FileType { get; set; }
    }
}