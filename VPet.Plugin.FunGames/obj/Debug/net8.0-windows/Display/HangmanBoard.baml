   M S B A M L   `   `   `  ����Y  ULinePutScript.Localization.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullwTclr-namespace:LinePutScript.Localization.WPF;assembly=LinePutScript.Localization.WPFLinePutScript.Localization.WPF  5   	   O KVPet.Plugin.FunGames, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:VPet.Plugin.FunGamesVPet.Plugin.FunGames 5   9   '   !VPet.Plugin.FunGames.HangmanBoard   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   cllTclr-namespace:LinePutScript.Localization.WPF;assembly=LinePutScript.Localization.WPF     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   H DPanuon.WPF.UI, Version=1.2.1.1, Culture=neutral, PublicKeyToken=null-pu$https://opensource.panuon.com/wpf-ui  5   	   .local"clr-namespace:VPet.Plugin.FunGames  69   -   5          � 
ResizeMode$  NoResize=�5   F    � ShowInTaskbar$ False��5   P    � AllowsTransparency$
 True��5   5    � WindowStyle$
 None=�6O   
������ $��466.75��5	   	   $	��256��6   $��14(�6%   "  Panuon.WPF.UI.WindowXCaption   Height$ 0Q�63      Buttons$ Close=�6P    � Topmost$
 True��5
   	    � Title 5      1   +LinePutScript.Localization.WPF.StrExtension  *	Hangman+'  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
   	   GlassFrameThickness$	 -1q�6   
 � 	Resources
 5         BorderedStackPanelTemplate �� 
TargetType  SecondaryLighter  	Secondary �� CornerRadius �� Padding  WordLetters �� 
TargetType �� Property �� Value ��Name �� TextAlignment  inCorrectWordLetters��%�  &	        &	 �     &	     �� 5      " ��67   .R�5      �� #���  6   #���  6<   $��3q�5      $	��Top=�6   $��0,0,0,0q�62   $ 5��6C   $ 2q�6T   ._�5      �� #��z��6   5      5      �� 5      " ��6   .�5      ��  � ��$ 15(�6    �� 5       � ��$ 25��6   �� 5       � {�$ False��6!   �� 5       � ��$ 
3, 0, 3, 0q�6   �� 5       � ��$	 Top=�6)   �� 5       � �� �#039be56"   �� 5       � �� 5      �� 5       " ��6   .R�5!      � .+�5"   	   �� ���#039be5$��1q�6&   $ 3��6:   ���#cbe4f9._�5#   
   �� $ MyContentPresenter��6   #��z��5$      $��Center=�5%      $��Center=�5&      $ Center=�5'      5)   
   5*   	   5+      5-      5.      �� 5/      " ��6'   .�50      ��  � ��$ 15(�6    �� 51       � ��$ 25��6   �� 52       � ��$ 25��6   �� 53       � {�$ False��6!   �� 54       � ��$ 
3, 0, 3, 0q�6   �� 55       � ��$ Bottom=�6)   �� 56       � �� �#d900006"   �� 58       � �� 59      �� 5:      " ��6&   .R�5;      � .+�5<   	   �� 
���  ��$��1q�6"   $ 3��66   ���#ffacac._�5=   
   �� $ MyContentPresenter��6   #��z��5>      $��Center=�5?      $��Center=�5@      $ Center=�5A      5C   
   5D      5E      5G      5H      5I      .��5J      �  � RowDefinitions 5K      �� 5L      $��369.75 �6   �� 5M      $��30 �6   �� 5N      $��35 �6   �� 5O      $��32 �6    � ColumnDefinitions 5Q      �� 5R      $��* �6   �� 5S      $��* �6   �� 5T      $��* �6   �� 5U      $��* �6   .+�5W      �� -   5         ��Name$ 
Hook_phase��5W   y   $
��Auto��6
   $��0��6   $��5��6$   $
��Auto��68   $
��Left=�6F   $	��Top=�6a   $��0,0,0,0q�6�   �� 5X      #��[  6   $��2��6J   $��5��6W   $��Center=�6k   .��5Y      ��  �� Orientation$ 
Horizontal=�6   .+�5Z      �� -   5         ��Name$ tbTalk��5Z        StandardTextBoxStyle#���  6   $��25��6M   $��25��6Y   $��15(�6d   $��
5, 0, 5, 0q�6r   $��0, 1.5, 0, 0q�6�   $��Center=�5[       �� 	MaxLength$ 1��6$    �� AcceptsReturn$
 True��62    �� TextWrapping$ WrapWithOverflow=�6G    �� TextAlignment$
 Left=�5\   '   �� 5]      -   5        $��
SendButton��5]         Panuon.WPF.UI.ButtonHelper   CornerRadius$ 4��6   $��2q�6Z   $��
5, 0, 5, 0q�6n     PrimaryLight#���  5^      $��25��63   $
��Auto��6?   $��15(�6L     DARKPrimaryDarker#���  5_      ��5]   @     *Send+5_   9   �� 5`      -   5        $��RestartButton��5`      $ 4��6"   $��2q�6`   $��25��6t   $
��Auto��6�   $��15(�6�   $��
5, 0, 5, 0q�6�   #���  5a      #���  5b      ��5`   C     *	Restart+5b   9   �� 5c      -   5        $��
ExitButton��5c      $ 4��6   $��2q�6Z   $��25��6n   $
��Auto��6z   $��15(�6�   $��
5, 0, 5, 0q�6�   #���  5d      #���  5e      ��5c   @     *Exit+5e   9   5f      5g      �� 5h      -   5        $��ChosenWordLabel��5h      $��1��6&   $��5��63   $ 
Horizontal=�6G   $��Center=�6`   .+�5i      �� $��?��6   #���  6   6@   �� 5j      $��?��6   #���  6   6@   �� 5k      $��?��6   #���  6   6@   5l      �� 5m      -   5        $��inCorrectWordLabel��5m      $��3��6)   $��5��66   $ 
Horizontal=�6J   $��Bottom=�6c   $��Center=�6~   .+�5n      �� $��?��6   #���  6   6I   �� 5o      $��?��6   #���  6   6I   �� 5p      $��?��6   #���  6   6I   5q      5s      5t      