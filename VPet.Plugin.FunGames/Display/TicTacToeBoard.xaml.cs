using Panuon.WPF.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LinePutScript.Localization.WPF;
using VPet_Simulator.Core;
using System.Windows.Threading;
using System.Media;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using VPet_Simulator.Windows.Interface;

namespace VPet.Plugin.FunGames
{
    public partial class TicTacToeBoard : WindowX
    {
        private enum Symbols { X, O };
        private bool isPlayerMove = true;
        public int difficulty = 0;

        Random rand;
        List<Button> buttons;
        private DispatcherTimer inactivityTimer;

        int playerWins = 0;
        int computerWins = 0;

        string path;
        DialogueForTicTacToe dialogue = new DialogueForTicTacToe();

        IMainWindow mw;
        SoundPlayer soundPlayer;
        private Brush mainColor = new SolidColorBrush(Color.FromArgb(0xFF, 0xD5, 0xD5, 0xD5));
        private Brush winColor = new SolidColorBrush(Color.FromArgb(0xFF, 0x80, 0xC8, 0x58));

        public TicTacToeBoard(IMainWindow mw)
        {
            this.mw = mw;
            this.InitializeComponent();
            this.path = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName;
            this.soundPlayer = new SoundPlayer(this.path + "\\audio\\chalk.wav");

            this.ResetGame();

            RestartB.Content = "Restart".Translate();
            ExitB.Content = "Exit".Translate();

            this.inactivityTimer = new DispatcherTimer();
            this.inactivityTimer.Interval = TimeSpan.FromSeconds(30);
            this.inactivityTimer.Tick += this.InactivityTimer;
        }

        private async void ResetGame()
        {
            rand = new Random();
            buttons = new List<Button> { Button00, Button01, Button02, Button10, Button11, Button12, Button20, Button21, Button22 };
            foreach (Button button in this.buttons)
            {
                button.Content = null;
                button.IsEnabled = true;
                button.Foreground = mainColor;
            }
            SendRandomMsg(dialogue.start, 0.4);

            if(!this.isPlayerMove)
                ComputerMove();

            await Task.Delay(200);
            PlayerScore.Text = "You".Translate() + ": " + this.playerWins.ToString();
            ComputerScore.Text = this.mw?.Core?.Save?.Name + ": " + this.computerWins.ToString();
        }

        private void PlayerMove(object sender, RoutedEventArgs e)
        {
            this.inactivityTimer.Start();
            Button button = (Button) sender;
            if(button.Content != null || !this.isPlayerMove) return;
            this.PlaySFX();
            this.DisableButton(button);
            this.ComputerMove();
        }

        private async void ComputerMove()
        {
            this.inactivityTimer.Stop();
            await Task.Delay(2800);

            if (this.buttons.Count <= 0) return;
            this.PlayAnimation("chalking", GraphInfo.AnimatType.Single);
            SendRandomMsg(dialogue.thinking, 0.1);

            int index = this.rand.Next(this.buttons.Count);
            this.PlaySFX();

            if (difficulty == 0) {
                this.DisableButton(this.buttons[index]);
                return;
            }

            else if(Button11.Content == null) {
                this.DisableButton(Button11);
                return;
            }

            else if(difficulty == 1) {
                Button newButton = GetOffensiveOrDefensiveButton(Enum.GetName(typeof(Symbols), Symbols.O));
                this.DisableButton(newButton != null ? newButton : this.buttons[index]);
                return;
            }

            else if(difficulty == 2) {
                Button newButton = GetOffensiveOrDefensiveButton(Enum.GetName(typeof(Symbols), Symbols.X));
                this.DisableButton(newButton != null ? newButton : this.buttons[index]);
                return;
            }
        }

        private void DisableButton(Button button)
        {
            button.Content = this.isPlayerMove ? Symbols.X : Symbols.O;
            button.IsEnabled = false;
            this.isPlayerMove = !this.isPlayerMove;
            this.buttons.Remove(button);
            this.CheckWins();
        }

        private void CheckWins()
        {
            string sX = Enum.GetName(typeof(Symbols), Symbols.X);
            string sO = Enum.GetName(typeof(Symbols), Symbols.O);

            if(buttons.Count <= 0)
            {
                DisableAllActiveButtons();
                SendRandomMsg(dialogue.draw, 0.4);
            }
            else if (
                Button00.Content?.ToString() == sX && Button01.Content?.ToString() == sX && Button02.Content?.ToString() == sX
                || Button10.Content?.ToString() == sX && Button11.Content?.ToString() == sX && Button12.Content?.ToString() == sX
                || Button20.Content?.ToString() == sX && Button21.Content?.ToString() == sX && Button22.Content?.ToString() == sX
                || Button00.Content?.ToString() == sX && Button10.Content?.ToString() == sX && Button20.Content?.ToString() == sX
                || Button01.Content?.ToString() == sX && Button11.Content?.ToString() == sX && Button21.Content?.ToString() == sX
                || Button02.Content?.ToString() == sX && Button12.Content?.ToString() == sX && Button22.Content?.ToString() == sX
                || Button00.Content?.ToString() == sX && Button11.Content?.ToString() == sX && Button22.Content?.ToString() == sX
                || Button02.Content?.ToString() == sX && Button11.Content?.ToString() == sX && Button20.Content?.ToString() == sX
            )
            {
                DisableAllActiveButtons(true);
                SendRandomMsg(dialogue.loss, 0.4);
                this.playerWins++;
                PlayerScore.Text = "You".Translate() + ": " + this.playerWins.ToString();
            }
            else if (
                Button00.Content?.ToString() == sO && Button01.Content?.ToString() == sO && Button02.Content?.ToString() == sO
                || Button10.Content?.ToString() == sO && Button11.Content?.ToString() == sO && Button12.Content?.ToString() == sO
                || Button20.Content?.ToString() == sO && Button21.Content?.ToString() == sO && Button22.Content?.ToString() == sO
                || Button00.Content?.ToString() == sO && Button10.Content?.ToString() == sO && Button20.Content?.ToString() == sO
                || Button01.Content?.ToString() == sO && Button11.Content?.ToString() == sO && Button21.Content?.ToString() == sO
                || Button02.Content?.ToString() == sO && Button12.Content?.ToString() == sO && Button22.Content?.ToString() == sO
                || Button00.Content?.ToString() == sO && Button11.Content?.ToString() == sO && Button22.Content?.ToString() == sO
                || Button02.Content?.ToString() == sO && Button11.Content?.ToString() == sO && Button20.Content?.ToString() == sO
            )
            {
                DisableAllActiveButtons(true);
                SendRandomMsg(dialogue.win, 0.4);
                this.computerWins++;
                ComputerScore.Text = this.mw.Core.Save.Name + ": " + this.computerWins.ToString();
            }
        }

