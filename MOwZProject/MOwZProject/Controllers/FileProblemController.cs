using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MOwZProject.Models;

namespace MOwZProject.Controllers
{
    /// <summary>
    /// Kontroler dla problemów z pliku.
    /// </summary>
    public class FileProblemController : Controller
    {
        /// <summary>
        /// GET dla wczytywania pliku.
        /// </summary>
        /// <returns>Wynik metody akcji.</returns>
        public ActionResult FileProblem()
        {
            return View(new FileProblem());
        }



        /// <summary>
        /// Metoda odpowiedzialna za przetworzenie pliku z problemem.
        /// </summary>
        /// <param name="model">Obiekt z zawartością pliku oraz obiekt reprezentujący problem.</param>
        /// <returns>Wynik metody akcji.</returns>
        [HttpPost]
        public ActionResult FileProblem(FileProblem model)
        {
           
            try
            {
                if (model.FileWithData == null)
                {
                    throw new Exception("Brak pliku!");
                }
                model.updateProblem();
                model.ProblemFromFile.getStillResult();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "ERROR:" + ex.Message.ToString();
            }
            return View(model);
        }
        
    }
}
