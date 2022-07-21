using System;
using System.IO;

namespace TestExerciseINSAP
{
    internal class Program
    {
        private static string _path = ""; // путь к файлу

        private static void Main(string[] args)
        {

            #region опрос пользоватея о пути к файлу
            bool exitPath = false;
            while (!exitPath)
            {
                Console.Write(@"Введите путь к файлу с исходными данными (D:\test\test.txt): ");
                _path = Console.ReadLine(); //путь к файу
                _path = _path.Replace("/", @"\"); // замена на правильный слеш в случае ошибки 
                try
                {
                    FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read);
                    exitPath = true;
                }
                catch
                {
                    Console.WriteLine("Не верно введен путь к файлу!");
                }
            }
            #endregion

            bool exitProgramm = false;
            while (!exitProgramm)
            {
                //запускаем алгоритм
                AalgorithmSearchingSubstringOfOneCharacter.AlgorithmSearchingSubstring(_path);

                //опрос хочет ли пользователь продолжить раотать с программой  
                Console.Write("Если хотите выйти из программы введите [y] : ");
                string answer = Console.ReadLine();
                if (answer == "y" || answer == "Y") exitProgramm = true;
                Console.Clear();
            }
        }
    }
}