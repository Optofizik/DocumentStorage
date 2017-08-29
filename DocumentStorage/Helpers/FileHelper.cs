using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using DocumentStorage.Helpers.Interfaces;
using DocumentStorage.Models;
using Microsoft.Office.Interop.Word;
using DocModel = DocumentStorage.Models.Document;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentStorage.Helpers
{
    public class FileHelper : IFileHelper
    {
        private Dictionary<FileType, string[]> typePath;
        private string mask;
        private List<DocModel> filtered;
        private string BaseDirectory { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        private WordprocessingDocument package;

        public FileHelper()
        {
            typePath = new Dictionary<FileType, string[]>()
            {
                {FileType.Image, new string[] { "image" } },
                {FileType.Audio, new string[] { "audio" } },
                {FileType.Video, new string[] { "video" } },
                {FileType.Text, new string[] { "text", "application" } }
            };
        }

        public string SaveFileAndReturnPath(HttpPostedFileBase file, out FileType fileType)
        {
            fileType = GetFileType(file.ContentType);
            string relativePath = $"\\files\\{fileType.ToString().ToLower()}";
            string path = BaseDirectory + relativePath;

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                file.SaveAs(path + "\\" + file.FileName);
                relativePath += "\\" + file.FileName;
            }
            catch
            {
                relativePath = string.Empty;
            }

            return relativePath;
        }

        public IEnumerable<DocModel> FilterDocs(IEnumerable<DocModel> docs, string mask)
        {
            this.mask = mask;
            this.filtered = new List<DocModel>();

            Parallel.ForEach<DocModel>(docs, ReadAllLines);

            return this.filtered;
        }

        private void ReadAllLines(DocModel doc)
        {
            bool result = false;

            if (IsWordDocument(doc.Path))
            {
                result = ReadWordDocument(BaseDirectory + doc.Path, this.mask);
            }
            else
            {
                result = ReadUsualTextFile(Path.Combine(BaseDirectory, doc.Path), this.mask);
            }

            if (result)
                this.filtered.Add(doc);
        }

        private bool ReadWordDocument(string path, string mask)
        {
            bool result = false;

            using (package = WordprocessingDocument.Open(path, false))
            {
                OpenXmlElement element = package.MainDocumentPart.Document.Body;

                foreach (OpenXmlElement section in element.Elements())
                {
                    if (section.InnerText.ToUpper().Contains(mask.ToUpper()))
                    {
                        result = true;
                        break;
                    }
                }

                package.Close();
            }      

            return result;
        }

        private bool ReadUsualTextFile(string path, string mask)
        {
            string line = string.Empty;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (TextReader tr = new StreamReader(fs, Encoding.UTF8))
                {
                    line = tr.ReadLine().ToUpper();
                    while (line != null)
                    {
                        if (line.Contains(this.mask.ToUpper()))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsWordDocument(string path)
        {
            return path.EndsWith("docx", StringComparison.InvariantCultureIgnoreCase);
        }

        private FileType GetFileType(string contenType)
        {
            foreach (var item in typePath)
            {
                if (item.Value.Any(c => contenType.Contains(c)))
                    return item.Key;
            }

            return FileType.Text;
        }

        public byte[] GetFileBytes(string path)
        {
            byte[] bytes = File.ReadAllBytes(BaseDirectory + path);
            return bytes;
        }

        public void Delete(string path)
        {
            File.Delete(BaseDirectory + path);
        }
    }
}
