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
        /// Informacja czy szczegóły mają być wyświetlane.
        /// </summary>
        [DisplayName("Wyświetl szczegóły przetwarzania")]
        public bool details { get; set; }



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
            Iterations = new List<Iteration>();
        }


        /// <summary>
        /// Zwraca największy wspólny dzielnik dwóch liczb.
        /// </summary>
        public int getNWD(int x, int y)
        {
            while (x % y != 0 && y % x != 0)
            {
                if (x > y)
                {
                    x %= y;
                }
                else
                {
                    y %= x;
                }
            }
            return x < y ? x : y;
        }

        /// <summary>
        /// Zwraca najmniejszą wspólną wielokrotność okresów zadań.
        /// </summary>
        public int getNWW()
        {
            int numberOfTasks = this.Tasks.Count();

            int nww = this.Tasks[0].Period;

            for (int i = 1; i < numberOfTasks; i++)
            {
                int taskPeriod = this.Tasks[i].Period;
                nww = (nww * taskPeriod) / getNWD(nww, taskPeriod);
            }

            return nww;
        }


        /// <summary>
        /// Rozwiązuje problem algorytmem statycznym Liu Laylanda.
        /// </summary>
        public void getLiuResult()
        {
            if (this.details)
            {
                this.StepsLiu = new List<StepLiu>();
            }

            //var sortedTasks = from task in this.Tasks orderby task.Duration,task.Period select task;
            List<Task> sortedTasks = this.Tasks.OrderBy(o => o.Period).ToList();

            int nww = getNWW();

            for (int i = 0; i < nww; i++)
            {
                Task nextTask = null;

                foreach (Task t in sortedTasks)
                {
                    if (t.TaskRemain > ((t.CompletedTask + 1) * t.Period) - i)
                    {
                        throw new Exception(String.Format("Nie wystarczająca liczba jednostek czasu dla zadania {0}", t.Id));
                    }
                    if (t.TaskRemain == 0 && t.CompletedTask * t.Period == i)
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
                    if (this.details)
                    {
                        this.StepsLiu.Add(new StepLiu(nextTask, i, i + 1));
                    }

                    Iteration lastIteration = null;
                    int c = this.Iterations.Count();

                    if (c > 0) 
                    {
                        lastIteration = this.Iterations.ElementAt(c - 1);
                    }
                    if (lastIteration != null && lastIteration.Task.Equals(nextTask))
                    {
                        lastIteration.Stop++;
                    }
                    else
                    {
                        this.Iterations.Add(new Iteration(nextTask, i, i + 1));
                    }
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