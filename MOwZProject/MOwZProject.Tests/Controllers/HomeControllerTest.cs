using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOwZProject;
using MOwZProject.Controllers;

namespace MOwZProject.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Still()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Still() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Still2()
        {

            HomeController controller = new HomeController();
            var privateObject = new PrivateObject(controller);
            List<int> result = (List<int>)privateObject.Invoke("getResult", 2, 2, new int[2] { 5, 5 });
            List<int> temp = new List<int> { 0, 1 };

            Assert.AreEqual(result.ElementAt(1), temp.ElementAt(1));
        }
    }
}
