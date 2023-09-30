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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using VPet_Simulator.Core;
using VPet_Simulator.Windows.Interface;

namespace VPet.Plugin.FunGames
{
    public partial class CookingBoard : WindowX
    {
        IMainWindow mw;
        private string path;
        SoundPlayer soundPlayer;
        DialogueForHamgman dialogue = new DialogueForHamgman();

        private int maxPoints = 4;
        private Random random = new Random();
        private List<Image> fruits = new List<Image>();
        private List<Point> trailPoints = new List<Point>();
        private DispatcherTimer fruitTimer = new DispatcherTimer();

        public CookingBoard(IMainWindow mw)
        {
            this.mw = mw;
            this.InitializeComponent();

            this.path = Directory.GetParent(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName;

            Cursor knifeCursor = new Cursor(path + "\\cursor\\knife.cur");
            Canvas.Cursor = knifeCursor;

            this.StartGeneratingCursorTrail();

            fruitTimer.Tick += GenerateFruit;
            fruitTimer.Interval = TimeSpan.FromSeconds(1);
            fruitTimer.Start();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void WindowMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void DrawMouseTrail()
        {
            CursorTrail.Children.Clear();
            if(this.trailPoints.Count > 2)
            {
                PolyBezierSegment bezierSegment = new PolyBezierSegment(trailPoints, true);
                PathFigure pathFigure = new PathFigure(trailPoints[0], new[] { bezierSegment }, false);
                PathGeometry pathGeometry = new PathGeometry(new[] { pathFigure });

                System.Windows.Shapes.Path path = new System.Windows.Shapes.Path
                {
                    Data = pathGeometry,
                    Stroke = Brushes.White,
                    StrokeThickness = 4,
                };

                CursorTrail.Children.Add(path);
            }
        }

        private async void StartGeneratingCursorTrail()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10));
                Point mousePosition = Mouse.GetPosition(CursorTrail);

                if (this.trailPoints.Count > this.maxPoints)
                    this.trailPoints.RemoveAt(0);

                this.trailPoints.Add(mousePosition);
                this.DrawMouseTrail();
            }
        }

        private void GenerateFruit(object sender, EventArgs e)
        {
            Image fruitImage = new Image();
            string imagePath = this.path + "\\image\\fruits\\apple.png";
            fruitImage.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            double randomX = random.Next((int) Canvas.ActualWidth - (int) fruitImage.Source.Width);
            Canvas.SetLeft(fruitImage, randomX);
            Canvas.SetTop(fruitImage, (int) Canvas.ActualHeight + fruitImage.Source.Height);

            Canvas.Children.Add(fruitImage);
            this.fruits.Add(fruitImage);

            double speed = random.NextDouble() * 2 + 1;

            DoubleAnimation throwAnimation = new DoubleAnimation
            {
                To = (double) Canvas.ActualHeight / (random.NextDouble() * 3 + 1),
                Duration = TimeSpan.FromSeconds(speed),
            };

            throwAnimation.Completed += (s, args) =>
            {
                DoubleAnimation fallAnimation = new DoubleAnimation
                {
                    To = (double) Canvas.ActualHeight,
                    Duration = TimeSpan.FromSeconds(speed),
                };

                fruitImage.BeginAnimation(Canvas.TopProperty, fallAnimation);
            };

            fruitImage.BeginAnimation(Canvas.TopProperty, throwAnimation);

            DispatcherTimer removeTimer = new DispatcherTimer();
            removeTimer.Interval = TimeSpan.FromSeconds(7);
            removeTimer.Tick += (s, args) =>
            {
                if(this.fruits.Contains(fruitImage))
                {
                    this.fruits.Remove(fruitImage);
                    Canvas.Children.Remove(fruitImage);
                }
                removeTimer.Stop();
            };
            removeTimer.Start();

            fruitImage.MouseEnter += (s, args) =>
            {
                if(this.fruits.Contains(fruitImage))
                {
                    this.fruits.Remove(fruitImage);
                    Canvas.Children.Remove(fruitImage);
                }
            };
        }

        private void SendRandomMsg(List<string> dialogue, double chance)
        {
            if (random.NextDouble() < 1 - chance) return;

            int index = this.random.Next(dialogue.Count);
            if (index < 0) return;
            string msgContent = dialogue[index];
            if (msgContent == null) return;

            try
            {
                this.mw.Main.Say(msgContent.Translate());
            }
            catch { };
        }

        private void PlayAnimation(string graphName, GraphInfo.AnimatType animatType)
        {
            try
            {
                IGraph graph = this.mw.Main.Core.Graph.FindGraph(graphName, animatType, GameSave.ModeType.Happy);
                if (graph == null) return;
                this.mw.Main.Display(graph, (Action)(() => {
                    this.mw.Main.DisplayToNomal();
                }));
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
    }
}
