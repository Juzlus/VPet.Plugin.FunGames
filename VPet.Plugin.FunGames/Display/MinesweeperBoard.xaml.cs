using Panuon.WPF.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using VPet_Simulator.Core;

namespace VPet.Plugin.FunGames
{
    public partial class MinesweeperBoard : WindowX
    {
        private FunGames games = null;

        private int row = 10;
        private int col = 10;
        private int bomb = 30;
        private double cellScale = 25;
        private double chance_win = 1;
        private double chance_loss = 0.4;
        private double chance_start = 0.5;
        private double chance_wait = 0.05;
        private bool isTimerSound = true;
        private bool isMarkEnabled = true;

        private int bombExists = 0;
        private int correctCell = 0;

        private DialogueForMinesweeper dialogue = new DialogueForMinesweeper();

        private Random rand;
        private int[,] gameBoard;
        private bool isFirst = true;
        private bool isGameOver = false;
        private List<KeyValuePair<int, int>> possibleBomb = new List<KeyValuePair<int, int>>();
        private List<KeyValuePair<int, int>> bombPlaces = new List<KeyValuePair<int, int>>();
        private int bombInt = 15;

        private int timerInt = 0;
        private DispatcherTimer timer;
        private DispatcherTimer waitTimer;

        private BitmapImage cellImage = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell.png", UriKind.Relative));
        private BitmapImage[] cellNumberImages = {
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_0.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_1.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_2.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_3.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_4.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_5.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_6.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_7.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_8.png", UriKind.Relative)),
        };
        private BitmapImage cellBombImage = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_bomb.png", UriKind.Relative));
        private BitmapImage cellBombIncorrectImage = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_bomb2.png", UriKind.Relative));
        private BitmapImage cellGameOverBombImage = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_bomb3.png", UriKind.Relative));
        private BitmapImage cellUnknownImage = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_unknown.png", UriKind.Relative));
        private BitmapImage cellFlagImage = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_cell_flag.png", UriKind.Relative));

        private BitmapImage restartDefaultImage = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_restart_default.png", UriKind.Relative));
        private BitmapImage restartLoseImage = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_restart_lose.png", UriKind.Relative));
        private BitmapImage restartWinImage = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_restart_win.png", UriKind.Relative));

        private BitmapImage[] timerNumbers = {
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_0.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_1.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_2.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_3.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_4.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_5.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_6.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_7.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_8.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_9.png", UriKind.Relative)),
            new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_-.png", UriKind.Relative)),
        };

        public MinesweeperBoard(FunGames games)
        {
            this.games = games;
            this.ImportConfig();
            this.InitializeComponent();

            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromSeconds(1);
            this.timer.Tick += this.ChangeTimer;

            this.waitTimer = new DispatcherTimer();
            this.waitTimer.Interval = TimeSpan.FromSeconds(45);
            this.waitTimer.Tick += this.InactivityTimer;

            this.InitializeBoard();
            this.games.SendRandomMsg(this.dialogue.start, this.chance_start);
        }

        private void ImportConfig()
        {
            try
            {
                string[] lines = File.ReadAllLines(this.games.path + "\\config\\Minesweeper.lps");
                if (lines.Length <= 0) return;

                foreach (string line in lines)
                {
                    string name = line.Split('=')[0];
                    string value = line.Split('=')[1];
                    if (name == null || value == null) continue;

                    if (name == "rows")
                        this.row = int.Parse(value);
                    else if (name == "cols")
                        this.col = int.Parse(value);
                    else if (name == "mines")
                        this.bomb = int.Parse(value);
                    else if (name == "cell_size")
                        this.cellScale = int.Parse(value);
                    else if (name == "chance_win")
                        this.chance_win = double.Parse(value);
                    else if (name == "chance_loss")
                        this.chance_loss = double.Parse(value);
                    else if (name == "chance_start")
                        this.chance_start = double.Parse(value);
                    else if (name == "chance_wait")
                        this.chance_wait = double.Parse(value);
                    else if (name == "isTimerSound")
                        this.isTimerSound = bool.Parse(value);
                    else if (name == "isMarkEnabled")
                        this.isMarkEnabled = bool.Parse(value);
                }
            }
            catch (IOException e)
            {
            }
        }

