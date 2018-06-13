using System;
using Xunit;
using WordGuessGame2;
using System.IO;
using System.Linq;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TestFileCreation()
        {
            string[] wordBank = { "for", "demi", "nachos", "coffee", "babbbbbbooooooon" };
            Program.CreateFile(wordBank);
            Assert.True(File.Exists(Program.path));
        }

        [Theory]
        [InlineData("frog")]
        [InlineData("cookie")]
        public void TestAddWord( string testWord)
        {
            string[] wordBank = { "for", "demi", "nachos", "coffee", "babbbbbbooooooon" };
            Program.CreateFile(wordBank);
            Program.AddWords(testWord);
            string[] wordList = Program.CreateWordList();
            Assert.Contains(testWord, wordList);

        }

        [Theory]
        [InlineData("demi")]
        public void TestDeleteWord(string testWord)
        {
            string[] wordBank = { "for", "demi", "nachos", "coffee", "babbbbbbooooooon" };
            Program.CreateFile(wordBank);
            Program.DeleteWords(testWord);
            string[] wordList = Program.CreateWordList();
            Assert.DoesNotContain(testWord, wordList);

        }

        [Fact]
        public void TestFileDeletion()
        {
            string[] wordBank = { "for", "demi", "nachos", "coffee", "babbbbbbooooooon" };
            Program.CreateFile(wordBank);
            Program.DestroyFile();
            Assert.False(File.Exists(Program.path));

        }

        [Fact]
        public void TestReadAllWords()
        {
            //Assert.Equal("Could not read all lines", Program.ReadWords());
            string[] wordBank = { "for", "demi", "nachos", "coffee", "babbbbbbooooooon" };
            Program.CreateFile(wordBank);
            Program.ReadWords();
            Assert.Equal("complete", Program.ReadWords());

        }


        [Theory]
        [InlineData("demi", "i", true)]
        [InlineData("demi", "o", false)]

        public void TestLetterExistsInWord(string testWord, string testGuess, bool testResult)
        {
           Assert.Equal(testResult, Program.CheckGuess(testWord, testGuess));

        }

    }
}
