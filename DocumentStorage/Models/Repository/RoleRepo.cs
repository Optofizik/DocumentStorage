using DocumentStorage.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DocumentStorage.Models.Repository
{
    public class RoleRepo : IRoleRepo
    {
        private ApplicationDbContext context;

        public RoleRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<IdentityRole> Get()
        {
            return context.Roles.ToList();
        }
    }
}