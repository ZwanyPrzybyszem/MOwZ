using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MOwZProject.Models;

namespace MOwZProject.Controllers
{
    public class FileProblemController : Controller
    {
        /// <summary>
        /// GET dla wczytywania pliku
        /// </summary>
        /// <returns></returns>
        public ActionResult FileProblem()
        {
            return View(new FileProblem());
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">Obiekt zawierający zawartość pliku oraz obiekt reprezentujący problem</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FileProblem(FileProblem model)
        {
            try
            {
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
