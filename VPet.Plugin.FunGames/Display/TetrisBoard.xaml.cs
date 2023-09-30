using Panuon.WPF.UI;
using System.Windows.Threading;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using Steamworks.Data;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Controls;
using System.Linq;

namespace VPet.Plugin.FunGames
{
    public partial class TetrisBoard : WindowX
    {
        private int columns = 10;
        private int rows = 20;
        private int blockSize = 30;
        private int speed = 500;

        private bool usedHold = false;
        private Random random = null;
        private FunGames games = null;
        private DispatcherTimer gameTimer = null;
        private char holdTetrominoSymbol = '\0';
        private char[] tetrominosSumbols = { 'I', 'O', 'T', 'J', 'L', 'S', 'Z' };
        private Tetromino currentTetromino = null;

        public TetrisBoard(FunGames games)
        {
            this.games = games;
            this.InitializeComponent();
            this.InitializeGame();
        }

        private void InitializeGame()
        {
            this.gameGrid.Children.Clear();
            for (int row = 0; row < this.rows; row++)
            {
                for (int col = 0; col < this.columns; col++)
                {
                    Rectangle cell = new Rectangle
                    {
                        Width = this.blockSize,
                        Height = this.blockSize,
                        Stroke = Brushes.Gray,
                        Fill = Brushes.Transparent,
                        Name = "Cell" + row.ToString() + col.ToString(),
                    };

                    Grid.SetRow(cell, row);
                    Grid.SetColumn(cell, col);
                    this.gameGrid.Children.Add(cell);

                }
            }

            this.gameTimer = new DispatcherTimer();
            this.gameTimer.Tick += GameTick;
            this.gameTimer.Interval = TimeSpan.FromMilliseconds(this.speed);
            this.gameTimer.Start();
        }

        private Tetromino GetTetromino(char symbol = '\0')
        {
            this.usedHold = false;
            this.random = new Random();
            return new Tetromino(symbol == '\0' ? this.tetrominosSumbols[this.random.Next(this.tetrominosSumbols.Length)] : symbol);
        }

        private void PlaceTetromino()
        {
            List<Rectangle> lastRectangles = new List<Rectangle>();
            List<Rectangle> newRectangles = new List<Rectangle>();

            foreach (Point p in this.currentTetromino.blocks)
            {
                int lastRow = (int)(this.currentTetromino.lastPosition.Y + p.Y);
                int lastCol = (int)(this.currentTetromino.lastPosition.X + p.X);

                int newRow = (int)(this.currentTetromino.position.Y + p.Y);
                int newCol = (int)(this.currentTetromino.position.X + p.X);

                if (newRow >= 20)
                    this.currentTetromino.canMove = false;

                Rectangle lastRectangle = this.gameGrid.Children.OfType<Rectangle>()
                    .FirstOrDefault(rectangle => rectangle.Name == "Cell" + lastRow.ToString() + lastCol.ToString());

                Rectangle newRectangle = this.gameGrid.Children.OfType<Rectangle>()
                    .FirstOrDefault(rectangle => rectangle.Name == "Cell" + newRow.ToString() + newCol.ToString());

                if (lastRectangle != null)
                    lastRectangles.Add(lastRectangle);

                if (newRectangle != null)
                    newRectangles.Add(newRectangle);
            }

            foreach (Rectangle rectangle in newRectangles)
            {
                if (rectangle.Fill != Brushes.Transparent && !lastRectangles.Contains(rectangle))
                    this.currentTetromino.canMove = false;
            }

            if (!this.currentTetromino.canMove)
            {
                this.currentTetromino = null;
                return;
            }

            for(int i = 0; i < lastRectangles.Count; i++)
            {
                lastRectangles[i].Fill = Brushes.Transparent;
            }

            for (int i = 0; i < newRectangles.Count; i++)
            {
                newRectangles[i].Fill = this.currentTetromino.color;
            }
        }

        private void GameTick(object sender, EventArgs e)
        {
            if (this.currentTetromino == null)
            {
                this.currentTetromino = this.GetTetromino();
                this.PlaceTetromino();
            }
            else
            {
                this.currentTetromino.Move(Key.S);
                this.PlaceTetromino();
            }
        }

        private void PlayerMove(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (this.currentTetromino == null) return;
            if (e.Key == Key.Space)
            {
                while(this.currentTetromino != null)
                {
                    this.currentTetromino.Move(Key.S);
                    this.PlaceTetromino();
                }
            }
            else if ((e.Key == Key.LeftShift || e.Key == Key.C) && !this.usedHold)
            {
                char holdSymbol = this.holdTetrominoSymbol;
                this.holdTetrominoSymbol = this.currentTetromino.shape;

                foreach (Point p in this.currentTetromino.blocks)
                {
                    int row = (int)(this.currentTetromino.position.Y + p.Y);
                    int col = (int)(this.currentTetromino.position.X + p.X);

                    Rectangle lastRectangle = this.gameGrid.Children.OfType<Rectangle>()
                        .FirstOrDefault(rectangle => rectangle.Name == "Cell" + row.ToString() + col.ToString());

                    if (lastRectangle != null)
                        lastRectangle.Fill = Brushes.Transparent;
                }

                this.currentTetromino = this.GetTetromino(holdSymbol);
                this.usedHold = true;
                this.PlaceTetromino();
            }
            else if (e.Key == Key.Up || e.Key == Key.W || e.Key == Key.Z)
            {
                this.currentTetromino.Rotate();
                this.PlaceTetromino();
            }
            else
            {
                this.currentTetromino.Move((Key)e.Key);
                this.PlaceTetromino();
            }
        }
    }
}
