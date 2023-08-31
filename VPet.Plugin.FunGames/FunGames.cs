using System.Windows;
using System.Windows.Controls;
using LinePutScript.Localization.WPF;
using VPet_Simulator.Windows.Interface;

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
                        ticTacToeboard.Show();
                    }
                    catch
                    {
                        ticTacToeboard = new TicTacToeBoard(this.MW);
                        ticTacToeboard.difficulty = gameDifficulty;
                        ticTacToeboard.Show();
                    }
                }
                else if (gameInt == 2)
                {
                    try
                    {
                        hangmanBoard.Show();
                    }
                    catch
                    {
                        hangmanBoard = new HangmanBoard(this.MW);
                        hangmanBoard.Show();
                    }
                }
            });

            return button;
        }
    }
}