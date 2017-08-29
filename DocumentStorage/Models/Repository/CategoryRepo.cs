using DocumentStorage.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentStorage.Models.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private ApplicationDbContext context;

        public CategoryRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<CategoryClass> Get()
        {
            return context.Category;
        }
    }
}