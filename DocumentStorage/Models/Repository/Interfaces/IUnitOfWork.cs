using DocumentStorage.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentStorage.Models.Repository
{
    public interface IUnitOfWork
    {
        IDocumentRepo DocumentRepo { get; }
        IFileTypeRepo FileTypeRepo { get; }
        ICategoryRepo CategoryRepo { get; }
        IUserRepo     UserRepo { get; }
        IRoleRepo     RoleRepo { get; }
    }
}
