using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    /// <summary>
    /// Layer between Date base And Controllers for making project more agile and ready to changes
    /// </summary>
    public interface IRepository
    {
        IEnumerable<Category> GetCatogry(); //return all Catogreis
        void InsertAnimal(Animal animal);  //Add New Animal 
        void DeleteAnimal(int id); //Delte Animal
        IEnumerable<Animal> GetAnimals(); //Return IEnumerable of all animals
        Animal GetAnimalById(int id); //find animal by id
        void AddNewComment(Comment comment); // add new comment to comment section
        void EditAnimal(Animal animal);   //make changes in exist animal
    }
}
