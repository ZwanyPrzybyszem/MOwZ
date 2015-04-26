using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MOwZProject.Models;
using System.Web.UI.DataVisualization.Charting;
using System.IO;

namespace MOwZProject.Controllers
{
    /// <summary>
    /// Kontroler dla problemów z formularza.
    /// </summary>
    public class ProblemController : Controller
    {

        /// <summary>
        /// GET dla formularza.
        /// </summary>
        /// <returns>Wynik metody akcji.</returns>
        public ActionResult Problem()
        {
            return View(new Problem());
        }



        /// <summary>
        /// Metoda dodaje nowy stan bądź przetwarza problem z formularza.
        /// </summary>
        /// <param name="Problem">Obiekt reprezentujący problem do przetworzenia.</param>
        /// <param name="add">Zawiera dane, gdy wybrano opcję dodania nowego stanu.</param>
        /// <param name="process">Zawiera dane, gdy wybrano opcję rozwiązania problemu.</param>
        /// <returns>Wynik metody akcji.</returns>
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
                else if (process != null)
                {
                    if (Problem.ParlamentSize < 1)
                    {
                        throw new Exception(String.Format("Wpisz odpowiednią liczbę (> 0) miejsc do przydziału!"));
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
                            throw new Exception(String.Format("Niepoprawne dane opisujące stany! Uzupełnij rozmiar stanu (liczba > 0) odpowiednimi danymi!"));
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

            chart.ChartAreas[0].Area3DStyle.Enable3D = true;

            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms, ChartImageFormat.Png);
            return File(ms.ToArray(), "image/png");

        }


    }
}
