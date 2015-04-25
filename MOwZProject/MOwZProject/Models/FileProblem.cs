﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace MOwZProject.Models
{
    /// <summary>
    /// Klasa reprezentująca dane przetwarzanego problemu, którego źródłem jest plik.
    /// </summary>
    public class FileProblem
    {
        /// <summary>
        /// Plik z danymi dotyczącymi problemu.
        /// </summary>
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Brak pliku!")]
        [DisplayName("Plik z danymi do przetworzenia")]
        [CheckExtensions(".txt")]
        public HttpPostedFileBase FileWithData { get; set; }



        /// <summary>
        /// Problem wygenerowany na podstawie danych z pliku.
        /// </summary>
        public Problem ProblemFromFile { get; set; }





        /// <summary>
        /// Przełożenie problemu z pliku do obiektu.
        /// </summary>
        public void updateProblem()
        {
            int[] words;
            int[] p;

            using (StreamReader reader = new StreamReader(this.FileWithData.InputStream))
            {
                //LiczbaStanow LiczbaMandatowDoPrzydzialu
                words = Array.ConvertAll(reader.ReadLine().Split(new Char[] { ' ', '\t' }), Int32.Parse);
                //Licznosc1Stanu Licznosc2Stanu ...
                p = Array.ConvertAll(reader.ReadLine().Split(new Char[] { ' ', '\t' }), Int32.Parse);
                //this.FileWithData.InputStream.Position = 0;
                reader.BaseStream.Position = 0;
                reader.Close();
            }

            if (words.Length == 2 && words[0] == p.Length && words.All(x => x > 0) && p.All(x => x > 0))
            {

                //this.ProblemFromFile = new Problem();
                this.ProblemFromFile.ParlamentSize = words[1];
                this.ProblemFromFile.States.Clear();
          

                for (int i = 0; i < p.Length; i++)
                {
                    this.ProblemFromFile.States.Add(new State { id = i, Mandats = 0, Name = (i + 1).ToString(), Size = p[i] });
                }
            }
            else
            {
                throw new Exception("Niepoprawne dane!");
            }
        }
    }



    /// <summary>
    /// Walidacja dotycząca pliku, czy posiada odpowiednie rozszerzenie.
    /// </summary>
    public class CheckExtensions : ValidationAttribute
    {
        /// <summary>
        /// Oczekiwane rozszerzenie.
        /// </summary>
        public string ext { get; private set; }


        
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="ext">Nazwa oczekiwanego rozszerzenia</param>
        public CheckExtensions(string ext)
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