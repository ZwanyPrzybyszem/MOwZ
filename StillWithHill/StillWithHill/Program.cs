using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillWithHill
{
    class Program
    {
        private class InvertedComparer : IComparer<double>
        {
            public int Compare(double x, double y)
            {
                return y.CompareTo(x);
            }
        }

        private static bool spelniaGornaKwote(double pi, double Epi, int ha, int ai)
        {
            System.Console.WriteLine((pi * ha / Epi) + "\t" + Math.Ceiling((pi * ha) / Epi) + " >= " + ai + "\t" + (Math.Ceiling((pi * ha) / Epi) >= ai ? Boolean.TrueString : Boolean.FalseString));
            return Math.Ceiling((pi * ha )/ Epi) >= ai;
        }

        private static int dolnaKwota(double pi, double Epi, double ha)
        {
            return Convert.ToInt32(Math.Floor(pi * ha / Epi));
        }

        private static bool spelniaDolnaKwote(int h, int index, int suma, SortedList<double, int> list, int[] v, int[] a)
        {
            int pi = v[index];
            int n = list.Count;

            double tmp = Math.Ceiling((double)suma / pi);
            int hb = tmp<n ? Convert.ToInt32(tmp) : n+1; //+1, bo potem sprawdzamy do <hb, ale tylko w przypadku gdy wartość jest niezmieniona, tj. wartość mianownika/licznik


            string tmpOut = "a: ";
            foreach (int aa in a)
            {
                tmpOut += aa + " ";
            }
            tmpOut += " < " + hb;
            System.Console.WriteLine(tmpOut);


            int[] s = new int[n];

            for (int hi = h; hi < hb; hi++)
            {
                int tmpSuma = 0;
                for (int k = 0; k < n; k++)
                {
                    if (list.Values[k] == index)
                    {
                        s[k] = a[list.Values[k]] + 1;
                    }
                    else
                    {
                        System.Console.WriteLine(a[list.Values[k]] + "\t" + dolnaKwota(v[list.Values[k]], suma, hi));
                        s[k] = Math.Max(a[list.Values[k]],dolnaKwota(v[list.Values[k]],suma,hi));
                    }
                    tmpSuma += s[k];
                }

                tmpOut = "s: ";
                foreach (int ss in s)
                {
                    tmpOut += ss + " ";
                }
                tmpOut += " < " + hi;
                System.Console.WriteLine(tmpOut);

                if (tmpSuma > hi || tmpSuma >= hb)
                {
                    return false;
                }
            }
            return true;
        }

        public static int still(int[] v, int suma, SortedList<double, int> list, int[] a, int hi)
        {

            for (int i = 0; i < list.Count; i++)
            {
                if (spelniaGornaKwote(v[list.Values[i]], suma, hi, a[list.Values[i]]) && spelniaDolnaKwote(hi,list.Values[i],suma,list,v,a))
                {
                    return list.Values[i];
                }
            }
            return -1;
        }

        public static double hill(int a)
        {
            int b = a + 1;
            return Math.Sqrt(b* (b + 1));
        }

        static void Main(string[] args)
        {
            System.Console.WriteLine("Podaj nazwę pliku (z rozszerzeniem): ");
            string file = System.Console.ReadLine();

            string[] text;
            try 
            {
                text = System.IO.File.ReadAllLines(@"..\..\..\tests\"+file);//Łap wyjątek!
            }catch(System.IO.FileNotFoundException){
                System.Console.WriteLine("Nie odnaleziono pliku");
                System.Console.ReadKey();
                return;
            }

            string[] parameters = text[0].Split(new Char[]{' ','\t'});

            int n = Convert.ToInt32(parameters[0]);
            int h = Convert.ToInt32(parameters[1]);

            string[] numbers = text[1].Split(new Char[]{' ','\t'});

            int suma = 0;
            int[] v = new int[n];
            int[] a = new int[n];

            for (int i = 0; i < n; i++)
            {
                a[i] = 0;
                v[i] = Convert.ToInt32(numbers[i]);
                suma += v[i];
            }

            for (int hi = 1; hi <= 5; hi++)
            {
                SortedList<double, int> list = new SortedList<double, int>(new InvertedComparer());
                for (int i = 0; i < n; i++)
                {
                    list.Add(v[i] / hill(a[i]), i);
                }

                try 
                { 
                    a[still(v,suma,list,a,hi)]++;
                    foreach(int aa in a){
                        System.Console.Write(aa + "\t");
                    }
                    System.Console.WriteLine();
                }catch(IndexOutOfRangeException){
                    System.Console.WriteLine("Nie da się przydzielić {0} miejsca w parlamencie",hi);
                    System.Console.ReadKey();
                    return;
                }

                list.Clear();
            }

            System.Console.WriteLine();

            foreach (int i in a)
            {
                System.Console.WriteLine(i);
            }

            System.Console.ReadKey();
        }
    }
}
