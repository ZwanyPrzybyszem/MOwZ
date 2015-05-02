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
    public class FileProblemLiu
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
        public ProblemLiu ProblemFromFile { get; set; }





        /// <summary>
        /// Przełożenie problemu z pliku do obiektu.
        /// </summary>
        public void updateProblem()
        {
            int[] words;
            int[] durations;
            int[] periods;

            using (StreamReader reader = new StreamReader(this.FileWithData.InputStream))
            {


                try
                {
                    //LiczbaZadanDoPrzydzialu LiczbaJednostekDoPrzesledzenia
                    words = Array.ConvertAll(reader.ReadLine().Split(new Char[] { ' ', '\t' }), Int32.Parse);
                }
                catch (Exception)
                {
                    throw new Exception("Niepoprawne dane w pliku");
                }
                if (words[0] > 50 || words[1] > 50)
                {
                    throw new Exception("Zbyt duża liczba zadan lub jednostek do prześledzenia");
                }
                try
                {
                    //TrwanieZadania1 TrwanieZadania2...
                    durations = Array.ConvertAll(reader.ReadLine().Split(new Char[] { ' ', '\t' }), Int32.Parse);
                    //OkresZadania1 OkresZadania2...
                    periods = Array.ConvertAll(reader.ReadLine().Split(new Char[] { ' ', '\t' }), Int32.Parse);
                }
                catch (Exception)
                {
                    throw new Exception("Brak poprawnych danych wejściowych dotyczących zadań");
                }
                //this.FileWithData.InputStream.Position = 0;
                reader.BaseStream.Position = 0;
                reader.Close();
            }

            if (words.Length == 2 && words[0] == durations.Length && words[0] == periods.Length &&
                words.All(x => x > 0) && durations.All(x => x > 0) && periods.All(x => x > 0))
            {

                //this.ProblemFromFile = new Problem();
                this.ProblemFromFile.NumberOfUnits = words[1];
                this.ProblemFromFile.Tasks.Clear();


                for (int i = 0; i < durations.Length; i++)
                {
                    this.ProblemFromFile.Tasks.Add(new Task { Id = i, Duration = durations[i], Period = periods[i] });
                }
            }
            else
            {
                throw new Exception("Niepoprawne dane!");
            }
        }
    }



}