using DocumentStorage.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentStorage.Models.Repository
{
    public class UserRepo : IUserRepo
    {
        private ApplicationDbContext context;

        public UserRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ApplicationUser> Get()
        {
            return context.Users.ToList();
        }

        public ApplicationUser Get(string id)
        {
            return context.Users.FirstOrDefault(c => c.Id == id);
        }

        public void Delete(string id)
        {
            var user = Get(id);

            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
    }
}