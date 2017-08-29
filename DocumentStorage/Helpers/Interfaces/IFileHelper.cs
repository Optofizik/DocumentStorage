using DocumentStorage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DocModel = DocumentStorage.Models.Document;

namespace DocumentStorage.Helpers.Interfaces
{
    public interface IFileHelper
    {
        string SaveFileAndReturnPath(HttpPostedFileBase file, out FileType fileType);
        IEnumerable<Document> FilterDocs(IEnumerable<DocModel> docs, string mask);
        byte[] GetFileBytes(string path);
        void Delete(string path);
    }
}
