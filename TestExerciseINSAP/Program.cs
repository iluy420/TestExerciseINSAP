using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TestExerciseINSAP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write(@"Введите путь к файлу с исходными данными (D:\test\test.txt): ");
            string path = Console.ReadLine(); //путь к файу

            path = path.Replace("/", @"\"); // замена на правильный слеш в случае ошибки 

            FileStream reader = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(reader);

            List<string> words = new List<string>(); // список слов

            //записываем слова в список пока они не закончатся
            bool wordsEnded = false;
            while (!wordsEnded)
            {
                string buferString = read.ReadLine();
                if (String.IsNullOrEmpty(buferString))
                {
                    wordsEnded = true;
                }
                else
                {
                    words.Add(buferString);
                }
            }

            //просим ввести букву пока пользователь не введет правильно
            char letter = ' '; // буква для создания подстроки 
            bool exit = true;
            while (exit)
            {
                try
                {
                    Console.Write("Введите из какой буквы вы хотите получить подстроку: ");
                    letter = Convert.ToChar(Console.ReadLine().ToLower()); // буква 
                    if (Char.IsLetter(letter) && letter >= 'a' && letter <= 'z' )
                    {
                        exit = false;
                    }
                    else
                    {
                        Console.WriteLine("Буква должна быть одна [a-z]!");
                    }
                }
                catch
                {
                    Console.WriteLine("Буква должна быть одна [a-z]!");
                }
            }

            string result = "";

            string max = "";


            for(int i = 0; i < words.Count - 1; i++)
            {
                
            }
            



            ////////////////
            Console.WriteLine($"N = {words.Count} \nБуква - {letter}") ;
            Console.ReadKey();


        }
    }
}
