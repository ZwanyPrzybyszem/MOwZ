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
        /// Metoda tworzy wykres kołowy.
        /// </summary>
        /// <param name="names">Nazwy dla poszczególnych części wykresu (np. nazwy stanów).</param>
        /// <param name="places">Liczby repreznetujące wielkość poszczególnych częsci wykresu (np. liczba przydzielonych mandatów).</param>
        /// <returns>Wynik metody akcji.</returns>
        public ActionResult EfficiencyChart(string names, string places)
        {
            Chart chart = new Chart();
            chart.ChartAreas.Add(new ChartArea());

            chart.Series.Add(new Series("Data"));
            chart.Series["Data"].ChartType = SeriesChartType.Pie;
            chart.Series["Data"]["PieLabelStyle"] = "Outside";
            chart.Series["Data"]["PieLineColor"] = "Black";
            chart.Series["Data"].Points.DataBindXY(
                names.Split(' '),
                Array.ConvertAll(places.Split(' '), Int32.Parse));

            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms, ChartImageFormat.Png);
            return File(ms.ToArray(), "image/png");
        }


    }
}