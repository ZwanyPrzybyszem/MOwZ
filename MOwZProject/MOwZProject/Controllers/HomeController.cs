using MOwZProject.Models;//.Chart;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult ProblemForm()
        {
            ViewData["i"] = 1;
            return View(new Problem());
        }


        [HttpPost]
        public ActionResult ProblemForm(Problem Problem, List<State> States)
        {

            Problem.States = States;
            return View(Problem);
        }


        public ActionResult FileProblemForm()
        {
            return View(new FileProblem());
        }

        // POST: State/Create
        [HttpPost]
        public ActionResult FileProblemForm(FileProblem model)
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


        // GET: State/Create
        public ActionResult StateForm()
        {
            return PartialView("_StateForm");
        }

        // POST: State/Create
        [HttpPost]
        public ActionResult StateForm(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("ProblemForm");
            }
            catch
            {
                return View();
            }
        }

        

        //TODO zmiana stringa
        public ActionResult EfficiencyChart(string names, string places)
        {
            Chart chart = new Chart();
            chart.ChartAreas.Add(new ChartArea());

            //TODO legenda?

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


        /////////////////////////////////////////////////////////////////////////////////////
        /////////////////// PONIZEJ WSZYTSKO DO WYRZUCENIA //////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////
        // zostało przeniesione do obiektów //


        /// <summary>
        /// //////????????????????????????????????????
        /// </summary>
        /// <returns></returns>
        public ActionResult Chart()
        {
            return View();
        }




        public ActionResult Still()
        {
            ViewBag.Message = "  ";

            return View();
        }

        [HttpPost]
        public ActionResult Still(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                if (Path.GetExtension(file.FileName) == ".txt")
                {
                    try
                    {
                        //string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                        //file.SaveAs(path);

                        int[] words;
                        int[] p;


                        using (StreamReader reader = new StreamReader(file.InputStream))
                        {
                            words = Array.ConvertAll(reader.ReadLine().Split(new Char[] { ' ', '\t' }), Int32.Parse);

                            p = Array.ConvertAll(reader.ReadLine().Split(new Char[] { ' ', '\t' }), Int32.Parse);
                        }

                        if (words.Length == 2 && words[0] == p.Length && words.All(x => x > 0) && p.All(x => x > 0))
                        {

                            //process(words[0], words[1], p);
                            List<int> result = getResult(words[0], words[1], p);
                            ViewBag.Message = "Przetworzono pomyślnie.";

                            int[] places = new int[words[0]];
                            string[] names = new string[words[0]];
                            for (int i = 0; i < words[0]; i++)
                            {
                                places[i] = result.Count(x => x == i);
                                names[i] = i.ToString();
                            }

                            ViewData["iters"] = result;
                            ViewData["places"] = places;
                            ViewData["names"] = names;

                        }
                        else
                        {
                            throw new Exception("Niepoprawne dane!");
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                }
                else
                {
                    ViewBag.Message = "Niepoprawny format pliku.";
                }
            else
            {
                ViewBag.Message = "Nie wybrano pliku!";
            }
            return View();
        }



        /// <summary>
        /// Metoda przetwarza dane zawarte w wczytanym pliku.
        /// </summary>
        /// <param name="all">Liczba rozważanych stanów.</param>
        /// <param name="size">Liczba miejsc do przydziału (rozmair parlamentu).</param>
        /// <param name="p">Wektor liczności poszczególnych stanów.</param>
        public void process(int all, int size, int[] p)
        {

            List<int> result = getResult(all, size, p);

        }





        /// <summary>
        /// Metoda wykonuje algorytm Stilla.
        /// </summary>
        /// <param name="all">Liczba rozważanych stanów.</param>
        /// <param name="size">Liczba miejsc do przydziału (rozmair parlamentu).</param>
        /// <param name="p">Wektor liczności poszczególnych stanów.</param>
        /// <returns>Lista kolejnych przydziałów miejsc.</returns>
        private List<int> getResult(int all, int size, int[] p)
        {
            ///Reprezentuje poszczegóne iteracje (w ktorej zostało komu przydzielone miejsce w parlamencie)
            //Dictionary<int, int> iters = new Dictionary<int, int>();
            List<int> iterations = new List<int>();

            int[] a = new int[all];

            for (int i = 0; i < all - 1; i++)
            {
                a[i] = 0;
            }


            SortedList<int, double> list = new SortedList<int, double>();
            int temp;

            for (int hi = 1; hi <= size; hi++)
            {

                for (int i = 0; i < all; i++)
                {
                    list.Add(i, p[i] / hill(a[i]));
                }
                
                /////////////////var l = list.OrderByDescending(x => x.Value);

                try
                {
                    temp = still(p, list.OrderByDescending(x => x.Value), a, hi);
                    iterations.Add(temp); //dla danej iteracji komu przydzielono
                    a[temp]++;

                    if (a[temp] > p[temp])//Stan nie może mieć więcej miejsc w parlamencie niż obywateli.
                    {
                        throw new Exception(String.Format("Nie da się przydzielić stanowi więcej miejsc w parlamencie, bo stan ma za mało obywateli"));
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    throw new Exception(String.Format("Nie da się przydzielić {0} miejsca w parlamencie", hi));
                    
                }

                list.Clear();
            }

            return iterations;
        }




        /// <summary>
        /// Metoda przeprowadza test górnej kwoty dla stanu.
        /// </summary>
        /// <param name="pi">Liczność stanu</param>
        /// <param name="Epi"></param>
        /// <param name="ha"></param>
        /// <param name="ai">Miejsca przydzielone dla tego stanu.</param>
        /// <returns>Informacja, czy dany stan spełnia test górnej kwoty.</returns>
        private static bool spelniaGornaKwote(double pi, double Epi, int ha, int ai)
        {
            return Math.Ceiling((pi * ha) / Epi) >= ai;
        }





        /// <summary>
        /// Metoda oblicza wartość dolnej kwoty.
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="Epi"></param>
        /// <param name="ha"></param>
        /// <returns>Wartość dolnej kwoty.</returns>
        private static int dolnaKwota(double pi, double Epi, double ha)
        {
            return Convert.ToInt32(Math.Floor(pi * ha / Epi));
        }





        /// <summary>
        /// Metoda przeprowadza test dolnej kwoty.
        /// </summary>
        /// <param name="h"></param>
        /// <param name="index"></param>
        /// <param name="suma"></param>
        /// <param name="list"></param>
        /// <param name="p"></param>
        /// <param name="a"></param>
        /// <returns>Informacja, czy dany stan spełnia test dolnej kwoty.</returns>
        private static bool spelniaDolnaKwote(int h, int index, int suma, IOrderedEnumerable<KeyValuePair<int, double>> list, int[] p, int[] a)
        {
            int pi = p[index];
            int n = p.Length;

            double tmp = Math.Ceiling((double)suma / pi);
            int hb = tmp < n ? Convert.ToInt32(tmp) : n + 1; //+1, bo potem sprawdzamy do <hb, ale tylko w przypadku gdy wartość jest niezmieniona, tj. wartość mianownika/licznik


            int[] s = new int[n];

            for (int hi = h; hi < hb; hi++)
            {
                for (int k = 0; k < n; k++)
                {
                    if (list.ElementAt(k).Value == index)
                    {
                        s[k] = a[list.ElementAt(k).Key] + 1;
                    }
                    else
                    {
                        s[k] = Math.Max(a[list.ElementAt(k).Key], dolnaKwota(p[list.ElementAt(k).Key], suma, hi));
                    }
                }


                if (s.Sum() > hi || s.Sum() >= hb)
                {
                    return false;
                }
            }
            return true;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="list"></param>
        /// <param name="a"></param>
        /// <param name="hi"></param>
        /// <returns>Numer stanu, któremu przydzielono miejsce lub -1.</returns>
        public static int still(int[] p, IOrderedEnumerable<KeyValuePair<int, double>> list, int[] a, int hi)
        {

            for (int i = 0; i < list.Count(); i++)
            {
                if (spelniaGornaKwote(p[list.ElementAt(i).Key], p.Sum(), hi, a[list.ElementAt(i).Key]) && 
                    spelniaDolnaKwote(hi, list.ElementAt(i).Key, p.Sum(), list, p, a))
                {
                    return list.ElementAt(i).Key;
                }
            }
            return -1;
        }




        /// <summary>
        /// Metoda oblicza wartość dla kryterium Hilla.
        /// </summary>
        /// <param name="a">Przydzielone miejsca dla analizowanego stanu.</param>
        /// <returns>Wartość obliczoną dla kryterium.</returns>
        public static double hill(int a)
        {
            int b = a + 1;
            return Math.Sqrt(b * (b + 1));
        }

    }
}