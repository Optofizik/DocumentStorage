using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentStorage.Models.Repository.Interfaces;

namespace DocumentStorage.Models.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        private IDocumentRepo documentRepo;
        public IDocumentRepo DocumentRepo
        {
            get
            {
                if (documentRepo == null)
                    documentRepo = new DocumentRepo(context);

                return documentRepo;
            }
        }

        private IFileTypeRepo fileTypeRepo;
        public IFileTypeRepo FileTypeRepo
        {
            get
            {
                if (fileTypeRepo == null)
                    fileTypeRepo = new FileTypeRepo(context);

                return fileTypeRepo;
            }
        }

        private ICategoryRepo categoryRepo;
        public ICategoryRepo CategoryRepo
        {
            get
            {
                if (categoryRepo == null)
                    categoryRepo = new CategoryRepo(context);

                return categoryRepo;
            }
        }

        private IUserRepo userRepo;
        public IUserRepo UserRepo
        {
            get
            {
                if(userRepo == null)
                {
                    userRepo = new UserRepo(context);
                }

                return userRepo;
            }
        }

        private IRoleRepo roleRepo;
        public IRoleRepo RoleRepo
        {
            get
            {
                if (roleRepo == null)
                {
                    roleRepo = new RoleRepo(context);
                }

                return roleRepo;
            }
        }
    }
}