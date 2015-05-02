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
        public List<StepLiu> StepsLiu { get; protected set; }



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
            int actualPosition = 0;
            foreach (Task t in this.Tasks)
            {
                this.StepsLiu.Add(new StepLiu(t, actualPosition, actualPosition + t.Duration));
            }
            
        }


    }
}