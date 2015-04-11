using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOwZProject.Models
{
    public class State
    {
        [Key]
        public int id { get; set; }

        [DisplayName("Nazwa stanu")]
        [Required(ErrorMessage = "Nazwa stanu jest polem obowiązkowym")]
        public string Name { get; set; }

        [DisplayName("Rozmiar stanu")]
        [Required(ErrorMessage = "Rozmiar stanu jest polem obowiązkowym")]
        [Range(0, int.MaxValue, ErrorMessage = "Wpisz liczbę z odpowiedniego przedziału")]
        public int Size { get; set; }

        [DisplayName("Zdobyte mandaty")]
        public int Mandats { get; set; }

    }
}