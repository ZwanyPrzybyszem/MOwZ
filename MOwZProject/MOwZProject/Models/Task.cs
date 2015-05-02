using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOwZProject.Models
{
    /// <summary>
    /// Obiekt reprezentujący zadanie.
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Numer identyfikacyjny zadania.
        /// </summary>
        [DisplayName("Numer zadania")]
        [Key]
        public int Id { get; set; }



        /// <summary>
        /// Czas trwania zadania.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Wpisz liczbę z odpowiedniego przedziału")]
        [DisplayName("Czas trwania zadania")]
        [Required(ErrorMessage = "Czas trwania zadania jest polem obowiązkowym")]
        public int Duration { get; set; }



        /// <summary>
        /// Okres w którym zadanie musi się wykonać.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Wpisz liczbę z odpowiedniego przedziału")]
        [DisplayName("Okres dla wykonania zadania")]
        [Required(ErrorMessage = "Okres dla wykonania zadania jest polem obowiązkowym")]
        public int Period { get; set; }
    }
}