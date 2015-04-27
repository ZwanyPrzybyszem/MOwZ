using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOwZProject;
using MOwZProject.Controllers; //do usuniecia!!!!!!!!!!!!!!!
using MOwZProject.Models;

namespace MOwZProject.Tests.Controllers
{
    /// <summary>
    /// Kontroler do testów.
    /// </summary>
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



        /// <summary>
        /// Metoda przetwarza problem.
        /// </summary>
        /// <param name="tab">Liczności poszczególnych stanów.</param>
        /// <param name="size">Rozmiar parlamentu.</param>
        /// <returns>Problem z wygenerowanymi wynikami.</returns>
        private Problem check(int[] tab, int size)
        {
            Problem p = new Problem();
            p.ParlamentSize = size;



            int i = 0;
            foreach (int t in tab)
            {
                p.States.Add(new State { Name = (i + 1).ToString(), id = i, Mandats = 0, Size = t });
                i++;
            }

            p.getStillResult();

            return p;
        }



        /// <summary>
        /// Przykładowy test.
        /// </summary>
        [TestMethod]
        public void StillTestNew()
        {
            int[] tab = new int[2] { 10, 5 };
            int[] res = new int[2] { 2, 1 };

            Problem p = check(tab, 3);

            for (int i =0; i< res.Length; i++)
            {
                Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
            }
        }

        [TestMethod]
        public void Still00()
        {
            int[] tab = new int[2] { 5, 5 };
            int[] res = new int[2] { 1, 1 };

            Problem p = check(tab, 2);

            for (int i = 0; i < res.Length; i++)
            {
                Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
            }
        }

        [TestMethod]
        public void Still01()
        {
            int[] tab = new int[3] { 7270, 1230, 2220 };
            int[] res = new int[3] { 4, 0, 1 };

            Problem p = check(tab, 5);

            for (int i = 0; i < res.Length; i++)
            {
                Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
            }
        }

        [TestMethod]
        public void Still02()
        {
            int[] tab = new int[7] { 1850, 2560, 6000, 4342, 1849, 2341, 5555 };
            int[] res = new int[7] { 1, 1, 4, 3, 1, 1, 4 };

            Problem p = check(tab, 15);

            for (int i = 0; i < res.Length; i++)
            {
                Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
            }
        }

        [TestMethod]
        public void Still03()
        {
            int[] tab = new int[3] { 500, 1500, 1500 };
            int[] res = new int[3] { 1, 5, 4 };

            Problem p = check(tab, 10);

            for (int i = 0; i < res.Length; i++)
            {
                Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
            }
        }

        [TestMethod]
        public void Still04()
        {
            int[] tab = new int[5] { 0, 0, 10, 0, 0 };
            int[] res = new int[5] { -1, -1, -1, -1, -1 };

            try { 
                Problem p = check(tab, 5);

                for (int i = 0; i < res.Length; i++)
                {
                    Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
                }
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void Still05()
        {   //Pytanie, czy można przydzielić więcej mandatów niż obywateli w stanie?
            int[] tab = new int[1] { 1 };
            int[] res = new int[1] { 2 };

            try
            {
                Problem p = check(tab, 2);

                for (int i = 0; i < res.Length; i++)
                {
                    Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
                }
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void Still06()
        {
            int[] tab = new int[5] { 2, 2, 2, 3, 2 };
            int[] res = new int[5] { 1, 0, 0, 1, 0 };

            Problem p = check(tab, 2);

            for (int i = 0; i < res.Length; i++)
            {
                Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
            }
        }

        [TestMethod]
        public void Still07()
        {
            int[] tab = new int[5] { 1, 2, 1, 1, 4 };
            int[] res = new int[5] { 0, 1, 0, 0, 4 };

            Problem p = check(tab, 5);

            for (int i = 0; i < res.Length; i++)
            {
                Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
            }
        }

        [TestMethod]
        public void Still08()
        {
            int[] tab = new int[16] { 2914362, 2096404, 2165651, 1023317, 2524651, 3354077, 5301760, 1010203, 2129951, 1198690, 2290070, 4615870, 1273995, 1450697, 3462196, 1721405 };
            int[] res = new int[16] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, };
            
            try
            {
                Problem p = check(tab, 100);

                for (int i = 0; i < res.Length; i++)
                {
                    Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
                }
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void Still09()
        {
            int[] tab = new int[9] { 1250, 1000, 750, 1500, 1750, 2000, 500, 1500, 1000 };
            int[] res = new int[9] { 1, 0, 0, 1, 1, 1, 0, 1, 0 };

            Problem p = check(tab, 5);

            for (int i = 0; i < res.Length; i++)
            {
                Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
            }
        }

        [TestMethod]
        public void Still10()
        {
            int[] tab = new int[12] { 25, 10, 75, 15, 17, 20, 50, 50, 10, 40, 30, 20 };
            int[] res = new int[12] { 0, 0, 2, 0, 0, 0, 1, 1, 0, 0, 0, 0 };

            Problem p = check(tab, 4);

            for (int i = 0; i < res.Length; i++)
            {
                Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
            }
        }

    }
}
