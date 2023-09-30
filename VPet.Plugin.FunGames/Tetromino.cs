using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace VPet.Plugin.FunGames
{
    public class Tetromino
    {
        public Point position = new Point(3, 0);
        public Point lastPosition = new Point(3, 0);

        public char shape;
        public Point[] blocks;
        public Brush color;
        public bool canMove = true;

        private int rotation = 0;

        public Tetromino(char shape, int styleId = 0)
        {
            this.shape = shape;
            this.blocks = this.GetShape(shape);
            this.color = this.GetColor(styleId);
        }

        private Point[] GetShape(char shape)
        {
            if(shape == 'I')
                return new Point[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(1, 3) };
            else if (shape == 'O')
                return new Point[] { new Point(0, 0), new Point(0, 1), new Point(1, 0), new Point(1, 1) };
            else if(shape == 'T')
                return new Point[] { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) };
            else if(shape == 'J')
                return new Point[] { new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) };
            else if(shape == 'L')
                return new Point[] { new Point(2, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) };
            else if(shape == 'S')
                return new Point[] { new Point(0, 1), new Point(1, 0), new Point(1, 1), new Point(2, 0) };
            else
                return new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(2, 1) };
        }

        private Brush GetColor(int styleId)
        {
            if (styleId == 0)
            {
                if (this.shape == 'I')
                    return Brushes.Cyan;
                else if (this.shape == 'O')
                    return Brushes.Yellow;
                else if (this.shape == 'T')
                    return Brushes.Pink;
                else if (this.shape == 'J')
                    return Brushes.DarkBlue;
                else if (this.shape == 'L')
                    return Brushes.Orange;
                else if (this.shape == 'S')
                    return Brushes.Lime;
                else
                    return Brushes.Red;
            } else
                return Brushes.Gray;
        }

        public void Move(Key key)
        {
            if (!this.canMove) return;

            double X = this.position.X;
            double Y = this.position.Y;
            this.lastPosition = new Point(X, Y);

            if ((key == Key.Left || key == Key.A) && this.position.X - 1 > -1)
                X--;
            else if ((key == Key.Right || key == Key.D) && this.position.X + 1 < 10)
                X++;
            else if ((key == Key.Down || key == Key.S) && this.position.Y + 1 < 20)
                Y++;

            this.position = new Point(X, Y);
        }

        public void Rotate()
        {
            List<Point[]> blockRotation = new List<Point[]>();

            if (shape == 'I')
            {
                blockRotation.Add(new Point[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(1, 3) });
                blockRotation.Add(new Point[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1) });
                blockRotation.Add(new Point[] { new Point(2, 0), new Point(2, 1), new Point(2, 2), new Point(2, 3) });
                blockRotation.Add(new Point[] { new Point(0, 2), new Point(1, 2), new Point(2, 2), new Point(3, 2) });
            }
            else if (shape == 'O')
            {
                blockRotation.Add(new Point[] { new Point(0, 0), new Point(0, 1), new Point(1, 0), new Point(1, 1) });
            }
            else if (shape == 'T')
            {
                blockRotation.Add(new Point[] { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) });
                blockRotation.Add(new Point[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(2, 1) });
                blockRotation.Add(new Point[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(1, 2) });
                blockRotation.Add(new Point[] { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(1, 2) });
            }
            else if (shape == 'J')
            {
                blockRotation.Add(new Point[] { new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) });
                blockRotation.Add(new Point[] { new Point(1, 0), new Point(2, 0), new Point(1, 1), new Point(1, 2) });
                blockRotation.Add(new Point[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(2, 2) });
                blockRotation.Add(new Point[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(0, 2) });
            }
            else if (shape == 'L')
            {
                blockRotation.Add(new Point[] { new Point(2, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) });
                blockRotation.Add(new Point[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(2, 2) });
                blockRotation.Add(new Point[] { new Point(0, 1), new Point(0, 2), new Point(1, 1), new Point(2, 1) });
                blockRotation.Add(new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(1, 2) });
            }
            else if (shape == 'S')
            {
                blockRotation.Add(new Point[] { new Point(0, 1), new Point(1, 0), new Point(1, 1), new Point(2, 0) });
                blockRotation.Add(new Point[] { new Point(1, 0), new Point(1, 1), new Point(2, 1), new Point(2, 2) });
                blockRotation.Add(new Point[] { new Point(0, 2), new Point(1, 1), new Point(1, 2), new Point(2, 1) });
                blockRotation.Add(new Point[] { new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(1, 2) });
            }
            else
            {
                blockRotation.Add(new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(2, 1) });
                blockRotation.Add(new Point[] { new Point(2, 0), new Point(1, 1), new Point(1, 2), new Point(2, 1) });
                blockRotation.Add(new Point[] { new Point(0, 1), new Point(1, 1), new Point(1, 2), new Point(2, 2) });
                blockRotation.Add(new Point[] { new Point(0, 1), new Point(0, 2), new Point(1, 1), new Point(1, 0) });
            }

            this.rotation++;
            if(this.rotation > blockRotation.Count - 1)
                this.rotation = 0;

            this.blocks = blockRotation[this.rotation];
        }
    }
}