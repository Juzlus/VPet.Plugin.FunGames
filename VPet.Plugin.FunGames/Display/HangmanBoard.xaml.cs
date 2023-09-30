using LinePutScript.Localization.WPF;
using Panuon.WPF.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VPet_Simulator.Core;
using VPet_Simulator.Windows.Interface;

namespace VPet.Plugin.FunGames
{
    public partial class HangmanBoard : WindowX
    {
        private FunGames games = null;

        private int lives = 5;
        private int maxLives = 5;
        private bool isOnlyCustom = false;
        private double chance_win = 0.8;
        private double chance_loss = 0.8;
        private double chance_start = 0.3;
        private double chance_correct = 0.15;
        private double chance_incorrect = 0.15;

        private Random rand = null;
        private string chosenWord = null;
        private char[] letters = null;
        private int correctChar = 0;

        private List<string> customWords = new List<string>();
        private List<Button> charButtons = new List<Button>();
        private DialogueForHamgman dialogue = new DialogueForHamgman();

        public HangmanBoard(FunGames games)
        {
            this.games = games;
            this.ImportConfig();
            this.maxLives = this.lives;
            this.ImportCustomWords();
            this.InitializeComponent();
            this.GetRandomWord();
        }

        private void ImportConfig()
        {
            try
            {
                string[] lines = File.ReadAllLines(this.games.path + "\\config\\Hangman.lps");
                if (lines.Length <= 0) return;

                foreach (string line in lines)
                {
                    string name = line.Split('=')[0];
                    string value = line.Split('=')[1];
                    if (name == null || value == null) continue;

                    if (name == "lives")
                        this.lives = int.Parse(value);
                    else if (name == "isOnlyCustom")
                        this.isOnlyCustom = bool.Parse(value);
                    else if (name == "chance_win")
                        this.chance_win = double.Parse(value);
                    else if (name == "chance_loss")
                        this.chance_loss = double.Parse(value);
                    else if (name == "chance_start")
                        this.chance_start = double.Parse(value);
                    else if (name == "chance_correct")
                        this.chance_correct = double.Parse(value);
                    else if (name == "chance_incorrect")
                        this.chance_incorrect = double.Parse(value);
                }
            }
            catch (IOException e)
            {
            }
        }

        private void ImportCustomWords()
        {
            try
            {
                string[] lines = File.ReadAllLines(this.games.path + "\\config\\Hangman_custom.txt");
                if (lines.Length <= 0) return;

                foreach (string line in lines)
                    this.customWords.Add(line);
            }
            catch (IOException e)
            {
            }
        }

        async private void GetRandomWord()
        {
            Hook_phase.Source = null;
            tbTalk.IsEnabled = true;
            SendButton.IsEnabled = true;

            ChosenWordLabel.Children.Clear();
            inCorrectWordLabel.Children.Clear();

            this.rand = new Random();
            this.games.SendRandomMsg(this.dialogue.start, this.chance_start);

            this.correctChar = 0;
            this.lives = this.maxLives;

            string word = string.Empty;

            if (this.isOnlyCustom == true) {
                int index = this.rand.Next(this.customWords.Count);
                if(index >= 0) word = this.customWords[index];
            }
            else
            {
                int index = this.rand.Next(this.dialogue.words.Count + this.customWords.Count);
                if (index <= this.dialogue.words.Count)
                    word = this.dialogue.words[index];
                else
                    word = this.customWords[index - this.dialogue.words.Count];
            }

            await Task.Delay(200);
            this.chosenWord = word != string.Empty ? word.Replace("{Name}", this.games.MW.Main.Core.Save.Name).Replace(" ", "") : "Sun".Translate();
            this.chosenWord = this.chosenWord.Translate().ToUpper();
            this.letters = this.chosenWord.ToCharArray();

            this.charButtons.Clear();
            foreach (char c in this.letters)
            {
                Button newLetter = new Button();
                newLetter.Content = "?";
                this.charButtons.Add(newLetter);
                newLetter.Style = (Style) this.FindResource("WordLetters");

                ChosenWordLabel.Children.Add(newLetter);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.games.hangmanBoard = (HangmanBoard) null;
            this.Close();
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void WindowMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void CheckWin()
        {
            if(lives <= 0)
            {
                tbTalk.IsEnabled = false;
                SendButton.IsEnabled = false;
                this.games.SendRandomMsg(this.dialogue.loss, this.chance_loss);

                int i = 0;
                foreach (char c in this.letters)
                {
                    if(charButtons[i]?.Content?.ToString() == "?")
                        charButtons[i].Content = Convert.ToString(c);
                    i++;
                }
            }
            else if(this.correctChar == charButtons.Count)
            {
                tbTalk.IsEnabled = false;
                SendButton.IsEnabled = false;
                this.games.SendRandomMsg(this.dialogue.win, this.chance_win);
            }
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tbTalk.Text))
                return;

            string playerLetter = tbTalk.Text[0].ToString().ToUpper();
            tbTalk.Text = "";

            int i = 0;
            bool isAnyCorrect = false;
            foreach (char c in this.letters)
            {
                if(playerLetter == Convert.ToString(c))
                {
                    this.games.SendRandomMsg(this.dialogue.correctChar, this.chance_correct);
                    if (charButtons[i].Content.ToString() == "?")
                    {
                        this.correctChar++;
                        charButtons[i].Content = c;
                    }
                    isAnyCorrect = true;
                }
                i++;
            }

            if(!isAnyCorrect) {
                this.lives--;
                this.games.PlaySFX(1);
                this.games.SendRandomMsg(this.dialogue.incorrectChar, this.chance_incorrect);
                this.games.PlayAnimation("fungames.building", GraphInfo.AnimatType.Single);

                Button newInCorrectLetter = new Button();
                newInCorrectLetter.Content = playerLetter;
                newInCorrectLetter.Style = (Style) this.FindResource("inCorrectWordLetters");

                inCorrectWordLabel.Children.Add(newInCorrectLetter);

                string imagePath = this.games.path + "\\image\\hook\\Hook_phase_" + this.lives.ToString() + ".png";
                if (File.Exists(imagePath)) {
                    BitmapImage image = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

                    ImageSource imageSource = image;
                    Hook_phase.Source = imageSource;
                }
            }

            this.CheckWin();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            this.GetRandomWord();
        }

        private void tbTalk_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SendMessage_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