        private void InitializeBoard()
        {
            double borderSize = (this.cellScale / 4);

            this.isFirst = true;
            this.isGameOver = false;
            this.correctCell = 0;
            this.bombExists = this.bomb;
            this.gameBoard = new int[this.row, this.col];
            this.restartImage.Source = restartDefaultImage;
            this.restartImage.Height = this.exitImage.Height = this.Timer_n1.Height = this.Timer_n2.Height = this.Timer_n3.Height = this.cellScale * 1.5;
            this.Bomb_n1.Height = this.Bomb_n2.Height = this.Bomb_n3.Height = this.Timer_n1.Height = this.Timer_n2.Height = this.Timer_n3.Height = this.cellScale * 1.5 - 2;

            this.waitTimer.Start();
            this.ChangeBombExists();

            this.bombPlaces.Clear();
            this.possibleBomb.Clear();
            this.gameGrid.Children.Clear();

            this.Timer_n1.Source = this.timerNumbers[0];
            this.Timer_n2.Source = this.timerNumbers[0];
            this.Timer_n3.Source = this.timerNumbers[0];

            this.UserPanel.Height = this.userPanelBorderLeft.Height = this.userPanelBorderRight.Height = this.cellScale * 2.5;
            this.mainBorderLeft.BorderThickness = this.userPanelBorderLeft.BorderThickness = new Thickness(0, 0, borderSize, borderSize);
            this.mainBorderRigth.BorderThickness = this.userPanelBorderRight.BorderThickness = new Thickness(borderSize, borderSize, 0, 0);
            this.Width = this.mainBorderLeft.Width = this.mainBorderRigth.Width = this.col * this.cellScale + (this.cellScale * 11/6);
            this.Height = this.mainBorderLeft.Height = this.mainBorderRigth.Height = this.row * this.cellScale + (this.cellScale * 24/5);

            this.gameGrid.Width = this.UserPanel.Width = this.userPanelBorderRight.Width = this.userPanelBorderLeft.Width = this.col * this.cellScale + (this.cellScale / 2);
            this.gameGrid.Height = this.row * this.cellScale + (this.cellScale / 2);
            this.gameGrid.Margin = new Thickness(this.cellScale / 1.5, this.cellScale * 18/5, this.cellScale / 1.5, this.cellScale / 1.5);

            this.UserPanelBombs.Margin = new Thickness(this.cellScale / 2, this.cellScale / 2, 0, 0);
            this.UserPanelTimer.Margin = new Thickness(this.gameGrid.Width - this.cellScale * 3, this.cellScale / 2, 0, 0);
            this.UserPanelButtons.Margin = new Thickness(this.UserPanel.Width / 2 - ((10 + this.cellScale * 3) / 2), this.cellScale / 2, 0, 0);

            for (int r = 0; r < this.row; r++)
            {
                for (int c = 0; c < this.col; c++)
                {
                    this.gameBoard[r, c] = 0;
                    this.possibleBomb.Add(new KeyValuePair<int, int>(r, c));

                    Image cell = new Image()
                    {
                        Source = cellImage,
                        Width = this.cellScale,
                        Height = this.cellScale,
                        Margin = new Thickness(c * this.cellScale + borderSize, r * this.cellScale + borderSize, 0, 0),
                        Name = "Cell_" + r.ToString() + "_" + c.ToString()
                    };
                    cell.MouseLeftButtonDown += CheckCell;
                    cell.MouseRightButtonDown += ChangeState;
                    this.gameGrid.Children.Add(cell);
                }
            }

            Border border1 = new Border()
            {
                BorderThickness = new Thickness(0, 0, borderSize, borderSize),
                BorderBrush = Brushes.White,
                Width = this.gameGrid.Width,
                Height = this.gameGrid.Height
            };

            Border border2 = new Border()
            {
                BorderThickness = new Thickness(borderSize, borderSize, 0, 0),
                BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#189edc")),
                Width = this.gameGrid.Width,
                Height = this.gameGrid.Height
            };

            this.gameGrid.Children.Add(border1);
            this.gameGrid.Children.Add(border2);
        }

