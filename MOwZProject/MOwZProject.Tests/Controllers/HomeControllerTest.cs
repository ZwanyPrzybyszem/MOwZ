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
        /*
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
        */

        [TestMethod]
        public void Still00()
        {

            HomeController controller = new HomeController();
            var privateObject = new PrivateObject(controller);
            List<int> result = (List<int>)privateObject.Invoke("getResult", 2, 2, new int[2] { 5, 5 });
            List<int> temp = new List<int> { 0, 1 };

            Assert.AreEqual(result.ElementAt(1), temp.ElementAt(1));
        }

        [TestMethod]
        public void Still01()
        {
            HomeController controller = new HomeController();
            var privateObject = new PrivateObject(controller);
            List<int> result = (List<int>)privateObject.Invoke("getResult", 3, 5, new int[3] { 7270, 1230, 2220 });
            List<int> temp = new List<int> { 0, 0 , 0, 0, 2 };

            for (int i = 0; i < temp.Count; i++)
            {
                Assert.AreEqual(result.ElementAt(i), temp.ElementAt(i));
            }
        }

        [TestMethod]
        public void Still02()
        {
            HomeController controller = new HomeController();
            var privateObject = new PrivateObject(controller);
            List<int> result = (List<int>)privateObject.Invoke("getResult", 7, 15, new int[7] { 1850, 2560, 6000, 4342, 1849, 2341, 5555 });
            List<int> temp = new List<int> { 2, 6, 3, 2, 6, 1, 3, 2, 5, 6, 2, 0, 4, 3, 6 };

            for (int i = 0; i < temp.Count; i++)
            {
                Assert.AreEqual(result.ElementAt(i), temp.ElementAt(i));
            }
        }

        [TestMethod]
        public void Still03()
        {
            HomeController controller = new HomeController();
            var privateObject = new PrivateObject(controller);
            List<int> result = (List<int>)privateObject.Invoke("getResult", 3, 10, new int[3] { 500, 1500, 1500 });
            List<int> temp = new List<int> { 1, 2, 1, 2, 1, 2, 0, 1, 2, 1 };

            for (int i = 0; i < temp.Count; i++)
            {
                Assert.AreEqual(result.ElementAt(i), temp.ElementAt(i));
            }
        }

        [TestMethod]
        public void Still04()
        {
            HomeController controller = new HomeController();
            var privateObject = new PrivateObject(controller);
            try 
            { 
                List<int> result = (List<int>)privateObject.Invoke("getResult", 5, 5, new int[5] { 0, 0, 10, 0, 0 });
                Assert.Fail();
            }
            catch (Exception) { }
            
        }

        [TestMethod]
        public void Still05()
        {   //Pytanie, czy można przydzielić więcej mandatów niż obywateli w stanie?
            HomeController controller = new HomeController();
            var privateObject = new PrivateObject(controller);
            try
            {
                List<int> result = (List<int>)privateObject.Invoke("getResult", 1, 2, new int[1] { 1 });
                Assert.Fail();
            }
            catch (Exception) { }
            /*List<int> temp = new List<int> { 0, 0 };

            for (int i = 0; i < temp.Count; i++)
            {
                Assert.AreEqual(result.ElementAt(i), temp.ElementAt(i));
            }*/
        }

        [TestMethod]
        public void Still06()
        {
            HomeController controller = new HomeController();
            var privateObject = new PrivateObject(controller);
            List<int> result = (List<int>)privateObject.Invoke("getResult", 5, 2, new int[5] { 2, 2, 2, 3, 2 });
            List<int> temp = new List<int> { 3, 0 };

            for (int i = 0; i < temp.Count; i++)
            {
                Assert.AreEqual(result.ElementAt(i), temp.ElementAt(i));
            }
        }

        [TestMethod]
        public void Still07()
        {
            HomeController controller = new HomeController();
            var privateObject = new PrivateObject(controller);
            List<int> result = (List<int>)privateObject.Invoke("getResult", 5, 5, new int[5] { 1, 2, 1, 1, 4 });
            List<int> temp = new List<int> { 4, 4, 1, 4, 4 };

            for (int i = 0; i < temp.Count; i++)
            {
                Assert.AreEqual(result.ElementAt(i), temp.ElementAt(i));
            }
        }

        [TestMethod]
        public void Still08()
        {
            HomeController controller = new HomeController();
            var privateObject = new PrivateObject(controller);
            List<int> result = (List<int>)privateObject.Invoke("getResult", 16, 100, new int[16] { 2914362, 2096404, 2165651, 1023317, 2524651, 3354077, 5301760, 1010203, 2129951, 1198690, 2290070, 4615870, 1273995, 1450697, 3462196, 1721405 });
            List<int> temp = new List<int> { 6, 11, 14, 5, 6, 0, 11, 4, 10, 2, 6, 8, 1, 14, 5, 11, 15, 0, 6, 11, 4, 13, 14, 5, 6, 10, 12, 2, 8, 1, 9, 11, 0, 6, 14, 5, 4, 3, 7, 11, 6, 15, 10, 0, 14, 2, 6, 11, 8, 5, 1, 13, 4, 6, 11, 14, 0, 12, 5, 10, 6, 15, 9, 11, 2, 8, 1, 14, 6, 4, 0, 5, 11, 6, 13, 10, 3, 7, 14, 11, 2, 5, 6, 4, 0, 8, 15, 1, 11, 12, 6, 14, 5, 10, 9, 0, 6, 11, 4, 2 };

            for (int i = 0; i < temp.Count; i++)
            {
                Assert.AreEqual(result.ElementAt(i), temp.ElementAt(i));
            }
        }

        [TestMethod]
        public void Still09()
        {
            HomeController controller = new HomeController();
            var privateObject = new PrivateObject(controller);
            List<int> result = (List<int>)privateObject.Invoke("getResult", 9, 5, new int[9] { 1250, 1000, 750, 1500, 1750, 2000, 500, 1500, 1000 });
            List<int> temp = new List<int> { 5, 4, 3, 7, 0 };

            for (int i = 0; i < temp.Count; i++)
            {
                Assert.AreEqual(result.ElementAt(i), temp.ElementAt(i));
            }
        }

        [TestMethod]
        public void Still10()
        {
            HomeController controller = new HomeController();
            var privateObject = new PrivateObject(controller);
            List<int> result = (List<int>)privateObject.Invoke("getResult", 12, 4, new int[12] { 25, 10, 75, 15, 17, 20, 50, 50, 10, 40, 30, 20 });
            List<int> temp = new List<int> { 2, 6, 7, 2 };

            for (int i = 0; i < temp.Count; i++)
            {
                Assert.AreEqual(result.ElementAt(i), temp.ElementAt(i));
            }
        }

    }
}
