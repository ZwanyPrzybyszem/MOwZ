using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace MOwZProject.Models
{
    /// <summary>
    /// Walidacja dotycząca pliku, czy posiada odpowiednie rozszerzenie.
    /// </summary>
    public class CheckExtensionsValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Oczekiwane rozszerzenie.
        /// </summary>
        public string ext { get; private set; }



        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="ext">Nazwa oczekiwanego rozszerzenia</param>
        public CheckExtensionsValidationAttribute(string ext)
        {
            this.ext = ext;
        }



        /// <summary>
        /// Sprawdzenie poprawności
        /// </summary>
        /// <param name="value">Przekazana wartość</param>
        /// <param name="validationContext">Kontekst, w którym odbywa się sprawdzanie poprawności.</param>
        /// <returns>Informacja czy format jest poprawny czy też nie.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && this.ext != Path.GetExtension(((HttpPostedFileBase)value).FileName))
            {
                return new ValidationResult(this.ErrorMessage = "Niepoprawny format pliku!");
            }
            return null;
        }
    }
}