using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace TestProject1.ControllersTest
{
    [TestClass]
    public class CatalogControllerTest
    {
        private Mock<IRepository> _repository;
        [TestInitialize]
        public void init()
        {
            _repository = new Mock<IRepository>();
        }

        [TestMethod]
        public void CatalogAction_retunrCatalogOfChosenCategory()
        {
            // Arrange
            var firstAnimal = new Animal() { AnimalId = 50, Name = "dog", CategoryId = 1 };
            var secondAnimal = new Animal() { AnimalId = 51, Name = "cat", CategoryId = 1 };
            var listForRepository = new List<Animal>() { firstAnimal, secondAnimal };
            _repository.Setup(repo => repo.GetAnimals()).Returns(listForRepository);
            var controller = new CatalogController(_repository.Object);

            // Act
            var result = controller.CatalogAction();
            var MyViewResult = controller.CatalogAction() as ViewResult;
            var mymodelResultList = (List<Animal>)MyViewResult.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(mymodelResultList[i], listForRepository[i]);
            }
        }
        [TestMethod]
        public void CatalogForAdm_retunrCatalogWithIsAdminEqualTrue()
        {
            // Arrange
            var controller = new CatalogController(_repository.Object);

            // Act
            var result = (RedirectToActionResult)controller.CatalogForAdm();
           var isAdminParamertName = result.RouteValues.Keys.ToList().Last(); //becuase first parameter is id
           var isAdminValue = result.RouteValues.Values.ToList().Last();

            //Assert
            Assert.AreEqual("isAdmin", isAdminParamertName);
            Assert.AreEqual(true, isAdminValue);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void AnimalPage_retunrViewWithAnimalId()
        {
            // Arrange
            var controller = new CatalogController(_repository.Object);
            _repository.Setup(repo => repo.GetAnimalById(1)).Returns(new Animal() { AnimalId = 50, Name = "dog" });
            // Act
            var result = controller.AnimalPage(1);
            var MyViewResult = controller.AnimalPage(1) as ViewResult;
            var mymodelResultAnimal =(Animal)MyViewResult.Model;
            //Assert
            Assert.AreEqual(50, mymodelResultAnimal.AnimalId);
            Assert.AreEqual("dog", mymodelResultAnimal.Name);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void AddNewComment_AddNewCommentFromAnimalPage()
        {
            // Arrange
            var controller = new CatalogController(_repository.Object);
       
            // Act
            var result = (RedirectToActionResult)controller.AddNewComment("newComment", 50);
            var paramaetrName  = result.RouteValues.Keys.ToList().First(); //id is the only parmeter
            var ActionName = result.ActionName; 
            var idValue = result.RouteValues.Values.ToList().First();

            //Assert
            Assert.AreEqual(50, idValue);
            Assert.AreEqual("id", paramaetrName);
            Assert.AreEqual("AnimalPage", ActionName); //redirect To this action
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

    }
}
