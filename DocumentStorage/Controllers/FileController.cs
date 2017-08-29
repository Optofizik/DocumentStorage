using DocumentStorage.Helpers.Interfaces;
using DocumentStorage.Models;
using DocumentStorage.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentStorage.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private IFileHelper fileHelper;
        private IUnitOfWork unitOfWork;

        public FileController(IFileHelper fileHelper, IUnitOfWork unitOfWork)
        {
            this.fileHelper = fileHelper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult Upload()
        {
            var file = Request.Files[0];
            FileType fileType;
            string path = fileHelper.SaveFileAndReturnPath(file, out fileType);

            Session["path"] = path;
            Session["name"] = file.FileName;
            Session["fileType"] = (int)fileType;
            Session["contentType"] = file.ContentType;

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            Document doc = unitOfWork.DocumentRepo.GetById(id);

            if (doc != null)
            {
                byte[] fileBytes = fileHelper.GetFileBytes(doc.Path);

                return File(fileBytes, doc.ContentType);
            }

            return new HttpStatusCodeResult(404);           
        }

        [HttpPost]
        public HttpStatusCodeResult Delete(int id)
        {
            string path = unitOfWork.DocumentRepo.Delete(id);
            fileHelper.Delete(path);

            return new HttpStatusCodeResult(200);
        }
    }
}