using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentStorage.Models.Repository.Interfaces
{
    public interface IUserRepo
    {
        ApplicationUser Get(string id);
        IEnumerable<ApplicationUser> Get();
        void Delete(string id);
    }
}