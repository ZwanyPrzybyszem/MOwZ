using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                            words = Array.ConvertAll(reader.ReadLine().Split(' '), Int32.Parse);

                            p = Array.ConvertAll(reader.ReadLine().Split(' '), Int32.Parse);
                        }

                        if (words.Length == 2 && words[0] == p.Length && words.All(x => x > 0) && p.All(x => x > 0))
                        {

                            process(words[0], words[1], p);

                            ViewBag.Message = "Przetworzono pomyślnie.";
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
        private void process(int all, int size, int[] p)
        {

            Dictionary<int, int> result = getResult(all, size, p);

        }





        /// <summary>
        /// Metoda wykonuje algorytm Stilla.
        /// </summary>
        /// <param name="all">Liczba rozważanych stanów.</param>
        /// <param name="size">Liczba miejsc do przydziału (rozmair parlamentu).</param>
        /// <param name="p">Wektor liczności poszczególnych stanów.</param>
        /// <returns>Lista kolejnych przydziałów miejsc.</returns>
        private Dictionary<int, int> getResult(int all, int size, int[] p)
        {
            ///Reprezentuje poszczegóne iteracje (w ktorej zostało komu przydzielone miejsce w parlamencie)
            Dictionary<int, int> iters = new Dictionary<int, int>();

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
                list.OrderByDescending(x => x.Value);


                try
                {
                    temp = still(p, list, a, hi);
                    iters.Add(hi, temp); //dla danej iteracji komu przydzielono
                    a[temp]++;

                    /*
                    foreach (int aa in a)
                    {
                        System.Console.Write(aa + "\t");
                    }
                    System.Console.WriteLine();
                    */
                }
                catch (IndexOutOfRangeException)
                {
                    throw new Exception(String.Format("Nie da się przydzielić {0} miejsca w parlamencie", hi));
                    /*
                    System.Console.WriteLine("Nie da się przydzielić {0} miejsca w parlamencie", hi);
                    System.Console.ReadKey();
                    return;
                    */
                }

                list.Clear();
            }

            return iters;
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
            //System.Console.WriteLine((pi * ha / Epi) + "\t" + Math.Ceiling((pi * ha) / Epi) + " >= " + ai + "\t" + (Math.Ceiling((pi * ha) / Epi) >= ai ? Boolean.TrueString : Boolean.FalseString));
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
        private static bool spelniaDolnaKwote(int h, int index, int suma, SortedList<int, double> list, int[] p, int[] a)
        {
            int pi = p[index];
            int n = p.Length;

            double tmp = Math.Ceiling((double)suma / pi);
            int hb = tmp < n ? Convert.ToInt32(tmp) : n + 1; //+1, bo potem sprawdzamy do <hb, ale tylko w przypadku gdy wartość jest niezmieniona, tj. wartość mianownika/licznik


            /*
            string tmpOut = "a: ";
            foreach (int aa in a)
            {
                tmpOut += aa + " ";
            }
            tmpOut += " < " + hb;
            System.Console.WriteLine(tmpOut);
            */

            int[] s = new int[n];

            for (int hi = h; hi < hb; hi++)
            {
                for (int k = 0; k < n; k++)
                {
                    if (list.Values[k] == index)
                    {
                        s[k] = a[list.Keys[k]] + 1;
                    }
                    else
                    {
                        //System.Console.WriteLine(a[list.Keys[k]] + "\t" + dolnaKwota(v[list.Values[k]], suma, hi));
                        s[k] = Math.Max(a[list.Keys[k]], dolnaKwota(p[list.Keys[k]], suma, hi));
                    }
                }

                /*
                tmpOut = "s: ";
                foreach (int ss in s)
                {
                    tmpOut += ss + " ";
                }
                tmpOut += " < " + hi;
                System.Console.WriteLine(tmpOut);
                */

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
        public static int still(int[] p, SortedList<int, double> list, int[] a, int hi)
        {

            for (int i = 0; i < list.Count; i++)
            {
                if (spelniaGornaKwote(p[list.Keys[i]], p.Sum(), hi, a[list.Keys[i]]) && spelniaDolnaKwote(hi, list.Keys[i], p.Sum(), list, p, a))
                {
                    return list.Keys[i];
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