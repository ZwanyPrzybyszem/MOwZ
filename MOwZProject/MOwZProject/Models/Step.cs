using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOwZProject.Models
{
    public class Step
    {
        [DisplayName("Nazwa rozważanego stanu")]
        public string Element { get; set; }

        [DisplayName("Test dolnej kwoty")]
        public string DolnaKwota { get; set; }

        public bool SpelniaTestDolnejKwoty { get; set; }

        [DisplayName("Test górnej kwoty")]
        public string GornaKwota { get; set; }

        public bool SpelniaTestGornejKwoty { get; set; }


    }
}