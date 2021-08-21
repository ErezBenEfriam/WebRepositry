using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class AdministorController : Controller
    {
        IRepository _repository; //injection dependency for MyReopistory
        public AdministorController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult DeltetAnimal(int id, int CategoryId)
        {
            _repository.DeleteAnimal(id);
            return RedirectToAction("CatalogAction", "Catalog", new { id = CategoryId, isAdmin = true });
        }

        public IActionResult EditAnimalPage(int id = 0)
        {
            if (id == 0) //for create new Animal
            {
                ViewBag.EditOrNew = "Create new Animal";
                ViewBag.CreateNew = true;
                return View();
            }//for edit exist Animal
            ViewBag.EditOrNew = "Edit Animal";
            ViewBag.CreateNew = false;
            return View(_repository.GetAnimalById(id));
        }
        [HttpPost]
        public IActionResult EditAnimal(Animal animal)//create Or Edit Animal
        {
            if (ModelState.IsValid)
            {
                if (animal.AnimalId == 0)//this is for create a new animal
                {
                    _repository.InsertAnimal(animal);
                }
                else //else we edit The details of existed animal
                {
                    _repository.EditAnimal(animal);
                }
                return RedirectToAction("AnimalPage", "Catalog", new { id = animal.AnimalId }); //the page of the animal
            }
            else
            {
                if (animal.AnimalId == 0) //this is for create new animal
                {
                    ViewBag.CreateNew = true;
                   return View("EditAnimalPage");
                }
                ViewBag.CreateNew = false;
                return View("EditAnimalPage", _repository.GetAnimalById(animal.AnimalId));
            }
        }
    }
}
