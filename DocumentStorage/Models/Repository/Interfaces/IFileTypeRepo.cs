using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentStorage.Models.Repository.Interfaces
{
    public interface IFileTypeRepo
    {
        IQueryable<FileTypeClass> Get();
    }
}
