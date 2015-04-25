using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOwZProject.Models
{
    /// <summary>
    /// Klasa reprezentująca krok algorytmu (wykorzystywany przy wyświetlaniu szczegółów).
    /// </summary>
    public class Step
    {
        /// <summary>
        /// Nazwa rozważanego stanu.
        /// </summary>
        [DisplayName("Nazwa rozważanego stanu")]
        public string Element { get; set; }



        /// <summary>
        /// Test dolnej kwoty.
        /// </summary>
        [DisplayName("Test dolnej kwoty")]
        public string DolnaKwota { get; set; }



        /// <summary>
        /// Informacja czy test dolnej kwoty został spełniony.
        /// </summary>
        public bool SpelniaTestDolnejKwoty { get; set; }



        /// <summary>
        /// Test górnej kwoty.
        /// </summary>
        [DisplayName("Test górnej kwoty")]
        public string GornaKwota { get; set; }



        /// <summary>
        /// Informacja czy test górnej kwoty został spełniony.
        /// </summary>
        public bool SpelniaTestGornejKwoty { get; set; }


    }
}