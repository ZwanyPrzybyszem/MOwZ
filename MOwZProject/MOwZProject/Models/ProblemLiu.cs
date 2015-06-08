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
        public List<string> Steps { get; private set; }



        /// <summary>
        /// Lista zawierająca dane dotyczące kolejnych uszeregowań.
        /// </summary>
        public List<Iteration> Iterations { get; private set; }



        /// <summary>
        /// Informacja czy uszeregowanie zostało zrealizowane.
        /// </summary>
        public bool Done { get; private set; }


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
                nww = nww * (taskPeriod / getNWD(nww, taskPeriod));
            }

            return nww;
        }

        /// <summary>
        /// Rozwiązuje problem algorytmem statycznym Liu Laylanda.
        /// </summary>
        public void getLiuResult()
        {
            this.Done = true;
            if (this.details)
            {
                this.Steps = new List<string>();
            }

            foreach (Task t in this.Tasks)
            {
                if (t.Duration < 1 || t.Period < 1) 
                {
                    throw new ProblemLiu.WrongDataException(String.Format("Niepoprawne dane opisujące zadania! Uzupełnij liczbami > 0!"));
                }
            }

            //var sortedTasks = from task in this.Tasks orderby task.Duration,task.Period select task;
            List<Task> sortedTasks = this.Tasks.OrderBy(o => o.Period).ToList();

            int nww = getNWW();

            for (int i = 0; i < nww; i++)
            {
                Task nextTask = null;

                foreach (Task t in sortedTasks)
                {
                    if ( (t.TaskRemain > ((t.CompletedTask + 1) * t.Period) - i) || ((t.TaskRemain == ((t.CompletedTask + 1) * t.Period) - i) && nextTask != null) )
                    {
                        //this.Iterations.Clear();
                        if (this.details)
                        {
                            this.Steps.Add("Nie wystarczająca liczba jednostek czasu dla zadania " + t.Id.ToString());
                        }
                        this.Done = false;
                        throw new ProblemLiu.NotEnoughTimeUnitsException(String.Format("Nie wystarczająca liczba jednostek czasu dla zadania {0}", t.Id));
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
                        if (this.details)
                        {
                            this.Steps.Add(String.Format("Uszeregowano zadanie {0} (po raz {1})", nextTask.Id, nextTask.CompletedTask));
                        }
                    }
                }  
            }

        }

        /// <summary>
        /// Wyrzucany gdy są podane błędne dane wejściowe.
        /// </summary>
        public class WrongDataException : Exception
        {
            public WrongDataException() : base() { }
            public WrongDataException(string s) : base(s) { }
            public WrongDataException(string s, Exception i) : base(s, i) { }
        }

        /// <summary>
        /// Wyrzucany gdy brakuje jednostek czasu do wykonania zadania.
        /// </summary>
        public class NotEnoughTimeUnitsException : Exception
        {
            public NotEnoughTimeUnitsException() : base() { }
            public NotEnoughTimeUnitsException(string s) : base(s) { }
            public NotEnoughTimeUnitsException(string s, Exception i) : base(s, i) { }
        }

    }

}