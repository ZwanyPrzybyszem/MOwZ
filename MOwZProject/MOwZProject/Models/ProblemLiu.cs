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
    public class ProblemLiu
    {
        /// <summary>
        /// Lista rozważanych zadań.
        /// </summary>
        [DisplayName("Rozważane zadania")]
        [Required(ErrorMessage = "Konieczne jest podanie zadań!")]
        public List<Task> Tasks { get; private set; }
        

        /// <summary>
        /// Liczba maszyn.
        /// </summary>
        [DisplayName("Liczba jednostek do prześledzenia")]
        [Required(ErrorMessage = "Liczba jednostek do prześledzenia jest polem obowiązkowym")]
        [Range(0, int.MaxValue, ErrorMessage = "Wpisz liczbę z odpowiedniego przedziału")]
        public int NumberOfUnits { get; set; }



        /// <summary>
        /// Informacja czy szczegóły mają być wyświetlane.
        /// </summary>
        [DisplayName("Wyświetl szczegóły przetwarzania")]
        public bool details { get; private set; }



        /// <summary>
        /// Lista zawierająca szczegóły kolejnych kroków.
        /// </summary>
        public List<StepLiu> StepsLiu { get; private set; }



        /// <summary>
        /// Lista zawierająca dane dotyczące kolejnych uszeregowań.
        /// </summary>
        public List<Iteration> Iterations { get; private set; }



        /// <summary>
        /// Konstruktor inicjalizujący listy.
        /// </summary>
        public ProblemLiu()
        {
            Tasks = new List<Task>();
            Tasks.Add(new Task());
        }







        /// <summary>
        /// Rozwiązuje problem algorytmem statycznym Liu Laylanda.
        /// </summary>
        public void getLiuResult()
        {
            this.Iterations = new List<Iteration>();

            //var sortedTasks = from task in this.Tasks orderby task.Duration,task.Period select task;
            List<Task> sortedTasks = this.Tasks.OrderBy(o => o.Period).ToList();

            int nww = this.NumberOfUnits;

            for (int i = 0; i < nww; i++)
            {
                Task nextTask = null;

                foreach (Task t in sortedTasks)
                {
                    if (t.TaskRemain > ((t.CompletedTask+1)*t.Period)-i)
                    {
                        throw new Exception(String.Format("Nie wystarczająca liczba jednostek czasu dla zadania {0}", t.Id));
                    }
                    if (t.TaskRemain == 0 && t.CompletedTask*t.Period == i)
                    {
                        t.TaskRemain = t.Duration;
                    }
                    if (nextTask == null && t.TaskRemain > 0)
                    {
                        nextTask = t;
                    }
                }

                if (nextTask != null)
                {
                    this.Iterations.Add(new Iteration(nextTask, i, i + 1));
                    if (--nextTask.TaskRemain == 0)
                    {
                        nextTask.CompletedTask++;
                    }
                }
                // Jeśli nextTask == null to ewentualnie dodać wyświetlanie informacji, że w tej iteracji nikomu nie przydzielono.   
            }

        }

    }
}