        private void ChangeState(object sender, MouseButtonEventArgs e)
        {
            Image cell = (Image)sender;
            if(cell.Source == this.cellImage)
            {
                this.bombExists--;
                cell.Source = this.cellFlagImage;
                this.ChangeBombExists();

            }
            else if (cell.Source == this.cellFlagImage)
            {
                this.bombExists++;
                if(this.isMarkEnabled)
                    cell.Source = this.cellUnknownImage;
                else
                    cell.Source = this.cellImage;
                this.ChangeBombExists();
            }
            else if (cell.Source == this.cellUnknownImage)
                cell.Source = this.cellImage;
        }

        private void ChangeBombExists()
        {
            string number = this.bombExists.ToString("D3");
            char[] numbers = number.ToCharArray();

            this.Bomb_n1.Source = this.timerNumbers[number[0] == '-' ? 10 : int.Parse(numbers[0].ToString())];
            this.Bomb_n2.Source = this.timerNumbers[number[0] == '-' ? int.Parse(numbers[2].ToString()) : int.Parse(numbers[1].ToString())];
            this.Bomb_n3.Source = this.timerNumbers[number[0] == '-' ? int.Parse(numbers[3].ToString()) : int.Parse(numbers[2].ToString())];
        }

        private async void CheckCell(object sender, MouseButtonEventArgs e)
        {
            if (this.isGameOver) return;

            Image cell = (Image)sender;
            if (cell.Source != this.cellImage && cell.Source != this.cellFlagImage && cell.Source != this.cellUnknownImage) return;

            string[] rectName = cell.Name.Split('_');
            int rectRow = int.Parse(rectName[1]);
            int rectCol = int.Parse(rectName[2]);

            if (this.isFirst)
                this.InitializeBombs(rectRow, rectCol);

            int number = this.gameBoard[rectRow, rectCol];
            if (number == this.bombInt)
            {
                this.isGameOver = true;
                this.ShowCell();
                cell.Source = cellGameOverBombImage;
                this.restartImage.Source = restartLoseImage;
                this.games.SendRandomMsg(this.dialogue.lose, this.chance_loss);
                this.games.PlaySFX(7);

                try
                {
                    IGraph graph = this.games.MW.Main.Core.Graph.FindGraph("fungames.exploding", GraphInfo.AnimatType.Single, GameSave.ModeType.Happy);
                    if (graph == null) return;
                    this.games.petBoard.SetVisibility(false);
                    this.games.MW.Main.Display(graph, (Action)(() => {
                        this.games.MW.Main.DisplayToNomal();
                    }));

                    await Task.Delay(1625);
                    this.games.petBoard.SetVisibility(true);
                }
                catch { };
            }
            else if (number >= 0 && number <= 8)
            {
                this.correctCell++;
                cell.Source = cellNumberImages[number];
                cell.IsEnabled = false;
            }

            if (number == 0)
                this.ShowZero(rectRow, rectCol);

            if(this.correctCell == (this.row * this.col - this.bomb))
            {
                this.isGameOver = true;
                this.restartImage.Source = restartWinImage;
                this.games.SendRandomMsg(this.dialogue.win, this.chance_win);
                this.games.PlaySFX(9);
            }
        }

        private void ShowZero(int r, int c)
        {
            KeyValuePair<int, int>[] positionAround = this.GetPostionAround(r, c);
            foreach (KeyValuePair<int, int> position in positionAround)
            {
                int pRow = position.Key;
                int pCol = position.Value;
                if (pRow < 0 || pRow >= this.row || pCol < 0 || pCol >= this.col) continue;
                int number = this.gameBoard[pRow, pCol];
                if (number < 0 || number > 8) continue;

                Image foundImage = FindVisualChild<Image>(this.gameGrid, "Cell_" + pRow.ToString() + '_' + pCol.ToString());
                if (foundImage == null) continue;

                if (foundImage.Source != cellImage) continue;
                foundImage.Source = cellNumberImages[number];
                this.correctCell++;
                foundImage.IsEnabled = false;
                if (number == 0)
                   this.ShowZero(pRow, pCol);
            }
        }

