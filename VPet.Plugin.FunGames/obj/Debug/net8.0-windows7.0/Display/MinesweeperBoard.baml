   M S B A M L   `   `   `  ����Y  ULinePutScript.Localization.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullwTclr-namespace:LinePutScript.Localization.WPF;assembly=LinePutScript.Localization.WPFLinePutScript.Localization.WPF  5   	   O KVPet.Plugin.FunGames, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:VPet.Plugin.FunGamesVPet.Plugin.FunGames 5   9   +   %VPet.Plugin.FunGames.MinesweeperBoard   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   cllTclr-namespace:LinePutScript.Localization.WPF;assembly=LinePutScript.Localization.WPF     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   H DPanuon.WPF.UI, Version=1.2.1.1, Culture=neutral, PublicKeyToken=null-pu$https://opensource.panuon.com/wpf-ui  5   	   .local"clr-namespace:VPet.Plugin.FunGames  69   -   5          � 
ResizeMode$  NoResize=�5   F    � ShowInTaskbar$ False��5   P    � AllowsTransparency$
 True��5   9    � WindowStyle$
 None=�6S   ���#6dc4eb$	��370��5	   	   $��295.7��6   $��14(�6$   "  Panuon.WPF.UI.WindowXCaption   Height$ 0Q�62      Buttons$ Close=�6O    � Topmost$
 True��5
   	    � Title 5      1   +LinePutScript.Localization.WPF.StrExtension  *Minesweeper+'  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
   	   GlassFrameThickness$	 -1q�6   .��5      �� .+�5   
   �� -   5        
 ��Name$
 	UserPanel��5      $��
16.6, 16.6q�6$   $��262.5��68   $
��62.5��6F   .+�5      � -   5        $��UserPanelBombs��5   j   $��Center=�6   $��Center=�65   $��12.5, 12.5, 0, 0q�6P   
���   �.+�5      �� -   5        $��Bomb_n1��5   i   $?��9/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_0.png��6   $
��35.5��6[   $	��1,1q�6x   6�   �� 5      -   5        $��Bomb_n2��5   i   $?��9/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_0.png��6   $
��35.5��6[   $	��1,1q�6x   6�   �� 5      -   5        $��Bomb_n3��5   i   $?��9/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_0.png��6   $
��35.5��6[   $	��1,1q�6x   6�   5      � 5      -   5        $��UserPanelButtons��5   k   $��Center=�6   $��Center=�65   $��88.75, 12.5, 0, 0q�6P   .+�5      �� -   5        $��-1, -1, -1, -1q�5      $��0, 0, 10, 0q�6F   .��5      �� -	   5        $��restartImage��5   y   $K��E/VPet.Plugin.FunGames;Component/Image/Minesweeper_restart_default.png��6   $
��37.5��6k   6�   5      �� 5      -
   5        $��-1, -1, -1, -1q�5      .��5      �� -   5        $��	exitImage��5   n   $@��:/VPet.Plugin.FunGames;Component/Image/Minesweeper_exit.png��6   $
��37.5��6`   6�   5      5      � 5      -   5        $��UserPanelTimer��5   k   $��Center=�6   $��Center=�65   $��187.5, 12.5, 0, 0q�6P   
���   �.+�5      �� -   5        $��Timer_n1��5   i   $?��9/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_0.png��6   $
��35.5��6[   $	��1,1q�6y   6�   �� 5      -   5        $��Timer_n2��5   i   $?��9/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_0.png��6   $
��35.5��6[   $	��1,1q�6y   6�   �� 5       -   5        $��Timer_n3��5    i   $?��9/VPet.Plugin.FunGames;Component/Image/Minesweeper_n_0.png��6   $
��35.5��6[   $	��1,1q�6y   6�   5!      �� 5"      -   5         ��Name$ userPanelBorderLeft��5"      $��0,0,6.25,6.25q�62   ���#ffffff$��262.5��6h   $
��62.5��6v   6�   �� 5#      -   5        $ userPanelBorderRight��5#      $��6.25,6.25,0,0q�63   ���#189edc$��262.5��6i   $
��62.5��6w   6�   5$      �� 5&   
   -   5        $
 gameGrid��5&      $��262.5��6#   $��262.5��61   $��16.6, 90, 16.6, 16.6q�6@   .+�5'      �� -   5        $ gameGridBorderLeft��5'      $��0,0,6.25,6.25q�61   ���#ffffff$��262.5��6g   $��262.5��6u   6�   �� 5(      -   5        $ gameGridBorderRigth��5(      $��6.25,6.25,0,0q�62   ���#189edc$��262.5��6h   $��262.5��6v   6�   5)      �� 5+   
   -   5        $ mainBorderLeft��5+      $��0,0,6.25,6.25q�6)   ���#189edc$��295.7��6_   $	��370��6m   6|   �� 5,   
   -   5        $ mainBorderRigth��5,      $��6.25,6.25,0,0q�6*   ���#ffffff$��295.7��6`   $	��370��6n   6}   5-      5.      