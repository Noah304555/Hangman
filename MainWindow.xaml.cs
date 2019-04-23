﻿
/*
 * Conner Warboys
 * April 18th
 * Hangman Summative #3
 * ICS3U
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _304555Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        string word = "";
        int randNumber = 0;
        string LetterGuess = "";
        bool GameOver = false;
        int RemainingGuesses = 4;
        public MainWindow()
        {
            InitializeComponent();
            startGame(true);
            randNumber = random.Next(1, 11);
            //MessageBox.Show(randNumber.ToString());

            System.IO.StreamReader sr = new System.IO.StreamReader("Words.txt");

            int counter = 0;
            string temp = "";
            while (counter != randNumber)
            {
                temp = sr.ReadLine();
                counter++;
                word = temp;

            }//end while
            //MessageBox.Show(temp);

            string hiddenword = "";
            //MessageBox.Show(word.Length.ToString());
            for (int i = 0; i < word.Length; i++)
            {
                hiddenword += "_ ";
                Console.WriteLine(i.ToString());
            }//end loop
            txbWord.Text = hiddenword;

        }

        private void BtnWord_Click(object sender, RoutedEventArgs e)
        {
            LetterGuess = txtInput.Text.ToLower();

            string NewOutput = "";

            bool incorect = true;


            for (int i = 0; i < word.Length; i++)
            {
                string oldLetter = txbWord.Text.ToString();
                string newLetter = word.Substring(i, 1);
                if (newLetter == LetterGuess)
                {
                    NewOutput += LetterGuess + " ";
                    incorect = false;
                }
                else
                {
                    NewOutput += oldLetter.Substring(i * 2, 2);
                }
            }//end for loop

            txbWord.Text = NewOutput;
            lblLetters.Content += LetterGuess;

            if (incorect == true)
            {
                string temp = lblLivesRemaining.Content.ToString();
                RemainingGuesses--;
                lblLivesRemaining.Content = "Lives left: " + RemainingGuesses;

                if (RemainingGuesses == 0)
                {
                    MessageBox.Show("You lose" + "\n" + "Play Again");
                    GameOver = true;
                    startGame(GameOver);
                }
            }//end if

            bool test = false;
            CheckForWin(test);

        }

        private bool CheckForWin(bool win)
        {
            if (!txbWord.Text.ToString().Contains("_"))
            {
                MessageBox.Show("You Win!!!");
                GameOver = true;
                startGame(GameOver);
                return win = true;
            }
            else
            {
                return win = false;
            }//end if

        }

        private bool startGame(bool test)
        {
            if (txbWord.Text.ToString() == word || GameOver == true)
            {
                randNumber = random.Next(1, 11);

                System.IO.StreamReader streamReader = new System.IO.StreamReader("Words.txt");

                int counter = 0;
                while (counter != randNumber)
                {
                    string temp = streamReader.ReadLine();
                    counter++;
                    word = temp;
                }//end while

                string hiddenWord = "";
                for (int i = 0; i < word.Length; i++)
                {
                    hiddenWord += "_ ";
                    Console.WriteLine(i.ToString());
                }

                txbWord.Text = hiddenWord;
                lblLivesRemaining.Content = "Lives left : 4";
                RemainingGuesses = 4;
                lblLetters.Content = "";
                GameOver = false;
                return test = true;
            }
            else
            {
                return test = false;
            }//end if
        }
    }
}