        private void InitializeBombs(int rectRow, int rectCol)
        {
            this.timer.Start();
            this.isFirst = false;
            this.rand = new Random();
            this.possibleBomb.Remove(new KeyValuePair<int, int>(rectRow, rectCol));

            KeyValuePair<int, int>[] positionAround = this.GetPostionAround(rectRow, rectCol);
            foreach (KeyValuePair<int, int> position in positionAround)
            {
                int pRow = position.Key;
                int pCol = position.Value;

                if (pRow < 0 || pRow >= this.row || pCol < 0 || pCol >= this.col) continue;
                this.possibleBomb.Remove(new KeyValuePair<int, int>(pRow, pCol));
            }

            for (int b = 0; b < this.bomb; b++)
            {
                int index = this.rand.Next(this.possibleBomb.Count);
                KeyValuePair<int, int> bombPlace = this.possibleBomb[index];

                this.bombPlaces.Add(bombPlace);
                this.possibleBomb.Remove(bombPlace);
                this.gameBoard[bombPlace.Key, bombPlace.Value] = this.bombInt;
            }
            this.InitializeNumbers();
        }

        private void InitializeNumbers()
        {
            foreach (KeyValuePair<int, int> bomb in this.bombPlaces)
            {
                int bombRow = bomb.Key;
                int bombCol = bomb.Value;

                KeyValuePair<int, int>[] positionAround = this.GetPostionAround(bombRow, bombCol);
                foreach (KeyValuePair<int, int> position in positionAround)
                {
                    int pRow = position.Key;
                    int pCol = position.Value;

                    if (pRow < 0 || pRow >= this.row || pCol < 0 || pCol >= this.col)
                        continue;

                    int pNumber = this.gameBoard[pRow, pCol];
                    if (pNumber == this.bombInt) continue;
                    this.gameBoard[pRow, pCol] = pNumber + 1;
                }
            }
        }

        private KeyValuePair<int, int>[] GetPostionAround(int posR, int posC) {
            KeyValuePair<int, int>[] positionAround = {
                new KeyValuePair<int, int>(posR-1, posC-1),
                new KeyValuePair<int, int>(posR-1, posC),
                new KeyValuePair<int, int>(posR-1, posC+1),
                new KeyValuePair<int, int>(posR, posC-1),
                new KeyValuePair<int, int>(posR, posC+1),
                new KeyValuePair<int, int>(posR+1, posC-1),
                new KeyValuePair<int, int>(posR+1, posC),
                new KeyValuePair<int, int>(posR+1, posC+1)
            };
            return positionAround;
        }

        private void ShowCell(bool debugMode = false)
        {
            for (int r = 0; r < this.row; r++)
            {
                for (int c = 0; c < this.col; c++)
                {
                    int number = this.gameBoard[r, c];
                    Image foundImage = FindVisualChild<Image>(this.gameGrid, "Cell_" + r.ToString() + '_' + c.ToString());

                    if(foundImage == null) continue;
                    if (number == bombInt)
                        foundImage.Source = cellBombImage;
                    else if (foundImage.Source == this.cellFlagImage)
                        foundImage.Source = this.cellBombIncorrectImage;
                    foundImage.IsEnabled = false;

                    if (!debugMode) continue;
                    if (number >= 0 && number <= 8)
                        foundImage.Source = cellNumberImages[number];
                }
            }
        }

        public static T FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T element && element.Name == name)
                {
                    return element;
                }

                T result = FindVisualChild<T>(child, name);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        private void RestartGame(object sender, RoutedEventArgs e)
        {
            this.timer.Stop();
            this.waitTimer.Stop();
            this.timerInt = 0;
            this.InitializeBoard();
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            this.games.minesweeperBoard = null;
            this.waitTimer.Stop();
            this.timer.Stop();
            this.games.petBoard.SetVisibility(false);
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

        private void ChangeTimer(object sender, EventArgs e)
        {
            if (this.isGameOver || this.timerInt >= 999 || this.games.minesweeperBoard == null) {
                this.timer.Stop();
                return;
            }

            this.timerInt++;
            if(this.isTimerSound)
                this.games.PlaySFX(8);
            string number = this.timerInt.ToString("D3");
            char[] numbers = number.ToCharArray();

            this.Timer_n1.Source = this.timerNumbers[int.Parse(numbers[0].ToString())];
            this.Timer_n2.Source = this.timerNumbers[int.Parse(numbers[1].ToString())];
            this.Timer_n3.Source = this.timerNumbers[int.Parse(numbers[2].ToString())];
        }

        private void InactivityTimer(object sender, EventArgs e)
        {
            this.waitTimer.Stop();
            if (this.games.minesweeperBoard == null) return;
            this.games.SendRandomMsg(this.dialogue.wait, this.chance_wait);
            this.waitTimer.Start();
        }
    }
}