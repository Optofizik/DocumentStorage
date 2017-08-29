using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentStorage.Models.Repository
{
    public class DocumentRepo : IDocumentRepo
    {
        private ApplicationDbContext context;

        public DocumentRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Document doc)
        {
            context.Document.Add(doc);
            context.SaveChanges();
        }

        public string Delete(int id)
        {
            string path = string.Empty;

            Document doc = GetById(id);
            path = doc.Path;

            context.Document.Remove(doc);
            context.SaveChanges();

            return path;
        }

        public IQueryable<Document> Get()
        {
            return context.Document;
        }

        public Document GetById(int id)
        {
            return context.Document.FirstOrDefault(c => c.Id == id);
        }
    }
}