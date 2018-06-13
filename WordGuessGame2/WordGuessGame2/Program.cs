using System;
using System.IO;

namespace WordGuessGame2
{
    public class Program
    {

        public static string path = "../../../WordGame.txt";

        public static void Main(string[] args)
        {
            string[] wordBank = { "for", "demi", "nachos", "coffee", "babbbbbbooooooon" };
            DestroyFile();
            CreateFile(wordBank);

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

        public static void GameMenu()
        {
            Console.WriteLine("Welcome to the Word Guess Game!");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) Play Game");
            Console.WriteLine("2) Admin");
            Console.WriteLine("3) Exit");
        }

        //selects a word at random from the available options
        public static string randomWord(string[] words)
        {
            Random random = new Random();
            int randomWord = random.Next(0, words.Length);
            string gameWord = words[randomWord];
            string[] displayWord = new string[gameWord.Length];

            return gameWord;
        }

        //turns the word into a character array for use during gameplay to have comparisons for the user guess
        public static char[] randomWordArrayed(string word)
        {
            string displayWord = word;
            char[] wordarr = displayWord.ToCharArray();

            return wordarr;
        }

        //make the _ for the user to see
        public static string[] DashedWord(char[] wordArr)
        {
            string[] mysteryWord = new string[wordArr.Length];

            for (int i = 0; i < wordArr.Length; i++)
            {
                mysteryWord[i] = " _ ";
            }
            return mysteryWord;
        }

        //display the _ array
        public static void showTheWord(string[] dashes)
        {
            for (int i = 0; i < dashes.Length; i++)
            {
                Console.Write(dashes[i]);
            }
        }

        //takes random word and displays the " _ " for each letter in the word
        public static void Play()
        {
            string[] words = CreateWordList();
            string mysteryWord = randomWord(words);
            char[] wordArray = randomWordArrayed(mysteryWord);
            string[] gameWord = DashedWord(wordArray);
            bool gameplay = true;
            
            while (gameplay)
            {
                Console.Clear();
                showTheWord(gameWord);
                Console.WriteLine(" Guess a letter");
                string userGuess = UserGuess();
                userGuess = userGuess.ToLower();
                CheckGuess(mysteryWord, userGuess);

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

        //for unit testing
        public static bool CheckGuess(string mysteryWord, string userGuess)
        {
            bool result;
            if (mysteryWord.Contains(userGuess))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static string UserGuess()
        {
            string userGuess = Console.ReadLine();
            return userGuess;
        }

        //compares the users inputs to the solution to check if the game still needs to run or not
        public static bool SolutionCheck(string[] guessingArray, char[] solutionArray)
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

        //initiate the file at the beginning of the game
        public static void CreateFile(string[] wordBank)
        {
            DestroyFile();

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

        //converts the file to a string array for use with the game and other methods
        public static string[] CreateWordList()
        {
            try
            {
                using (StreamReader read = File.OpenText(path))
                {
                    string word = null;
                    string words = null;
                    while ((word = read.ReadLine()) != null)
                    {
                        words = words + word + " ";
                    }
                    string[] wordBank = words.Trim().Split();
                    return wordBank;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("something went wrong. make sure the file exists and contains words");
                throw;
            }

        }

        //takes the file and displays all of the words
        public static string ReadWords()
        {
            using (StreamReader read = File.OpenText(path))
            {
            }
            try
            {
                string[] myText = File.ReadAllLines(path);

                foreach (string value in myText)
                {
                    Console.WriteLine(value);
                }
                Console.WriteLine("complete");
                return "complete";
            }
            catch (Exception)
            {
                return "Could not read all lines";
            }
        }

        //this is allowing the user to add lines to the main file
        public static void AddWords(string newWord)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(newWord.ToLower());
            }
        }

        //checks to see if the user word is in the list and if so, removes it
        public static void DeleteWords(string userWord)
        {
            string[] wordBank = CreateWordList();
            string[] newArray = new string[wordBank.Length - 1];
            int counter = wordBank.Length;


            for (int i = 0; i < wordBank.Length; i++)
            {
                if (userWord.ToLower() == wordBank[i])
                {
                    continue;
                }
                else
                {
                    newArray[wordBank.Length - counter] = wordBank[i];
                    counter--;
                }
            }
            CreateFile(newArray);
        }

        public static void DestroyFile()
        {
            File.Delete(path);
        }

        //admin/CRUD menu
        public static void Admin()
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
                            Console.WriteLine("Write a word to add to the list");
                            string addingWord = Console.ReadLine();
                            AddWords(addingWord);
                            break;
                        }

                    case 3:
                        {
                            ReadWords();
                            Console.WriteLine("Pick a word to delete");
                            string deleteWord = Console.ReadLine();
                            DeleteWords(deleteWord);
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

