using DocumentStorage.Helpers.Interfaces;
using DocumentStorage.Models;
using DocumentStorage.Models.Repository;
using DocumentStorage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentStorage.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IFileHelper fileHelper;

        public HomeController(IUnitOfWork unitOfWork, IFileHelper fileHelper)
        {
            this.unitOfWork = unitOfWork;
            this.fileHelper = fileHelper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.Categories = GetCategories().Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == 1 });
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(IndexViewModel model)
        {
            IQueryable<Document> allDocs = unitOfWork.DocumentRepo
                .Get()
                .Where(c => model.CategoryId == (int)Category.All ? true : c.CategoryId == model.CategoryId);

           Dictionary<int, string> fileType = GetFileTypeDict();


            IEnumerable<Document> media = allDocs
                .Where(c => c.Name.Contains(model.Mask) || c.Description.Contains(model.Mask));

            IEnumerable<Document> textDocs = allDocs
                .Where(c => c.FileTypeId == (int)FileType.Text && !(c.Name.Contains(model.Mask) || c.Description.Contains(model.Mask)));

            IEnumerable<Document> filtered = fileHelper.FilterDocs(textDocs, model.Mask);

            SearchViewModel viewModel = new SearchViewModel();

            viewModel.Documents = media
                .ToList()
                .Union(filtered)
                .OrderByDescending(c => c.ModificationDate)
                .GroupBy(c => fileType[c.FileTypeId]);

            viewModel.Categories = GetCategories().ToDictionary(c => c.Id, v => v.Name);

            return PartialView("_Search", viewModel);
        }

        [HttpGet]
        public ActionResult Upload()
        {
            if (User.IsInRole("Admin"))
            {
                UploadViewModel viewModel = new UploadViewModel();
                viewModel.Categories = GetCategories()
                    .Where(c => c.Id != (int)Category.All)
                    .Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name });

                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadViewModel model)
        {
            Document doc = new Document();

            doc.Description = model.Description;
            doc.Source = model.Source;
            doc.Name = Session["name"] != null ? Session["name"].ToString() : string.Empty;
            doc.Path = Session["path"] != null ? Session["path"].ToString() : string.Empty;
            doc.ContentType = Session["contentType"] != null ? Session["contentType"].ToString() : string.Empty;
            doc.ModificationDate = DateTime.Now;
            doc.ModificationUser = HttpContext.User.Identity.Name;
            doc.CategoryId = Convert.ToInt32(model.Category);
            doc.FileTypeId = Convert.ToInt32(Session["fileType"]);

            unitOfWork.DocumentRepo.Add(doc);

            return new HttpStatusCodeResult(200, "Успішно");
        }

        private Dictionary<int, string> GetFileTypeDict()
        {
            Dictionary<int, string> fileType = new Dictionary<int, string>();

            if (Session["FileTypes"] == null)
            {
                var ienum = unitOfWork.FileTypeRepo.Get();
                fileType = ienum.ToDictionary(c => c.Id, v => v.Name);
                Session["FileTypes"] = fileType;
            }
            else
            {
                fileType = Session["FileTypes"] as Dictionary<int, string>;
            }

            return fileType;
        }

        private IEnumerable<CategoryClass> GetCategories()
        {
            IEnumerable<CategoryClass> categories = null;

            if (Session["Categories"] == null)
            {
                categories = unitOfWork.CategoryRepo.Get();
                Session["Categories"] = categories;
            }
            else
            {
                categories = Session["Categories"] as IEnumerable<CategoryClass>;
            }

            return categories;
        }
    }
}