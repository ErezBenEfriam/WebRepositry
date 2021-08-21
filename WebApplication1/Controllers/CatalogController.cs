using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class CatalogController : Controller
    {
        IRepository _repository;
        public CatalogController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult CatalogAction(int id = 1, bool isAdmin = false)
        {
            var animal = from Animal in _repository.GetAnimals()
                         where Animal.CategoryId == id
                         select Animal;
            ViewBag.Catagories = _repository.GetCatogry().ToList();
            ViewBag.IsAdmin = isAdmin;

            return View(animal.ToList());
        }
        public IActionResult CatalogForAdm()
        {
            return RedirectToAction("CatalogAction", new { id = 1, isAdmin = true });
        }
        public IActionResult AnimalPage(int id = 0)
        {

            return View(_repository.GetAnimalById(id));
        }
        [HttpPost]
        public IActionResult AddNewComment(string Newcomment, int Animalid)
        {
            Comment c = new Comment() { CommentString = Newcomment, AnimalId = Animalid };
            _repository.AddNewComment(c);
            return RedirectToAction("AnimalPage", new { id = Animalid });
        }

    }
}
