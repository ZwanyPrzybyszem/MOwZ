using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOwZProject.Models
{
    /// <summary>
    /// Klasa reprezentująca stan.
    /// </summary>
    public class State
    {
        /// <summary>
        /// Numer identyfikacyjny stanu.
        /// </summary>
        [Key]
        public int Id { get; set; }



        /// <summary>
        /// Nazwa stanu.
        /// </summary>
        [DisplayName("Nazwa stanu")]
        [MaxLength(20)]
        public string Name { get; set; }



        /// <summary>
        /// Rozmiar stanu.
        /// </summary>
        [DisplayName("Rozmiar stanu")]
        [Required(ErrorMessage = "Rozmiar stanu jest polem obowiązkowym")]
        [Range(0, int.MaxValue, ErrorMessage = "Wpisz liczbę z odpowiedniego przedziału")]
        public int Size { get; set; }



        /// <summary>
        /// Mandaty przydzielone stanowi.
        /// </summary>
        [DisplayName("Zdobyte mandaty")]
        public int Mandats { get; set; }

    }
}