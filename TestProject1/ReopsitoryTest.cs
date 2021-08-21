using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace TestProject1
{
    [TestClass]
    public class ReopsitoryTest
    {
        private DbContextOptions<ZooContext> _options;

        [TestInitialize]
        public void Init()
        {
            _options = new DbContextOptionsBuilder<ZooContext>()
                .UseInMemoryDatabase(databaseName: "DbContextDatabase").Options;
        }
        [TestCleanup]
        public void Cleanup()
        {
            // Insert seed data into the database using one instance of the context
            using (var context = new ZooContext(_options))
            {
                context.Animals.RemoveRange(context.Animals);
                context.Categories.RemoveRange(context.Categories);
                context.Comments.RemoveRange(context.Comments);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void MyRepository_GetAnimalById_ReturnAnimal()
        {
            // Arrange
            using var context = new ZooContext(_options);
            context.Animals.Add(new Animal { AnimalId = 99, Name = "Cat", CategoryId = 3, Description = "cool" });
            context.SaveChanges();
            var repository = new MyRepository(context);
            var expectedName = "Cat";
            var expectedCategoryId = 3;
            var expectedDescription = "cool";

            // Act
            var result = repository.GetAnimalById(99);

            // Assert
            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedCategoryId, result.CategoryId);
            Assert.AreEqual(expectedDescription, result.Description);
        }
        [TestMethod]
        public void MyRepository_GetCatogry_GetAllCategoriesl()
        {
            // Arrange
            using var context = new ZooContext(_options);
            context.Categories.Add(new Category { CategoryId = 1, Name = "mamels" });
            context.Categories.Add(new Category { CategoryId = 2, Name = "birds" });
            context.Categories.Add(new Category { CategoryId = 3, Name = "otherCategory" });
            context.SaveChanges();
            var repository = new MyRepository(context);
            string[] Catagories = { "mamels", "birds", "otherCategory" };
            // Act
            var result = repository.GetCatogry();

            // Assert
            foreach (var category in result)
                Assert.AreEqual(category.Name, Catagories[category.CategoryId - 1]);
        }
        [TestMethod]
        public void MyRepository_EditAnimal_ChangeAnimalDetails()
        {
            // Arrange
            using var context = new ZooContext(_options);
            context.Animals.Add(new Animal { AnimalId = 50, Name = "dog", CategoryId = 3, Description = "cool Dog" });
            context.SaveChanges();
            var repository = new MyRepository(context);
            var EditAnimal = new Animal { AnimalId = 50, Name = "dog", CategoryId = 3, Description = "very cool Dog" }; //description changed!!

            // Act
            repository.EditAnimal(EditAnimal);
            var result = repository.GetAnimalById(50);

            // Assert
            Assert.AreEqual(result.Description, "very cool Dog");
        }
        [TestMethod]
        public void MyRepository_InsertAnimal_InsertNewAnimalToDB()
        {
            // Arrange
            using var context = new ZooContext(_options);
            var newAnimal = new Animal { AnimalId = 50, Name = "dog", CategoryId = 3, Description = "cool Dog" };
            var repository = new MyRepository(context);
            // Act
            repository.InsertAnimal(newAnimal);
            var newAnimalFromDB = repository.GetAnimalById(50);
            // Assert
            Assert.AreEqual(newAnimalFromDB, newAnimal);
        }
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void MyRepository_DeleteAnimal_DeleteAnimalFromDB()
        {
            // Arrange
            using var context = new ZooContext(_options);
            context.Animals.Add(new Animal { AnimalId = 50, Name = "dog", CategoryId = 3, Description = "cool Dog" });
            context.SaveChanges();
            var repository = new MyRepository(context);
            // Act
            repository.DeleteAnimal(50);
            var newAnimalFromDB = context.Animals.Single(c => c.AnimalId == 50);

            // Assert
            Assert.AreEqual(newAnimalFromDB, null);
        }
        [TestMethod]
        public void MyRepository_GetAnimals_ListOfAllAnimals()
        {
            // Arrange
            using var context = new ZooContext(_options);
            var FirstAnimal = new Animal { AnimalId = 50, Name = "dog", CategoryId = 1, Description = "cool Dog" };
            var SecondAnimal = new Animal { AnimalId = 51, Name = "cat", CategoryId = 1, Description = "cool cat" };
            var ThirdAnimal = new Animal { AnimalId = 52, Name = "cool bird", CategoryId = 2, Description = "vary cool bird" };
            var listOfAnimals = new List<Animal>() { FirstAnimal, SecondAnimal, ThirdAnimal };
            listOfAnimals.ForEach(animal => context.Animals.Add(animal));
            context.SaveChanges();
            var repository = new MyRepository(context);
            // Act
            var listOfAnimalsFromDB = repository.GetAnimals().ToList();
            // Assert
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(listOfAnimalsFromDB[i], listOfAnimals[i]);
            }
        }
        [TestMethod]
        public void MyRepository_AddNewComment_AddNewCommentlToDB()
        {
            // Arrange
            using var context = new ZooContext(_options);
            context.Animals.Add(new Animal { AnimalId = 50, Name = "dog" });
            context.SaveChanges();
            var repository = new MyRepository(context);
            // Act
            repository.AddNewComment(new Comment { CommentId = 50, AnimalId = 50, CommentString = "cool Dog" });
            var newAnimalFromDB = repository.GetAnimalById(50);
            // Assert
            Assert.AreEqual(newAnimalFromDB.Comments[0].CommentString, "cool Dog");
        }
       




    }
}
