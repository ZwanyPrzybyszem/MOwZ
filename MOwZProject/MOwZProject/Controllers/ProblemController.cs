using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MOwZProject.Models;

namespace MOwZProject.Controllers
{
    public class ProblemController : Controller
    {

        /// <summary>
        /// GET dla formularza
        /// </summary>
        /// <returns></returns>
        public ActionResult Problem()
        {
            return View(new Problem());
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Problem">Obiekt reprezentujący problem do przetworzenia</param>
        /// <param name="add">Zawiera dane, gdy wybrano opcję dodania nowego stanu</param>
        /// <param name="process">Zawiera dane, gdy wybrano opcję rozwiązania problemu</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Problem(Problem Problem, string add, string process)
        {
            try
            {
                ViewBag.Message = "";
                if (add != null)
                {
                    Problem.States.Add(new State() { id = Problem.States.Count });
                }
                else
                {
                    if (Problem.ParlamentSize < 1)
                    {
                        throw new Exception(String.Format("Wpisz odpowiednią liczbę miejsc do przydziału! "));
                    }
                    int i = 0;
                    foreach (var s in Problem.States)
                    {
                        s.id = i;

                        if (s.Name == null || s.Name.Length < 0)
                        {
                            s.Name = (s.id + 1).ToString();
                        }

                        if (s.Size < 1)
                        {
                            throw new Exception(String.Format("Niepoprawne dane opisujące stany! Uzupełnij rozmiar stanu (liczba) odpowiednimi danymi!"));
                        }
                        i++;

                    }
                    Problem.getStillResult();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "ERROR:" + ex.Message.ToString();
            }
            return View(Problem);
        }



    }
}
