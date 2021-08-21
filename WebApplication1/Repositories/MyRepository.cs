using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    /// <summary>
    /// implemnt IReopository - flexible layer between Db and Controllers
    /// </summary>
    public class MyRepository : IRepository
    {
        private ZooContext _context;
        public MyRepository(ZooContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetCatogry()
        {
            return _context.Categories.ToList();
        }
        public void InsertAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            _context.SaveChanges();
        }
        public void DeleteAnimal(int id)
        {
            var animal = _context.Animals.SingleOrDefault(m => m.AnimalId == id); //find Animal by id
            _context.Animals.Remove(animal);
            _context.SaveChanges();
        }
        public IEnumerable<Animal> GetAnimals()
        {
            return _context.Animals.Include(x => x.Comments);
        }
        public Animal GetAnimalById(int id)
        {
            var animal = _context.Animals //find Animal by id
          .Single(c => c.AnimalId == id);

            _context.Entry(animal) //Load The Comment list of Every Animal
                .Collection(c => c.Comments)
                .Load();

            _context.Entry(animal)  //Load The Category of Every Animal
                .Reference(c => c.category)
                .Load();

            return animal;
        }

        public void AddNewComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void EditAnimal(Animal animal)
        {
            var FindAnimal = _context.Animals.SingleOrDefault(an => an.AnimalId == animal.AnimalId); //Find animal from DB
            FindAnimal.CategoryId = animal.CategoryId;     //Only Four fileds  can change at Animal- Catogry
            FindAnimal.Description = animal.Description;  //Descriiption
            FindAnimal.PictureName = animal.PictureName;  //image
            FindAnimal.Name = animal.Name;                //Name
            _context.SaveChanges();
        }
    }
}
