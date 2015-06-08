using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MOwZProject.Models;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Drawing;

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
                    Problem.States.Add(new State() { Id = Problem.States.Count });
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
                        s.Id = i;

                        if (s.Name == null || s.Name.Length < 0)
                        {
                            s.Name = (s.Id + 1).ToString();
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


        /// <summary>
        /// GET dla formularza.
        /// </summary>
        /// <returns>Wynik metody akcji.</returns>
        public ActionResult ProblemLiu()
        {
            return View(new ProblemLiu());
        }



        /// <summary>
        /// Metoda dodaje nowe zadanie bądź przetwarza problem z formularza.
        /// </summary>
        /// <param name="Problem">Obiekt reprezentujący problem do przetworzenia.</param>
        /// <param name="add">Zawiera dane, gdy wybrano opcję dodania nowego zadania.</param>
        /// <param name="process">Zawiera dane, gdy wybrano opcję rozwiązania problemu.</param>
        /// <returns>Wynik metody akcji.</returns>
        [HttpPost]
        public ActionResult ProblemLiu(ProblemLiu Problem, string add, string process)
        {
            try
            {
                ViewBag.Message = "";
                if (add != null)
                {
                    Problem.Tasks.Add(new Task() { Id = Problem.Tasks.Count });
                }
                else if (process != null)
                {
                    int i = 0;
                    foreach (var s in Problem.Tasks)
                    {
                        s.Id = i + 1;

                        i++;

                    }
                    Problem.getLiuResult();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "ERROR:" + ex.Message.ToString();
            }
            return View(Problem);
        }



        /// <summary>
        /// Metoda tworzy wykres Gantta.
        /// </summary>
        /// <returns>Wynik metody akcji.</returns>
        public ActionResult GanttChart(string tasks, string starts, string stops, string color)
        {
            int[] startsTab = Array.ConvertAll(starts.Split(' '), Int32.Parse);
            int[] stopsTab = Array.ConvertAll(stops.Split(' '), Int32.Parse);
            int min = 0;

            for (int i = 0; i< startsTab.Length; i++) 
            {
                if (min == 0 || min > (stopsTab[i] - startsTab[i]))
                {
                    min = (stopsTab[i] - startsTab[i]);
                }
            }

            Chart chart = new Chart();
            chart.ChartAreas.Add(new ChartArea());
            chart.Width = ((stopsTab.Max() / min) * 40) > 1200 ? 1200 : ((stopsTab.Max() / min) * 40) ;
            chart.Height = Array.ConvertAll(tasks.Split(' '), Int32.Parse).Length * 8 < 20 ? 20 : Array.ConvertAll(tasks.Split(' '), Int32.Parse).Length * 8;
            chart.Series.Add(new Series("Data"));
            chart.Series["Data"].ChartType = SeriesChartType.RangeBar;
            if (color == "red")
            {
                chart.Series["Data"].Color = Color.Red;
            }
            chart.Series["Data"].XValueType = ChartValueType.Int32;
            chart.Series["Data"].YValueType = ChartValueType.String;

            for (int i = 0; i < tasks.Length; i++)
            {
                chart.Series["Data"].Points.DataBindXY(
                Array.ConvertAll(tasks.Split(' '), Int32.Parse),
                startsTab,
                stopsTab);
            }

            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms, ChartImageFormat.Png);
            return File(ms.ToArray(), "image/png");
        }


    }
}
