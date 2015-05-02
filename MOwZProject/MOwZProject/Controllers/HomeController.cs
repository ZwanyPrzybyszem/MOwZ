using MOwZProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;

namespace MOwZProject.Controllers
{
    /// <summary>
    /// Kontroler główny.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// GET dla strony głównej.
        /// </summary>
        /// <returns>Wynik metody akcji.</returns>
        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// GET dla strony o autorach.
        /// </summary>
        /// <returns>Wynik metody akcji.</returns>
        public ActionResult About()
        {
            return View();
        }



        



    }
}