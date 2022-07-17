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
                    if (buferString.Contains(letter))
                    words.Add(buferString);// записываем слово в массив если оно содержит данную букву
                }
            }

            string result = "";//результат 

            //находим первое слово 
            int maxLength = 0;
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i][words[i].Length - 1] != letter) continue;//если правая буква не та которую мы ищем то проходим мимо
                int bufLength = 1;
                for (int j = words[i].Length - 2; j >= 0; j--)
                {
                    if (words[i][j] == letter) bufLength++;
                    else break;
                }
                if (bufLength > maxLength && bufLength != words[i].Length) 
                { 
                    result = words[i];
                    maxLength = bufLength;
                }
            }

            string str = "";


            ////////////////
            Console.WriteLine($"N = {words.Count} \nБуква - {letter}");
            Console.WriteLine($"РЕЗУЛЬТАТ = {result}");
            Console.ReadKey();


        }
    }
}
