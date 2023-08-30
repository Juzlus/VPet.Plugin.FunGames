using LinePutScript.Localization.WPF;
using System;
using System.Windows;
using System.Windows.Controls;
using VPet_Simulator.Core;
using VPet_Simulator.Windows.Interface;
using static VPet_Simulator.Core.GraphInfo;

namespace VPet.Plugin.FunGames
{
    public class FunGames : MainPlugin
    {
        public enum GameType
        {
            None,
            TicTacToe,
            Hangman,
            Millionaire,
            Battleship
        }

        HangmanBoard hangmanBoard;
        TicTacToeBoard ticTacToeboard;

        public override string PluginName => nameof(FunGames);

        public FunGames(IMainWindow mainwin)
          : base(mainwin)
        {
        }

        public override void LoadDIY()
        {
            this.CreateMenuButton();
        }

        public void SendMsg(string msg)
        {
            try {
                this.MW.Main.MsgBar.Show(this.MW.Main.Core.Save.Name, msg.Translate());
            } catch { };
        }

        public void PlayAnim(string graphName, AnimatType animatType)
        {
            IGraph graph = this.MW.Main.Core.Graph.FindGraph(graphName, animatType, GameSave.ModeType.Happy);
            if(graph == null) return;
            this.MW.Main.Display(graph, (Action)(() => {
                this.MW.Main.DisplayToNomal();
            }));
        }

        private void CreateMenuButton()
        {
            MenuItem buttonsMenu = this.CreateMenuItem("Fun Games");

            MenuItem TicTacToeButton = this.CreateMenuItem("Tic Tac Toe");
            MenuItem TicTacToeEasyButton = this.CreateMenuItem("Easy", (int) GameType.TicTacToe, 0);
            MenuItem TicTacToeMediumButton = this.CreateMenuItem("Normal", (int) GameType.TicTacToe, 1);
            MenuItem TicTacToeHardButton = this.CreateMenuItem("Hard", (int) GameType.TicTacToe, 2);
            TicTacToeButton.Items.Add(TicTacToeHardButton);
            TicTacToeButton.Items.Add(TicTacToeMediumButton);
            TicTacToeButton.Items.Add(TicTacToeEasyButton);

            MenuItem HangmanButton = this.CreateMenuItem("Hangman", (int) GameType.Hangman);

            buttonsMenu.Items.Add(TicTacToeButton);
            buttonsMenu.Items.Add(HangmanButton);
            this.MW.Main.ToolBar.MenuDIY.Items.Add(buttonsMenu);
        }

        private MenuItem CreateMenuItem(string buttonName, int gameInt = 0, int gameDifficulty = 0)
        {
            MenuItem button = new MenuItem()
            {
                Header = buttonName.Translate(),
                HorizontalContentAlignment = HorizontalAlignment.Center
            };

            if(gameInt == (int) GameType.None) return button;
            button.Click += (RoutedEventHandler)((s, e) =>
            {
                if (gameInt == 1)
                {
                    try
                    {
                        ticTacToeboard.difficulty = gameDifficulty;
                        ticTacToeboard.mainGames = this;
                        ticTacToeboard.Show();
                    }
                    catch
                    {
                        ticTacToeboard = new TicTacToeBoard();
                        ticTacToeboard.difficulty = gameDifficulty;
                        ticTacToeboard.mainGames = this;
                        ticTacToeboard.Show();
                    }
                }
                else if (gameInt == 2)
                {
                    try
                    {
                        hangmanBoard.mainGames = this;
                        hangmanBoard.Show();
                    }
                    catch
                    {
                        hangmanBoard = new HangmanBoard();
                        hangmanBoard.mainGames = this;
                        hangmanBoard.Show();
                    }
                }
            });

            return button;
        }
    }
}