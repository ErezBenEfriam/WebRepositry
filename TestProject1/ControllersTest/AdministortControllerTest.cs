using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace TestProject1.ControllersTest
{
    [TestClass]
   public class AdministortControllerTest
    {
        private Mock<IRepository> _repository;
        [TestInitialize]
        public void init()
        {
            _repository = new Mock<IRepository>();
        }
        [TestMethod]
        public void DeltetAnimal_RedierctToCatalogAction()
        {
            //Arrange
            AdministorController controller = new AdministorController(_repository.Object);

            //Act
            var result = (RedirectToActionResult)controller.DeltetAnimal(1, 3);
            var IdparamaetrName = result.RouteValues.Keys.ToList().First(); //id is the First parmeter
            var IsAdminparamaetrName = result.RouteValues.Keys.ToList().Last(); //IsAdmin is the Last parmeter
            var ActionName = result.ActionName;
            var ControllerName = result.ControllerName;
            var catrogtyidValue = result.RouteValues.Values.ToList().First();
            var IsAdminValue = result.RouteValues.Values.ToList().Last();

            //Asset
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.IsTrue((bool)IsAdminValue);
            Assert.AreEqual(catrogtyidValue, 3);
            Assert.AreEqual(ActionName, "CatalogAction");
            Assert.AreEqual(ControllerName, "Catalog");
            Assert.AreEqual(IdparamaetrName, "id");
            Assert.AreEqual(IsAdminparamaetrName, "isAdmin");
        }
        [TestMethod]
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        public void EditAnimalPage_GettingViewOfCreateOrEditNewAnimal(int id)
        {
            //Arrange
            AdministorController controller = new AdministorController(_repository.Object);

            //Act
            var result = (ViewResult)controller.EditAnimalPage(id);
            var listOFValues = result.ViewData.Values.ToList();
            //Assert
            if (id == 0) //in case of create a new animal - fileds are empty
            {
                Assert.AreEqual("Create new Animal", listOFValues[0]);
                Assert.IsTrue((bool)listOFValues[1]);
            }
            else //in case of edit an exsit animal - fileds are fill with it info
            {
                Assert.AreEqual("Edit Animal", listOFValues[0]);
                Assert.IsFalse((bool)listOFValues[1]);
            }
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void EditAnimal_CreateOrEditAnimal()
        {
            //Arrange
            AdministorController controller = new AdministorController(_repository.Object);
               Animal ValidAnimal = new Animal() { AnimalId = 50, Name = "Dog", PictureName = "pic", Description = "coolDog", CategoryId = 1 };


            //Act
            var resultValid = (RedirectToActionResult)controller.EditAnimal(ValidAnimal);
            var ActionName = resultValid.ActionName;
            var ControllerName = resultValid.ControllerName;
            var parmaeterValue = resultValid.RouteValues.Values.First();


            //Assert
            Assert.AreEqual(ActionName, "AnimalPage");
            Assert.AreEqual(ControllerName, "Catalog");
            Assert.AreEqual(ControllerName, "Catalog");
            Assert.AreEqual(parmaeterValue, ValidAnimal.AnimalId);
            Assert.IsInstanceOfType(resultValid, typeof(RedirectToActionResult));
        
        }
    }
}
