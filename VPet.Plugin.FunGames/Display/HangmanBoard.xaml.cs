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

namespace VPet.Plugin.FunGames
{
    public partial class HangmanBoard : WindowX
    {
        Random rand;
        private string path;
        private int lives = 5;
        private string chosenWord;
        private char[] letters;
        private int correctChar = 0;
        private List<Button> charButtons = new List<Button>();

        SoundPlayer soundPlayer;
        public FunGames mainGames;
        DialogueForHamgman dialogue = new DialogueForHamgman();

        public HangmanBoard()
        {
            this.InitializeComponent();
            this.GetRandomWord();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                if (assembly.GetName().Name == "VPet.Plugin.FunGames")
                {
                    path = Directory.GetParent(System.IO.Path.GetDirectoryName(assembly.Location)).FullName;
                    soundPlayer = new SoundPlayer(path + "\\audio\\hammer.wav");
                }

            SendButton.Content = "Send".Translate();
            RestartButton.Content = "Restart".Translate();
            ExitButton.Content = "Exit".Translate();
        }

        async private void GetRandomWord()
        {
            Hook_phase.Source = null;
            tbTalk.IsEnabled = true;
            SendButton.IsEnabled = true;

            ChosenWordLabel.Children.Clear();
            inCorrectWordLabel.Children.Clear();

            this.rand = new Random();
            this.SendRandomMsg(dialogue.start, 0.3);

            this.lives = 5;
            this.correctChar = 0;
            int index = this.rand.Next(dialogue.words.Count);
            await Task.Delay(200);
            this.chosenWord = index >= 0 ? dialogue.words[index].Replace("{Name}", this.mainGames.MW.Main.Core.Save.Name).ToUpper() : "SUN";
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
                this.SendRandomMsg(dialogue.loss, 0.8);

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
                this.SendRandomMsg(dialogue.win, 0.8);
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
                    this.SendRandomMsg(dialogue.correctChar, 0.15);
                    this.correctChar++;
                    charButtons[i].Content = c;
                    isAnyCorrect = true;
                }
                i++;
            }

            if(!isAnyCorrect) {
                this.lives--;
                PlaySFX();

                this.SendRandomMsg(dialogue.incorrectChar, 0.15);
                this.PlayAnimation("building", GraphInfo.AnimatType.Single);

                Button newInCorrectLetter = new Button();
                newInCorrectLetter.Content = playerLetter;
                newInCorrectLetter.Style = (Style) this.FindResource("inCorrectWordLetters");

                inCorrectWordLabel.Children.Add(newInCorrectLetter);

                string imagePath = path + "\\image\\Hook_phase_" + (5 - this.lives).ToString() + ".png";
                BitmapImage image = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

                ImageSource imageSource = image;
                Hook_phase.Source = imageSource;
            }

            this.CheckWin();
        }

        private void SendRandomMsg(List<string> dialogue, double chance)
        {
            if (rand.NextDouble() < 1 - chance) return;

            int index = this.rand.Next(dialogue.Count);
            if (index < 0) return;
            string msgContent = dialogue[index];
            if (msgContent == null) return;

            try
            {
                this.mainGames?.SendMsg(msgContent);
            }
            catch { };
        }

        private void PlayAnimation(string graphName, GraphInfo.AnimatType animatType)
        {
            try
            {
                this.mainGames?.PlayAnim(graphName, animatType);
            }
            catch { };
        }

        private void PlaySFX()
        {
            try
            {
                this.soundPlayer.Play();
            }
            catch { }
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

        private void tbTalk_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
