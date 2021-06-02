using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTECurd
{
    class GameSystem
    {
        public void RitaHuvudmeny()
        {
            Console.WriteLine("1=Spela");
            Console.WriteLine("2=Visa Highscore");
            Console.WriteLine("0=Avsluta");
        }

        public int RunGameRound()
        {
            Console.WriteLine("Jag tänker på ett tal mellan 1 och 10. Gissa vilket:");

            var oRand = new Random();
            var secretTal = oRand.Next(1, 10);
            var antalGissningar = 0;
            while (true)
            {
                var gissning = Convert.ToInt32(Console.ReadLine());
                antalGissningar++;
                if (gissning == secretTal)
                {

                    Console.WriteLine("Hurra! Det var rätt");
                    break;
                }
                else if (gissning < secretTal)
                {
                    Console.WriteLine("Nej! Mitt tal är högre än så");
                }
                else if (gissning > secretTal)
                {
                    Console.WriteLine("Nej! Mitt tal är lägre än så");
                }

            }

            return antalGissningar;
        }

        public List<HighScoreEntry> ReadListFromFile()
        {
            if (System.IO.File.Exists("C:\\Users\\stefa\\Temp\\highscore.txt") == false)
                return new List<HighScoreEntry>();

            var filecontents =
                File.ReadAllText("C:\\Users\\stefa\\Temp\\highscore.txt");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<HighScoreEntry>>(filecontents);
        }

        public void SaveListToFile(List<HighScoreEntry> list)
        {
            var contents = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            System.IO.File.WriteAllText("C:\\Users\\stefa\\Temp\\highscore.txt", contents);
        }


        public void Run()
        {
            var highscoreLista = ReadListFromFile();
            while (true)
            {
                RitaHuvudmeny();
                Console.WriteLine("Välj var du vill göra:");
                var val = Convert.ToInt32(Console.ReadLine());
                if (val == 0)
                {
                    SaveListToFile(highscoreLista);
                    return;
                }

                if (val == 1)
                {
                    Console.Clear();
                    var antal = RunGameRound();
                    Console.WriteLine("Bra! Skriv ditt namn så kommer du in på highscorelistan:");
                    var namn = Console.ReadLine();
                    var entry = new HighScoreEntry();
                    entry.Name = namn;
                    entry.Points = antal;
                    highscoreLista.Add(entry);
                }

                if (val == 2)
                {
                    Console.Clear();
                    Console.WriteLine("*** HIGHSCORE ***");
                    foreach (var entry in highscoreLista)
                    {
                        Console.WriteLine($"{entry.Name} {entry.Points}");
                    }
                }

            }
        }
    }
}
