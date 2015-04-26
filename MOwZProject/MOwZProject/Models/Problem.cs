using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOwZProject.Models
{
    /// <summary>
    /// Klasa reprezentująca dane przetwarzanego problemu.
    /// </summary>
    public class Problem
    {
        /// <summary>
        /// Lista rozważanych stanów.
        /// </summary>
        [DisplayName("Rozważane stany")]
        [Required(ErrorMessage="Konieczne jest podanie stanów!")]
        public List<State> States { get; set; }
        


        /// <summary>
        /// Rozmiar parlamentu.
        /// </summary>
        [DisplayName("Wielkość parlamentu")]
        [Required(ErrorMessage = "Wielkość parlamentu jest polem obowiązkowym")]
        [Range(0, int.MaxValue, ErrorMessage = "Wpisz liczbę z odpowiedniego przedziału")]
        public int ParlamentSize { get; set; }
        


        /// <summary>
        /// Lista zawierająca przydziały w kolejnych iteracjach.
        /// </summary>
        public List<int> Iterations {get; set; }



        /// <summary>
        /// Informacja czy szczegóły mają być wyświetlane.
        /// </summary>
        [DisplayName("Wyświetl szczegóły przetwarzania")]
        public bool details { get; set; }



        /// <summary>
        /// Lista zawierająca szczegóły kolejnych kroków.
        /// </summary>
        public List<Step> Steps { get; set; }



        /// <summary>
        /// Konstruktor inicjalizujący listy.
        /// </summary>
        public Problem()
        {
            States = new List<State>();
            States.Add(new State());
            Iterations = new List<int>();
        }







        /// <summary>
        /// Rozwiązuje problem metodą Stilla z kryterium Hilla.
        /// </summary>
        public void getStillResult()
        {

            //ograniczenie do max 50 stanów
            if (this.States.Count > 50)
            {
                throw new Exception("Zbyt duża liczba stanów. (max 50)");
            }

            //int to id stanu, double wartosc
            SortedList<int, double> list = new SortedList<int, double>();
            int temp;

            if (this.details)
            {
                this.Steps = new List<Step>();
            }

            for (int hi = 1; hi <= this.ParlamentSize; hi++)
            {

                foreach(State s in this.States)
                {
                    list.Add(s.id, s.Size / hill(s.id));
                }

                try
                {
                    temp = still(list.OrderByDescending(x => x.Value), hi);

                    if (temp >= 0)
                    {
                        this.Iterations.Add(temp); //dla danej iteracji komu przydzielono
                        // zwiększenie liczby mandatów
                        this.States.Find(s => s.id == temp).Mandats++;
                    }
                    if (this.States.Find(s => s.id == temp).Mandats > this.States.Find(s => s.id == temp).Size)//Stan nie może mieć więcej miejsc w parlamencie niż obywateli.
                    {
                        this.Iterations.Clear();
                        foreach(State s in this.States)
                        {
                            s.Mandats = 0;
                        }
                        throw new Exception(String.Format("Nie da się przydzielić stanowi więcej miejsc w parlamencie, bo stan ma za mało obywateli"));
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    throw new Exception(String.Format("Nie da się przydzielić {0} miejsca w parlamencie", hi));
                    
                }

                list.Clear();
            }

        }



        /// <summary>
        /// Wyznaczenie stanu który otrzyma kolejne miejsce w parlamencie.
        /// </summary>
        /// <param name="list">Posortowana wg kryterium Hilla sekwencja stanów.</param>
        /// <param name="hi">Numer aktualnie przydzielanego miejsca w parlamencie.</param>
        /// <returns>Numer stanu, któremu przydzielono miejsce lub gdy przydział był niemożliwy -1.</returns>
        private int still(IOrderedEnumerable<KeyValuePair<int, double>> list, int hi)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                if (details)
                {
                    this.Steps.Add(new Step());
                    this.Steps.Last().Element = this.States.Find(s => s.id == list.ElementAt(i).Key).Name;
                }
                if (spelniaGornaKwote(hi, list.ElementAt(i).Key) &&
                    spelniaDolnaKwote(hi, list.ElementAt(i).Key, list))
                {
                    return list.ElementAt(i).Key;
                }
                if (details && this.Steps.Last().DolnaKwota == null)
                {
                    this.Steps.Last().DolnaKwota = "Niespełniono testu górnej kwoty";
                }
            }
            return -1;
        }



        /// <summary>
        /// Metoda przeprowadza test dolnej kwoty.
        /// </summary>
        /// <param name="hi">Numer aktualnie przydzielanego miejsca w parlamencie</param>
        /// <param name="StateIndex">Indeks sprawdzanego stanu</param>
        /// <param name="list">Posortowana wg kryterium Hilla sekwencja stanów</param>
        /// <returns>Informacja, czy dany stan spełnia test dolnej kwoty.</returns>
        private bool spelniaDolnaKwote(int hi, int StateIndex, IOrderedEnumerable<KeyValuePair<int, double>> list)
        {
            
            double tmp = Math.Ceiling((double)(this.States.Sum(st => st.Size))/this.States.Find(st => st.id == StateIndex).Size * hi);
            int hb = tmp < this.ParlamentSize ? Convert.ToInt32(tmp) : this.ParlamentSize + 1; //+1, bo potem sprawdzamy do <hb, ale tylko w przypadku gdy wartość jest niezmieniona, tj. wartość mianownika/licznik

            int[] s = new int[this.States.Count];

            for (int hii = hi; hii < hb; hii++)
            {
                for (int k = 0; k < this.States.Count; k++)
                {
                    if (list.ElementAt(k).Value == StateIndex)
                    {
                        s[k] = this.States.Find(st => st.id == list.ElementAt(k).Key).Mandats + 1;
                    }
                    else
                    {
                        s[k] = Math.Max(this.States.Find(st => st.id == list.ElementAt(k).Key).Mandats, dolnaKwota(hii, list.ElementAt(k).Key));
                    }
                }

                if (this.details)
                {
                    this.Steps.Last().DolnaKwota = "(" + s.Sum().ToString() + " ≤ " + hii.ToString() + ") AND (" + s.Sum().ToString() + " < " + hb.ToString() + ")";
                    this.Steps.Last().SpelniaTestDolnejKwoty = (s.Sum() <= hii && s.Sum() < hb);
                }

                if (s.Sum() > hii || s.Sum() >= hb)
                {
                    return false;
                }
            }
            if (this.details && this.Steps.Last().DolnaKwota == null)
            {
                this.Steps.Last().DolnaKwota = "Spełniony, ale nie wiem dlaczego";
                this.Steps.Last().SpelniaTestDolnejKwoty = true;
            }
            return true; //DLACZEGO TU JEST TRUE?
        }



        /// <summary>
        /// Metoda oblicza wartość dolnej kwoty.
        /// </summary>
        /// <param name="hi">Numer aktualnie przydzielanego miejsca w parlamencie</param>
        /// <param name="StateIndex">Numer identyfikujący analizowany stan.</param>
        /// <returns>Wartość dolnej kwoty.</returns>
        private int dolnaKwota(double hi, int StateIndex)
        {
            return Convert.ToInt32(Math.Floor(this.States.Find(s => s.id == StateIndex).Size * hi / this.States.Sum(st => st.Size)));
        }



        /// <summary>
        /// Metoda przeprowadza test górnej kwoty dla stanu.
        /// </summary>
        /// <param name="hi">Numer aktualnie przydzielanego miejsca w parlamencie</param>
        /// <param name="StateIndex">Numer id analizowanego stanu.</param>
        /// <returns>Informacja, czy dany stan spełnia test górnej kwoty.</returns>
        private bool spelniaGornaKwote(int hi, int StateIndex)
        {
            if (this.details)
            {
                this.Steps.Last().GornaKwota = "⌈ (" + this.States.Find(s => s.id == StateIndex).Size.ToString() + " * " + hi.ToString() + ") / " + this.States.Sum(s => s.Size).ToString() + " ⌉ >= " + this.States.Find(s => s.id == StateIndex).Mandats.ToString();
                this.Steps.Last().SpelniaTestGornejKwoty = Math.Ceiling((this.States.Find(s => s.id == StateIndex).Size * hi) / (double)this.States.Sum(s => s.Size)) >= this.States.Find(s => s.id == StateIndex).Mandats;
            }
            return Math.Ceiling((this.States.Find(s => s.id == StateIndex).Size * hi) / (double)this.States.Sum(s => s.Size)) >= this.States.Find(s => s.id == StateIndex).Mandats;
        }



        /// <summary>
        /// Metoda oblicza wartość dla kryterium Hilla.
        /// </summary>
        /// <param name="StateIndex">Numer id analizowanego stanu.</param>
        /// <returns>Wartość obliczona dla kryterium.</returns>
        private double hill(int StateIndex)
        {
            int b = this.States.Find(s => s.id == StateIndex).Mandats + 1;
            return Math.Sqrt(b * (b + 1));
        }

    }
}