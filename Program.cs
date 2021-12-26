using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoSeeker;

namespace Pepper_seeker_v2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("PromoSeeker 1.0! \n----------------");
            System.Threading.Thread.Sleep(3000);

            int delayInMinutes = 3;
            int turn = 0;
            string sendIdsPath = "wyslane.txt";
            string wordsPath = "words.txt";

            //remove all info from a file
            File.WriteAllText(sendIdsPath, String.Empty);

            //load txt file 
            string text = File.ReadAllText(wordsPath);
            var wordsList = text.Split(',').ToList();

            while (true)
            {
                var timeNow = DateTime.Now;
                Console.Clear();
                turn++;
                List<Tweet> getInfo = Pepper.GetData(wordsList);
                Console.Clear();
                
                //here you can add some more information sources and add it to getInfo list

                Console.WriteLine("{0}. Sprawdzenie: {1}\n\n", turn, timeNow.ToString());
                WriteText.Color("Black", "Blue", "Znaleziono pasujące:");

                foreach (var item in getInfo)
                {
                    Console.WriteLine(item.Id + ": " + item.Name);
                }

                //check if already sent
                var alreadyHaveSentPromo = File.ReadAllText(sendIdsPath);
                List<string> sentIdsList = alreadyHaveSentPromo.Split(',').ToList();
                Console.WriteLine();
                WriteText.Color("Black", "Red", "Wysłano:");

                foreach (var info in getInfo)
                {
                    bool isExit = false;

                    foreach (var sentId in sentIdsList)
                    {
                        if (info.Id == sentId)
                        {
                            isExit = true;
                            break;
                        }
                    }

                    if (isExit)
                    {
                        continue;
                    }
                    else
                    {
                        //send
                        File.AppendAllText(sendIdsPath, info.Id + ",");
                        Console.WriteLine("{0}: {1}", info.Id, info.Name);
                        
                        //send tweet
                        var zadanie = SendTweet.Send(info);
                    }
                }

                for (int i = delayInMinutes * 60; i > 0; i--)
                {
                    Console.SetCursorPosition(0, 1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Do kolejnego sprawdzenia pozostało: {0} s   ", i);
                    System.Threading.Thread.Sleep(1000);
                }

                Console.ForegroundColor = ConsoleColor.Green;
            }
        }
    }
}
