using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentStorage.Models.Repository
{
    public interface IDocumentRepo
    {
        IQueryable<Document> Get();
        Document GetById(int id);
        void Add(Document doc);
        string Delete(int id);
    }
}