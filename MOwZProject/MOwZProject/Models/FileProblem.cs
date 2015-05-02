using System;
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
        [CheckExtensionsValidationAttribute(".txt")]
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


                try
                {
                    //LiczbaStanow LiczbaMandatowDoPrzydzialu
                    words = Array.ConvertAll(reader.ReadLine().Split(new Char[] { ' ', '\t' }), Int32.Parse);
                }
                catch (Exception)
                {
                    throw new Exception("Niepoprawne dane w pliku");
                }
                if (words[0] > 50)
                {
                    throw new Exception("Zbyt duża liczba stanów");
                }
                try
                {
                    //Licznosc1Stanu Licznosc2Stanu ...
                    p = Array.ConvertAll(reader.ReadLine().Split(new Char[] { ' ', '\t' }), Int32.Parse);
                }
                catch (Exception)
                {
                    throw new Exception("Brak poprawnych danych wejściowych dotyczących stanów");
                }
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
                    this.ProblemFromFile.States.Add(new State { Id = i, Mandats = 0, Name = (i + 1).ToString(), Size = p[i] });
                }
            }
            else
            {
                throw new Exception("Niepoprawne dane!");
            }
        }
    }



    

}