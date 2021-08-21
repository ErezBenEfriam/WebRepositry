using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        IRepository _repository; //injection dependency for MyReopistory
        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index() //Find the two animals with most comments
        {
            var TopTwoComments = (from Animal in _repository.GetAnimals()
                                  orderby Animal.Comments.Count() descending
                                  select Animal).Take(2).ToList();
            return View(TopTwoComments); //pass them in the view
        }
    }
}
