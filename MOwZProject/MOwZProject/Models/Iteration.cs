using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOwZProject.Models
{
    /// <summary>
    /// Klasa reprezentująca krok algorytmu uszeregowywujący zadanie.
    /// </summary>
    public class Iteration
    {
        /// <summary>
        /// Zadanie, którego dotyczy krok.
        /// </summary>
        [DisplayName("Zadanie")]
        public Task Task { get; private set; }



        /// <summary>
        /// Rozpoczęcie wykonywania zadania.
        /// </summary>
        [DisplayName("Rozpoczęcie wykonywania zadania")]
        public int Start { get; private set; }



        /// <summary>
        /// Zakończenie wykonywania zadania.
        /// </summary>
        [DisplayName("Zakończenie wykonywania zadania")]
        public int Stop { get; private set; }




        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="t">Zadanie, którego dotyczy krok.</param>
        /// <param name="start">Rozpoczęcie zadania.</param>
        /// <param name="stop">Zakończenie zadania.</param>
        public Iteration(Task t, int start, int stop) 
        {
            this.Task = t;
            this.Start = start;
            this.Stop = stop;
        }

    }
}