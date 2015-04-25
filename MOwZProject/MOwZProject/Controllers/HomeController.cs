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
    public class HomeController : Controller
    {
        /// <summary>
        /// GET dla strony głównej
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// GET dla strony o autorach
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }





        //TODO zmiana stringa
        /// <summary>
        /// 
        /// </summary>
        /// <param name="names">Nazwy stanów</param>
        /// <param name="places">Liczby przydzielonych mandatów poszczególnym stanom</param>
        /// <returns></returns>
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