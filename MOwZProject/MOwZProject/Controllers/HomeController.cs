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



        /// <summary>
        /// Metoda tworzy wykres Gantta.
        /// </summary>
        /// <returns>Wynik metody akcji.</returns>
        public ActionResult GanttChart()
        {
            Chart chart = new Chart();
            chart.ChartAreas.Add(new ChartArea());

            chart.Series.Add(new Series("Data"));
            chart.Series["Data"].ChartType = SeriesChartType.RangeBar;
            chart.Series["Data"].XValueType = ChartValueType.Int32;
            chart.Series["Data"].YValueType = ChartValueType.String;
            
            chart.Series["Data"].Points.AddXY(1, 1, 2);
            chart.Series["Data"].Points.AddXY(1, 3, 5);
            chart.Series["Data"].Points.AddXY(2, 1, 4);

            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms, ChartImageFormat.Png);
            return File(ms.ToArray(), "image/png");
        }



    }
}