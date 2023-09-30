using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using LinePutScript.Localization.WPF;
using VPet_Simulator.Core;
using VPet_Simulator.Windows.Interface;

namespace VPet.Plugin.FunGames
{
    public class FunGames : MainPlugin
    {
        public string path = null;
        public petBoard petBoard = null;
        public winSettings settingsPanel = null;
        public HangmanBoard hangmanBoard = null;
        public TicTacToeBoard ticTacToeboard = null;
        public MillionaireBoard millionaireBoard = null;
        public MinesweeperBoard minesweeperBoard = null;

        private Random rand = null;
        private SoundPlayer soundPlayer = null;
        private string[] songList = { "tictactoe_chalk", "hangman_hammer", "millionaire_play", "millionaire_selected", "millionaire_win", "millionaire_lose", "millionaire_50-50",
            "minesweeper_explode", "minesweeper_tick", "minesweeper_win"
        };

        public override string PluginName => nameof(FunGames);

        public FunGames(IMainWindow mainwin)
          : base(mainwin)
        {
        }

        public override void LoadDIY()
        {
            this.path = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName;
            this.CreateMenuButton();

            this.petBoard = new petBoard();
            this.MW.Main.UIGrid.Children.Insert(0, this.petBoard);
        }

        private void CreateMenuButton()
        {
            MenuItem buttonsMenu = this.CreateMenuItem("Fun Games");

            MenuItem SettingButton = this.CreateMenuItem("Settings", 0);
            MenuItem TicTacToeButton = this.CreateMenuItem("Tic Tac Toe", 1);
            MenuItem HangmanButton = this.CreateMenuItem("Hangman", 2);
            MenuItem millionaireButton = this.CreateMenuItem("Millionaire", 3);
            MenuItem minesweeperButton = this.CreateMenuItem("Minesweeper", 4);

            buttonsMenu.Items.Add(SettingButton);
            buttonsMenu.Items.Add(TicTacToeButton);
            buttonsMenu.Items.Add(HangmanButton);
            buttonsMenu.Items.Add(millionaireButton);
            buttonsMenu.Items.Add(minesweeperButton);
            this.MW.Main.ToolBar.MenuDIY.Items.Add(buttonsMenu);

            MenuItem MODConfigButton = this.CreateMenuItem(nameof(FunGames), 0);
            MenuItem menuModConfig = this.MW.Main.ToolBar.MenuMODConfig;
            menuModConfig.Visibility = Visibility.Visible;
            menuModConfig.Items.Add(MODConfigButton);
        }

        private MenuItem CreateMenuItem(string buttonName, int gameId = -1)
        {
            MenuItem button = new MenuItem()
            {
                Header = buttonName.Translate(),
                HorizontalContentAlignment = HorizontalAlignment.Center
            };

            if(gameId == -1) return button;
            button.Click += (RoutedEventHandler)((s, e) =>
            {
                this.ShowPanel(gameId);
            });

            return button;
        }

        public override void Setting()
        {
            this.ShowPanel(0);
        }

        public void ShowPanel(int gameId)
        {
            if (gameId == 0)
            {
                if (this.settingsPanel == null)
                {
                    this.settingsPanel = new winSettings(this);
                    this.settingsPanel.Show();
                }
                else
                    this.settingsPanel.Topmost = true;
            }
            else if (gameId == 1)
            {
                if (this.ticTacToeboard == null)
                {
                    this.ticTacToeboard = new TicTacToeBoard(this);
                    this.ticTacToeboard.Show();
                }
                else
                    this.ticTacToeboard.Topmost = true;
            }
            else if(gameId == 2)
            {
                if (this.hangmanBoard == null)
                {
                    this.hangmanBoard = new HangmanBoard(this);
                    this.hangmanBoard.Show();
                }
                else
                    this.hangmanBoard.Topmost = true;
            }
            else if (gameId == 3)
            {
                if (this.millionaireBoard == null)
                {
                    this.millionaireBoard = new MillionaireBoard(this);
                    try
                    {
                        this.millionaireBoard.Show();
                        this.petBoard.SetImage("Millionaire_petDesk", true);
                    } catch (Exception e)
                    {
                        this.millionaireBoard = null;
                    }
                }
                else
                    this.millionaireBoard.Topmost = true;
            }
            else if (gameId == 4)
            {
                if (this.minesweeperBoard == null)
                {
                    this.minesweeperBoard = new MinesweeperBoard(this);
                    this.petBoard.SetImage("Minesweeper_petDesk", true);
                    this.minesweeperBoard.Show();
                }
                else
                    this.minesweeperBoard.Topmost = true;
            }
        }

        public void PlaySFX(int soundId)
        {
            try
            {
                this.soundPlayer = new SoundPlayer(this.path + "\\audio\\" + this.songList[soundId] + ".wav");
                this.soundPlayer.Play();
            }
            catch { }
        }

        public void SendMsg(string msg)
        {
            try
            {
                this.MW.Main.Say(msg.Translate());
            }
            catch { };
        }

        public void SendRandomMsg(List<string> dialogue, double chance)
        {
            this.rand = new Random();
            if (this.rand.NextDouble() < 1 - chance) return;

            int index = this.rand.Next(dialogue.Count);
            if (index < 0) return;
            string msgContent = dialogue[index];
            if (msgContent == null) return;

            try
            {
                this.MW.Main.Say(msgContent.Translate());
            }
            catch { };
        }

        public void PlayAnimation(string graphName, GraphInfo.AnimatType animatType)
        {
            try
            {
                IGraph graph = this.MW.Main.Core.Graph.FindGraph(graphName, animatType, GameSave.ModeType.Happy);
                if (graph == null) return;
                this.MW.Main.Display(graph, (Action)(() => {
                    this.MW.Main.DisplayToNomal();
                }));
            }
            catch { };
        }
    }
}