        private void DisableAllActiveButtons(bool wined = false)
        {
            foreach (Button button in this.buttons)
                button.IsEnabled = false;
            buttons.Clear();

            if(!wined) return;
            this.HiglightButtons(Button00, Button01, Button02);
            this.HiglightButtons(Button10, Button11, Button12);
            this.HiglightButtons(Button20, Button21, Button22);
            this.HiglightButtons(Button00, Button10, Button20);
            this.HiglightButtons(Button01, Button11, Button21);
            this.HiglightButtons(Button02, Button12, Button22);
            this.HiglightButtons(Button00, Button11, Button22);
            this.HiglightButtons(Button02, Button11, Button20);
        }

        private void HiglightButtons(Button b1, Button b2, Button b3)
        {
            if (b1.Content?.ToString() == b2.Content?.ToString() && b2.Content?.ToString() == b3.Content?.ToString())
            {
                b1.Foreground = winColor;
                b2.Foreground = winColor;
                b3.Foreground = winColor;
            }
        }

        private void PlaySFX()
        {
            try {
                this.soundPlayer.Play();
            } catch { }
        }

        private void InactivityTimer(object sender, EventArgs e)
        {
            this.inactivityTimer.Stop();
            this.inactivityTimer.Start();
            this.SendRandomMsg(dialogue.waiting, 0.4);
        }

        private void RestartButton(object sender, RoutedEventArgs e)
        {
            this.PlaySFX();
            this.ResetGame();
        }
        
        private void ExitButton(object sender, RoutedEventArgs e)
        {
            this.PlaySFX();
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

        private void SendRandomMsg(List<string> dialogue, double chance)
        {
            if(rand.NextDouble() < 1 - chance) return;

            int index = this.rand.Next(dialogue.Count);
            if(index < 0) return;
            string msgContent = dialogue[index];
            if(msgContent == null) return;

            try
            {
                this.mw.Main.MsgBar.Show(this.mw.Main.Core.Save.Name, msgContent.Translate());
            }
            catch { };
        }

        private void PlayAnimation(string graphName, GraphInfo.AnimatType animatType)
        {
            try {
                IGraph graph = this.mw.Main.Core.Graph.FindGraph(graphName, animatType, GameSave.ModeType.Happy);
                if (graph == null) return;
                this.mw.Main.Display(graph, (Action)(() => {
                    this.mw.Main.DisplayToNomal();
                }));
            } catch { };
        }

        private Button GetOffensiveOrDefensiveButton(string symbol)
        {
            Button buttonH0 = CheckLine(Button00, Button01, Button02, symbol);
            Button buttonH1 = CheckLine(Button10, Button11, Button12, symbol);
            Button buttonH2 = CheckLine(Button20, Button21, Button22, symbol);

            Button buttonV0 = CheckLine(Button00, Button10, Button20, symbol);
            Button buttonV1 = CheckLine(Button01, Button11, Button21, symbol);
            Button buttonV2 = CheckLine(Button02, Button12, Button22, symbol);

            Button buttonD0 = CheckLine(Button00, Button11, Button22, symbol);
            Button buttonD1 = CheckLine(Button20, Button11, Button02, symbol);

            List<Button> allButtons = new List<Button> { };

            if(buttonH0 != null) allButtons.Add(buttonH0);
            if(buttonH1 != null) allButtons.Add(buttonH1);
            if(buttonH2 != null) allButtons.Add(buttonH2);

            if(buttonV0 != null) allButtons.Add(buttonV0);
            if(buttonV1 != null) allButtons.Add(buttonV1);
            if(buttonV2 != null) allButtons.Add(buttonV2);

            if(buttonD0 != null) allButtons.Add(buttonD0);
            if(buttonD1 != null) allButtons.Add(buttonD1);

            if (allButtons.Count > 0)
            {
                int index = this.rand.Next(allButtons.Count);
                return allButtons[index];
            } 
            else return null;
        }

        private Button CheckLine(Button button1, Button button2, Button button3, string symbol)
        {
            if(button1.Content?.ToString() == symbol && button2.Content?.ToString() == symbol && button3.Content == null)
                return button3;
            else if (button1.Content?.ToString() == symbol && button2.Content == null && button3.Content?.ToString() == symbol)
                return button2;
            else if (button1.Content == null && button2.Content?.ToString() == symbol && button3.Content?.ToString() == symbol)
                return button1;
            return null;
        }
    }
}