using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace VPet.Plugin.FunGames
{
    public partial class petBoard : UserControl
    {
        public petBoard()
        {
            this.InitializeComponent();
            this.Visibility = Visibility.Hidden;
            this.SetImage("Millionaire_petDesk");
        }

        public void SetVisibility(bool visible)
        {
            this.Visibility = visible ? Visibility.Visible : Visibility.Hidden; 
        }

        public void SetImage(string imageName, bool setVisible = false)
        {
            this.petBoardImage.Source = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/" + imageName + ".png", UriKind.Relative));
            if (setVisible) this.SetVisibility(true);
        }
    }
}
