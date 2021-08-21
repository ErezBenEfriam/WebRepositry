using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApplication1.Controllers;
using WebApplication1.Repositories;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace TestProject1
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IRepository> _repository;
      
        [TestMethod]
        public void Index_ReturnsAViewResult()
        {
            // Arrange
            var firstCommennt = new Comment() { AnimalId = 51, CommentId = 1 };
            var secondCommennt = new Comment() { AnimalId = 51, CommentId = 2 };
            var thirdCommennt = new Comment() { AnimalId = 50, CommentId = 3 };
            var firstlistofComments =new  List<Comment>(){ firstCommennt,secondCommennt };
            var secondlistofComments = new  List<Comment>(){ thirdCommennt };

            var firstAnimal = new Animal() { AnimalId = 50, Name = "dog"  , Comments = firstlistofComments };
            var secondAnimal = new Animal() { AnimalId = 51, Name = "cat", Comments = secondlistofComments };

            var listForRepository = new List<Animal>() { firstAnimal, secondAnimal };
            _repository.Setup(repo => repo.GetAnimals()).Returns(listForRepository);

            var controller = new HomeController(_repository.Object);

            // Act
            var result = controller.Index();
            var MyViewResult = controller.Index() as ViewResult;
            var mymodelResultList = (List<Animal>)MyViewResult.Model;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(mymodelResultList[i],listForRepository[i]);
            }
        }
    }
}
