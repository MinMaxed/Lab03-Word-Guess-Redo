using System;
using System.IO;

namespace WordGuessGame2
{
    class Program
    {

        static string path = "../../../WordGame.txt";

        static void Main(string[] args)
        {
            //string[] realWordBank = .Split();
            string[] wordBank = { "for", "demi", "nachos" };
            DestroyFile();
            CreateFile();

            Console.WriteLine("Hello World!");

            bool gameplay = true;
            while (gameplay)
            {
                GameMenu();
                Int32.TryParse(Console.ReadLine(), out int choice);

                switch (choice)
                {
                    case 1:
                        {
                            Play();
                            break;
                        }
                    case 2:
                        {
                            Admin();
                            break;
                        }
                    case 3:
                        {
                            gameplay = false;
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }

        static void GameMenu()
        {
            Console.WriteLine("Welcome to the Word Guess Game!");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) Play Game");
            Console.WriteLine("2) Admin");
            Console.WriteLine("3) Exit");
        }

        static string randomWord(string[] words)
        {
            Random random = new Random();
            int randomWord = random.Next(0, words.Length);
            string gameWord = words[randomWord];
            string[] displayWord = new string[gameWord.Length];

            return gameWord;
        }

        static char[] randomWordArrayed(string word)
        {
            string displayWord = word;
            char[] wordarr = displayWord.ToCharArray();

            return wordarr;
        }

        static string[] DashedWord(char[] wordArr)
        {
            string[] mysteryWord = new string[wordArr.Length];

            for (int i = 0; i < wordArr.Length; i++)
            {
                mysteryWord[i] = " _ ";
            }
            return mysteryWord;
        }

        static void showTheWord(string[] dashes)
        {
            for (int i = 0; i < dashes.Length; i++)
            {
                Console.Write(dashes[i]);
            }
        }

        //takes random word and displays the " _ " for each letter in the word
        static void Play()
        {
            string[] words = CreateWordList();
            //DashedWord(words);
            string mysteryWord = randomWord(words);
            char[] wordArray = randomWordArrayed(mysteryWord);
            string[] gameWord = DashedWord(wordArray);
            bool gameplay = true;

            while (gameplay)
            {
                Console.Clear();
                showTheWord(gameWord);
                Console.WriteLine(" Guess a letter");
                string userGuess = Console.ReadLine();
                userGuess = userGuess.ToLower();

                for (int i = 0; i < wordArray.Length; i++)
                {
                    if (userGuess == wordArray[i].ToString())
                    {
                        gameWord[i] = userGuess;
                        Console.Write(gameWord[i]);
                    }
                    else if (userGuess != wordArray[i].ToString())
                    {
                        Console.Write(" _ ");
                    }
                    gameplay = SolutionCheck(gameWord, wordArray);
                }
            }
            Console.Clear();
            Console.WriteLine($"Congrats you got '{mysteryWord}' ");
        }
        static bool SolutionCheck(string[] guessingArray, char[] solutionArray)
        {
            for (int i = 0; i < solutionArray.Length; i++)
            {
                if (guessingArray[i] != solutionArray[i].ToString())
                {
                    return true;
                }
            }
            return false;
        }


        //-----------CRUD-----------------


        static void CreateFile()
        {
            DestroyFile();
            string[] wordBank = { "for", "demi", "nachos" };

            if (!File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    try
                    {
                        foreach (string word in wordBank)
                        {
                            sw.WriteLine(word);
                        }
                    }
                    finally
                    {
                        sw.Close();
                    }
                }
            }
        }

        static string[] CreateWordList()
        {
            try
            {
                using (StreamReader read = File.OpenText(path))
                {
                    string word = "";
                    string words = "";
                    string[] wordBank;
                    while ((word = read.ReadLine()) != null)
                    {
                        words = word + " ";
                    }
                    wordBank = words.Split(" ");
                    return wordBank;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("something went wrong. make sure the file exists and contains words");
                throw;
            }

        }

        static void ReadWords()
        {
            using (StreamReader read = File.OpenText(path))
            {
                string line = "";
                while ((line = read.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            try
            {
                string[] myText = File.ReadAllLines(path);

                foreach (string value in myText)
                {
                    Console.WriteLine(value);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Could not read all lines");
            }
        }

        //this is allowing the user to add lines to the main file
        static void AddWords()
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(Console.ReadLine());
            }
        }

        static void DeleteWords()
        {
            string[] wordBank = CreateWordList();


        }


        static void DestroyFile()
        {
            File.Delete(path);
        }


        static void Admin()
        {

            bool adminMenu = true;



            while (adminMenu)
            {

                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1) View Words");
                Console.WriteLine("2) Add a Word");
                Console.WriteLine("3) Delete a Word");
                Console.WriteLine("4) Main Menu");

                Int32.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        {
                            ReadWords();
                            break;
                        }

                    case 2:
                        {
                            AddWords();
                            break;
                        }

                    case 3:
                        {
                            DeleteWords();
                            break;
                        }

                    case 4:
                        {
                            adminMenu = false;
                            break;
                        }
                }
            }
        }
    }
}

