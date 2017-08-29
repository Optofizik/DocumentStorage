using DocumentStorage.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentStorage.Models.Repository
{
    public class FileTypeRepo : IFileTypeRepo
    {
        private ApplicationDbContext context;

        public FileTypeRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<FileTypeClass> Get()
        {
            return context.FileType;
        }
    }
}