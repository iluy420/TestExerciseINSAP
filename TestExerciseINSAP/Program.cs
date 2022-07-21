﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestExerciseINSAP
{
    internal class Program
    {
        private static string _path = ""; // путь к файлу
        private static List<string> _words = new List<string>(); // список слов
        private static string _result = "";//результат
        private static char _letter = ' '; // буква для создания подстроки 

        private static int _maxLengthLeft = 0;//кол-во символов в левом слове
        private static string _strLeft = ""; // левое слово

        private static int _maxLengthRight = 0;//кол-во символов в правом слове
        private static string _strRight = ""; // правое слово

        private static string _bufWord = "";
        private static int _middleString = 0;

        //массив запоминаем максимальные подстроки
        private static List<int> _countArr = new List<int>();
        private static List<string> _resultStringArr = new List<string>();

        //находим первое слово 
        private static void SearchFirstWord()
        {
            _maxLengthLeft = 0;
            for (int i = 0; i < _words.Count; i++)
            {
                if (_words[i][_words[i].Length - 1] != _letter) continue;//если правая буква не та которую мы ищем то проходим мимо
                int bufLength = 1;
                for (int j = _words[i].Length - 2; j >= 0; j--)
                {
                    if (_words[i][j] == _letter) bufLength++;
                    else break;
                }
                if (bufLength > _maxLengthLeft && bufLength != _words[i].Length)
                {
                    _strLeft = _words[i];
                    _maxLengthLeft = bufLength;
                }
            }
        }

        //находим последнее слово
        private static void SearchLastWord()
        {
            _maxLengthRight = 0;
            for (int i = 0; i < _words.Count; i++)
            {
                if (_words[i][0] != _letter) continue;//если левая буква не та которую мы ищем то проходим мимо
                int bufLength = 1;
                for (int j = 1; j < _words[i].Length; j++)
                {
                    if (_words[i][j] == _letter) bufLength++;
                    else break;
                }
                if (bufLength > _maxLengthRight && bufLength != _words[i].Length)
                {
                    _strRight = _words[i];
                    _maxLengthRight = bufLength;
                }
            }
        }

        //количество вхождений слева
        private static int NumberOccurrencesOnLeft(string word)
        {
            int leftCount = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == _letter) leftCount++;
                else return leftCount;
            }
            return leftCount;
        }

        //количество вхождений справа
        private static int NumberOccurrencesOnRight(string word)
        {
            int reghtCount = 0;
            for (int i = word.Length - 1; i >= 0; i--)
            {
                if (word[i] == _letter) reghtCount++;
                else return reghtCount;
            }
            return reghtCount;
        }

        //алгоритм нахождения подстроки в одном слове
        private static void AlgorithmFindingSubstringInWords()
        {
            
            FileStream reader = new FileStream(_path, FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(reader);

            #region обработка данных из файла
            //записываем слова в список пока они не закончатся
            bool _wordsEnded = false;
            while (!_wordsEnded)
            {
                string buferString = read.ReadLine();
                if (String.IsNullOrEmpty(buferString))
                {
                    _wordsEnded = true;
                }
                else
                {
                    _words.Add(buferString);// записываем слово в массив если оно содержит данную букву  
                }
            }
            #endregion

            #region поиск подстроки в одном слове

            for(int i = 0; i < _words.Count; i++)
            {
                char buf = ' ';
                int amount = 1;
                for(int j = 0; j < _words[i].Length; j++)
                {
                    if(_words[i][j] != buf)
                    {
                        if(buf != ' ' && amount > _countArr[buf- 97])
                        {
                            _countArr[buf - 97] = amount;
                            _resultStringArr[buf - 97] = _words[i];
                        }
                        amount = 1;
                        buf = _words[i][j];
                    }
                    else
                    {
                        amount++;
                    }
                }
                if (buf != ' ' && amount > _countArr[buf - 97])
                {
                    _countArr[buf - 97] = amount;
                    _resultStringArr[buf - 97] = _words[i];
                }
            }

            #endregion

            read.Close();
            reader.Close();
            _words.Clear();
        }

        //очиска полей
        private static void ClearingData()
        {
            _words.Clear();
            _result = "";
            _letter = ' ';
            _maxLengthLeft = 0;
            _strLeft = "";
            _maxLengthRight = 0;
            _strRight = "";
            _bufWord = "";
            _middleString = 0;
        }

        //алгоритм нахождения подстроки во всех словах
        private static int AlgorithmFindingSubstring(char chars)
        {
            _letter = chars;
            FileStream reader = new FileStream(_path, FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(reader);

            #region обработка данных из файла
            //записываем слова в список пока они не закончатся
            bool _wordsEnded = false;
            while (!_wordsEnded)
            {
                string buferString = read.ReadLine();
                if (String.IsNullOrEmpty(buferString))
                {
                    _wordsEnded = true;
                }
                else
                {
                    if (buferString.Contains(_letter))
                    {
                        int amount = new Regex(_letter.ToString()).Matches(buferString).Count;
                        if (amount == buferString.Length)
                        {
                            _middleString += amount;
                            _result += buferString; // если слово состоит только из данного символа
                            _result += " + ";
                        }
                        else _words.Add(buferString);// записываем слово в массив если оно содержит данную букву но не состоит полностью из него 
                    }
                }
            }
            try
            {
                _result = _result.Substring(0, _result.Length - 3);
            }
            catch
            {

            }
            #endregion

            SearchFirstWord();
            SearchLastWord();

            #region проверка на корректность
            if (_strLeft == _strRight) // если слова одинаковы
            {
                // ищем кол-во таких слов в массиве
                int _wordsAmount = 0;
                for (int i = 0; i < _words.Count; i++)
                {
                    if (_words[i] == _strLeft)
                    {
                        _wordsAmount++;
                    }
                }
                if (_wordsAmount < 2) // если таких слов меньше двух
                {
                    _bufWord = _strLeft; // запоминаем это слово 
                    _words.Remove(_strLeft);// удаляем это слово из массива

                    _strRight = "";
                    _strLeft = "";

                    // заново ищем крайние слова
                    SearchFirstWord();
                    SearchLastWord();

                    // ищем с какой стороны больше символов у буферного слова
                    int leftCount = NumberOccurrencesOnLeft(_bufWord);
                    int reghtCount = NumberOccurrencesOnRight(_bufWord);

                    //находим количество идущих подря символов в новых словах
                    int newLeftCount = NumberOccurrencesOnLeft(_strRight);
                    int newReghtCount = NumberOccurrencesOnRight(_strLeft);

                    //если новое крайнее слева слово + старое крайнее справа слово >=
                    //новое крайнее справа слово + старое крайнее слева слово
                    if (newReghtCount + leftCount >= newLeftCount + reghtCount)
                        _strRight = _bufWord;
                    else _strLeft = _bufWord;
                }
            }
            #endregion

            #region запись результата
            int SubstringLength = NumberOccurrencesOnRight(_strLeft) + _middleString + NumberOccurrencesOnLeft(_strRight);
            if (_letter != ' ' && SubstringLength > _countArr[_letter - 97])
            {
                _countArr[_letter - 97] = SubstringLength;
                if (_strLeft != "" && (_result != "" || _strRight != "")) _strLeft += " + ";
                if (_strRight != "" && _result != "") _result += " + ";
                _resultStringArr[_letter - 97] = _strLeft+ _result+ _strRight;
            }
            #endregion
            read.Close();
            reader.Close();
            return SubstringLength;
        }

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

            //заполняем массив 0
            for (int i = 0; i < 26; i++)
            {
                _countArr.Add(0);
                _resultStringArr.Add("");
            }

            bool exitProgramm = false;
            while (!exitProgramm)
            {

                #region опрос на счет конкретной буквы
                Console.Write("Если хотите задать определенный символ то введите [y] :");
                string answer = Console.ReadLine();
                if (answer == "y" || answer == "Y")
                {
                    //просим ввести букву пока пользователь не введет правильно
                    bool exit = true;
                    while (exit)
                    {
                        try
                        {
                            Console.Write("Введите из какой буквы вы хотите получить подстроку: ");
                            _letter = Convert.ToChar(Console.ReadLine().ToLower()); // буква 
                            if (Char.IsLetter(_letter) && _letter >= 'a' && _letter <= 'z')
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
                }
                #endregion

                AlgorithmFindingSubstringInWords();


                if (_letter == ' ')
                {
                    for (char chars = 'a'; chars <= 'z'; chars++)
                    {
                        AlgorithmFindingSubstring(chars);
                        ClearingData();
                    }
                    Console.WriteLine("\n\n////////////ОТВЕТ////////////");
                    for (int j = 0; j < _countArr.Count; j++)
                    {
                        if(_countArr[j] == _countArr.Max())
                        {
                            Console.WriteLine($"Буква: {Convert.ToChar('a'+ j)}");
                            Console.WriteLine($"Результат: { _resultStringArr[j]}");
                            Console.WriteLine($"Max длинна: { _countArr[j]}");
                            Console.WriteLine("/////////////////////////////\n");
                        }
                    }               
                }
                else
                {
                    Console.WriteLine("\n\n////////////ОТВЕТ////////////");
                    Console.WriteLine($"Буква: {_letter}");
                    Console.WriteLine($"Результат: { _resultStringArr[_letter-97]}");
                    Console.WriteLine($"Max длинна: { _countArr[_letter - 97]}");
                    Console.WriteLine("/////////////////////////////\n");
                }

                ClearingData();
                Console.Write("Если хотите выйти из программы введите [y] : ");
                answer = Console.ReadLine();
                if (answer == "y" || answer == "Y") exitProgramm = true;
                Console.Clear();
            }
        }
    }
}