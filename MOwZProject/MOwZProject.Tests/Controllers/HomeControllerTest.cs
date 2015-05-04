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

        /// <summary>
        /// Metoda przetwarza problem.
        /// </summary>
        /// <param name="tab">Liczności poszczególnych stanów.</param>
        /// <param name="size">Rozmiar parlamentu.</param>
        /// <returns>Problem z wygenerowanymi wynikami.</returns>
        private Problem checkStill(int[] tab, int size)
        {
            Problem p = new Problem();
            p.ParlamentSize = size;
            p.States.Clear();

            int i = 0;
            foreach (int t in tab)
            {
                p.States.Add(new State { Name = (i + 1).ToString(), Id = i, Mandats = 0, Size = t });
                i++;
            }

            p.getStillResult();

            return p;
        }

        /// <summary>
        /// Metoda przetwarza problem.
        /// </summary>
        /// <param name="durations">Czasy trwania poszczególnych zadań.</param>
        /// <param name="periods">Okresy wykonywania poszczególnych zadań.</param>
        /// <returns>Problem z wygenerowanymi wynikami.</returns>
        private ProblemLiu checkLiu(int[] durations, int[] periods, int size)
        {
            ProblemLiu p = new ProblemLiu();
            p.Tasks.Clear();

            for (int i = 0; i < size; i++)
            {
                p.Tasks.Add(new Task { Id = i, Duration = durations[i], Period = periods[i], TaskRemain = durations[i], CompletedTask = 0 });
            }

            p.getLiuResult();

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

            Problem p = checkStill(tab, 3);

            for (int i =0; i < res.Length; i++)
            {
                Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
            }
        }

        [TestMethod]
        public void Still00()
        {
            int[] tab = new int[2] { 5, 5 };
            int[] res = new int[2] { 1, 1 };

            Problem p = checkStill(tab, 2);

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

            Problem p = checkStill(tab, 5);

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

            Problem p = checkStill(tab, 15);

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

            Problem p = checkStill(tab, 10);

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
                Problem p = checkStill(tab, 5);

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
                Problem p = checkStill(tab, 2);

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

            Problem p = checkStill(tab, 2);

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

            Problem p = checkStill(tab, 5);

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
                Problem p = checkStill(tab, 100);

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

            Problem p = checkStill(tab, 5);

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

            Problem p = checkStill(tab, 4);

            for (int i = 0; i < res.Length; i++)
            {
                Assert.AreEqual(p.States.ElementAt(i).Mandats, res[i]);
            }
        }



        /// <summary>
        /// Przykładowy test.
        /// </summary>
        [TestMethod]
        public void LiuTestNew()
        {
            const int size = 2;

            int[] dur = new int[size] { 1, 2 };
            int[] per = new int[size] { 3, 3 };

            int[] resultIter = new int[] { 0, 1 };

            ProblemLiu p = checkLiu(dur, per, size);

            for (int i = 0; i < p.Iterations.Count(); i++)
            {
                Assert.AreEqual(p.Iterations.ElementAt(i).Task.Id, resultIter[i]);
            }
        }

        [TestMethod]
        public void Liu00()
        {
            const int size = 2;

            int[] dur = new int[size] { 2, 5 };
            int[] per = new int[size] { 4, 10 };

            int[] resultIter = new int[] { };
            try
            { 
                ProblemLiu p = checkLiu(dur, per, size);
                for (int i = 0; i < p.Iterations.Count(); i++)
                {
                    Assert.AreEqual(p.Iterations.ElementAt(i).Task.Id, resultIter[i]);
                }
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void Liu01()
        {
            const int size = 4;

            int[] dur = new int[size] { 3, 2, 1, 3 };
            int[] per = new int[size] { 9, 9, 9, 9 };

            int[] resultIter = new int[] { 0, 1, 2, 3 };

            ProblemLiu p = checkLiu(dur, per, size);

            for (int i = 0; i < p.Iterations.Count(); i++)
            {
                Assert.AreEqual(p.Iterations.ElementAt(i).Task.Id, resultIter[i]);
            }
        }

        [TestMethod]
        public void Liu02()
        {
            const int size = 2;

            int[] dur = new int[size] { 1, 2 };
            int[] per = new int[size] { 3, 6 };

            int[] resultIter = new int[] { 0, 1, 0 };

            ProblemLiu p = checkLiu(dur, per, size);

            for (int i = 0; i < p.Iterations.Count(); i++)
            {
                Assert.AreEqual(p.Iterations.ElementAt(i).Task.Id, resultIter[i]);
            }
        }

        [TestMethod]
        public void Liu03()
        {
            const int size = 1;

            int[] dur = new int[size] { 10 };
            int[] per = new int[size] { 8 };

            int[] resultIter = new int[] { };
            try
            {
                ProblemLiu p = checkLiu(dur, per, size);
                for (int i = 0; i < p.Iterations.Count(); i++)
                {
                    Assert.AreEqual(p.Iterations.ElementAt(i).Task.Id, resultIter[i]);
                }
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void Liu04()
        {
            const int size = 2;

            int[] dur = new int[size] { 10, 5 };
            int[] per = new int[size] { 20, 10 };

            int[] resultIter = new int[] { 1, 0, 1, 0 };

            ProblemLiu p = checkLiu(dur, per, size);

            for (int i = 0; i < p.Iterations.Count(); i++)
            {
                Assert.AreEqual(p.Iterations.ElementAt(i).Task.Id, resultIter[i]);
            }
        }

        [TestMethod]
        public void Liu05()
        {
            const int size = 2;

            int[] dur = new int[size] { 5, 0 };
            int[] per = new int[size] { 5, 0 };

            int[] resultIter = new int[] { };
            try
            {
                ProblemLiu p = checkLiu(dur, per, size);
                for (int i = 0; i < p.Iterations.Count(); i++)
                {
                    Assert.AreEqual(p.Iterations.ElementAt(i).Task.Id, resultIter[i]);
                }
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void Liu06()
        {
            const int size = 2;

            int[] dur = new int[size] { 1, 3 };
            int[] per = new int[size] { 5, 4 };

            int[] resultIter = new int[] { 1, 0, 1, 0, 1, 0, 1, 0, 1 };

            ProblemLiu p = checkLiu(dur, per, size);

            for (int i = 0; i < p.Iterations.Count(); i++)
            {
                Assert.AreEqual(p.Iterations.ElementAt(i).Task.Id, resultIter[i]);
            }
        }

        [TestMethod]
        public void Liu07()
        {
            const int size = 1;

            int[] dur = new int[size] { -3 };
            int[] per = new int[size] { -5 };

            int[] resultIter = new int[] { };
            try
            {
                ProblemLiu p = checkLiu(dur, per, size);
                for (int i = 0; i < p.Iterations.Count(); i++)
                {
                    Assert.AreEqual(p.Iterations.ElementAt(i).Task.Id, resultIter[i]);
                }
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void Liu08()
        {
            const int size = 2;

            int[] dur = new int[size] { int.Parse("444"), 111 };
            int[] per = new int[size] { int.Parse("555"), 111 };

            int[] resultIter = new int[] { };
            try
            {
                ProblemLiu p = checkLiu(dur, per, size);
                for (int i = 0; i < p.Iterations.Count(); i++)
                {
                    Assert.AreEqual(p.Iterations.ElementAt(i).Task.Id, resultIter[i]);
                }
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void Liu09()
        {
            const int size = 3;

            int[] dur = new int[size] { 5, 250, 300 };
            int[] per = new int[size] { 1000, 500, 900 };

            int[] resultIter = new int[] { 1, 2, 1, 2, 0, 2, 1, 2, 0, 1, 2, 1, 2, 0, 1, 2, 1, 2, 0, 1, 2, 1, 2, 0, 1, 2, 1, 2, 0, 2, 1, 2, 1, 0, 2, 1, 2, 1, 2, 1, 2, 0, 1, 2, 1, 2, 0 };

            ProblemLiu p = checkLiu(dur, per, size);

            for (int i = 0; i < p.Iterations.Count(); i++)
            {
                Assert.AreEqual(p.Iterations.ElementAt(i).Task.Id, resultIter[i]);
            }
        }

        [TestMethod]
        public void Liu10()
        {
            const int size = 2;

            int[] dur = new int[size] { 3, 1 };
            int[] per = new int[size] { 3, 3 };

            int[] resultIter = new int[] { 0, 1 };
            try
            {
                ProblemLiu p = checkLiu(dur, per, size);
                for (int i = 0; i < p.Iterations.Count(); i++)
                {
                    Assert.AreEqual(p.Iterations.ElementAt(i).Task.Id, resultIter[i]);
                }
                Assert.Fail();
            }
            catch (Exception) { }
        }

    }
}
