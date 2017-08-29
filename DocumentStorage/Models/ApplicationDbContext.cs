using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace DocumentStorage.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
           
        }

        public static ApplicationDbContext GetContext()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Document> Document { get; set; }
        public DbSet<CategoryClass> Category { get; set; }
        public DbSet<FileTypeClass> FileType { get; set; }
    }
}