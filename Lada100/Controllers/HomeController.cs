using Microsoft.AspNetCore.Mvc;
using Lada100.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;
using Lada100.Controllers;

namespace Lada100.Controllers
{
    public class HomeController : Controller
    {

        private readonly SpisoksRepository spisoksRepository;

        public HomeController(SpisoksRepository spisoksRepository)
        {
            this.spisoksRepository = spisoksRepository;
        }

        public IActionResult Index(string search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var foundPets = spisoksRepository.SearchSpisok(search);
                return View(foundPets);
            }

            var spisok = spisoksRepository.GetAllSpisok();
            return View(spisok);
        }

        public IActionResult Details(int Id)
        {


            var spisok = spisoksRepository.GetSingleSpisok(Id);
            return View(spisok);

        }
        [HttpGet]
        public IActionResult New(int id)
        {
            ViewBag.IsEditMode = "false";
            var spisok = new Spisok();
            return View(spisok);
        }
        [HttpPost]
        public IActionResult New(Spisok spisok, string IsEditMode, IFormFile file)
        {
            if (IsEditMode.Equals("false"))

            {
                spisoksRepository.CreateSpisok(spisok);
                UploadFile(file, spisok.Id);
            }
            else
            {
                spisoksRepository.EditSpisok(spisok);
                UploadFile(file, spisok.Id);
            }
            return RedirectToAction("Index");
            //return RedirectToAction(nameof(Index));
        }
        //------------------------------------------------------------------------------------------------попытка сохранить фото-----------------
        //public void UploadFile(IFormFile file, int Id)
        //{
        //    var fileName = file.FileName;
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
        //    using (var fileStream = new FileStream(path,FileMode.Create))
        //    {
        //        file.CopyTo(fileStream);
        //    }

        //    var spisok = spisoksRepository.GetSingleSpisok(Id);//(x => x.Id == spisokId);
        //    spisok.Img1 = fileName;
        //    spisoksRepository.EditSpisok(spisok);
        //}

        public void UploadFile(IFormFile file, int Id)
        {
            try
            {
                var fileName = file.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                var spisok = spisoksRepository.GetSingleSpisok(Id);//(x => x.Id == spisokId);
                spisok.Img1 = fileName;
                spisoksRepository.EditSpisok(spisok);
            }
            catch (Exception)
            {
                Response.Redirect("Index");
                //return Content("gde kartina");
                //Response.WriteAsync("HET KARTINKI !!!");
            }
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            ViewBag.IsEditMode = "true";
            var spisok = spisoksRepository.GetSingleSpisok(Id);
            return View("New", spisok);
        }
        public IActionResult Delete(int Id)
        {
            var spisok = spisoksRepository.GetSingleSpisok(Id);
            spisoksRepository.DeleteSpisok(spisok);

            return RedirectToAction(nameof(Index));
        }
    }
}
