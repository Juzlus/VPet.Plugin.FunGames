   M S B A M L   `   `   `  ����Y  ULinePutScript.Localization.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullwTclr-namespace:LinePutScript.Localization.WPF;assembly=LinePutScript.Localization.WPFLinePutScript.Localization.WPF  5   	   O KVPet.Plugin.FunGames, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:VPet.Plugin.FunGamesVPet.Plugin.FunGames 5   9   +   %VPet.Plugin.FunGames.MillionaireBoard   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   cllTclr-namespace:LinePutScript.Localization.WPF;assembly=LinePutScript.Localization.WPF     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   H DPanuon.WPF.UI, Version=1.2.1.1, Culture=neutral, PublicKeyToken=null-pu$https://opensource.panuon.com/wpf-ui  5   	   .local"clr-namespace:VPet.Plugin.FunGames  69   -   5          � 
ResizeMode$  NoResize=�5   F    � ShowInTaskbar$ False��5   P    � AllowsTransparency$
 True��5   9    � WindowStyle$
 None=�6S   
������ $	��201��5	   	   $	��650��6   $��14(�6"   "  Panuon.WPF.UI.WindowXCaption   Height$ 0Q�60      Buttons$ Close=�6M    � Topmost$
 True��5
   	    � Title 5      1   +LinePutScript.Localization.WPF.StrExtension  *Millionaire+.��5      � -   5        $��Canvas��5       � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      $��75 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��30 �6   	 � ColumnDefinitions	 5   
   �� 5      $	��50* �6   �� 5      $	��50* �6   .+�5   
   �� $��1��6   $��2��6   $@��:/VPet.Plugin.FunGames;Component/Image/Millionaire_line.png��61   $��0,10,0,0q�6u   �� 5   
   $��2��6   $@��:/VPet.Plugin.FunGames;Component/Image/Millionaire_line.png��6   $��0,-4,0,3q�6a   �� 5   
   $��2��6   $��1��6   $@��:/VPet.Plugin.FunGames;Component/Image/Millionaire_line.png��6-   $��0,-4,0,3q�6q   �� 5   
   $��3��6   $@��:/VPet.Plugin.FunGames;Component/Image/Millionaire_line.png��6   $��0,-4,0,3q�6a   �� 5   
   $��3��6   $��1��6   $@��:/VPet.Plugin.FunGames;Component/Image/Millionaire_line.png��6-   $��0,-4,0,3q�6q   �� 5   
   $��4��6   $��0��6   $@��:/VPet.Plugin.FunGames;Component/Image/Millionaire_line.png��6-   $��
100,-4,0,0q�6q   �� 5   
   $��4��6   $��1��6   $@��:/VPet.Plugin.FunGames;Component/Image/Millionaire_line.png��6-   $��
0,-4,100,0q�6q   �� 5   
   $��1��6   $��2��6   $D��>/VPet.Plugin.FunGames;Component/Image/Millionaire_question.png��61   $��
0,19,0,9.2q�6y   �� 5    
   -   5        
 ��Name$
 Question_Text��5    !   $��1��6   $��2��68   $�� ��6L   
������� �� TextAlignment$ Center=�6l   $	��Top=�6�   $��
58,31,58,0q�6�   �� 5"   
   -   5         ��Name$ Prize��5"       �� Orientation$ Vertical=�6$   $��2��6;   $��1��6O   $��0��6\   .+�5#      �� -   5         ��Name$ Prize_Image��5#      $A��;/VPet.Plugin.FunGames;Component/Image/Millionaire_prize.png��6)   $��250,0,250,0q�6n   �� 5$      -   5        $
 
Prize_Text��5$      $�� ��6,   
�������$ Center=�6L   $��Center=�6c   $��250,-24,250,0q�6~   �� 5&      -   5        $ Lifelines_Image��5&      $A��;/VPet.Plugin.FunGames;Component/Image/Millionaire_prize.png��6-   $��420,-20,80,0q�6r   �� 5(      -   5        $ Lifelines_50_50_Image��5(   /   $A��;/VPet.Plugin.FunGames;Component/Image/Millionaire_50_50.png��6N   $��444,-20,180,0q�6�    � ToolTip 6     *50:50+6�   �� 5)      -	   5        $ Lifelines_askHost_Image��5)   6   $C��=/VPet.Plugin.FunGames;Component/Image/Millionaire_askHost.png��6W   $��482,-20,142,0q�6�    6     *Ask the host+6�   �� 5*      -
   5        $ Lifelines_switch_Image��5*   =   $B��</VPet.Plugin.FunGames;Component/Image/Millionaire_switch.png��6]   $��520,-20,104,0q�6�    6     *Switch the question+6�   5+      �� 5-   
   -   5        $ AnswerA��5-      $ Vertical=�6&   $��2��6=   $��0��6J   .+�5.      �� -   5        $ AnswerA_Image��5.      $B��</VPet.Plugin.FunGames;Component/Image/Millionaire_answer.png��6+   $��
41,0,8,4.6q�6q   �� 5/      -   5        $
 AnswerA_Text��5/      $�� ��6.   
�������$ Center=�6N   $��Center=�6e   $��58,-55,27,0q�6�   6�   50      �� 52   
   -   5        $ AnswerB��52      $ Vertical=�6&   $��2��6=   $��1��6J   .+�53      �� -   5        $ AnswerB_Image��53      $B��</VPet.Plugin.FunGames;Component/Image/Millionaire_answer.png��6+   $��
8,0,41,4.6q�6q   �� 54      -   5        $
 AnswerB_Text��54      $�� ��6.   
�������$ Center=�6N   $��Center=�6e   $��27,-55,58,0q�6�   6�   55      �� 57   
   -   5        $ AnswerC��57      $ Vertical=�6&   $��3��6=   $��0��6J   .+�58      �� -   5        $ AnswerC_Image��58      $B��</VPet.Plugin.FunGames;Component/Image/Millionaire_answer.png��6+   $��
41,0,8,4.6q�6q   �� 59      -   5        $
 AnswerC_Text��59      $�� ��6.   
�������$ Center=�6N   $��Center=�6e   $��58,-55,27,0q�6�   6�   5:      �� 5<   
   -   5        $ AnswerD��5<      $ Vertical=�6&   $��3��6=   $��1��6J   .+�5=      �� -   5        $ AnswerD_Image��5=      $B��</VPet.Plugin.FunGames;Component/Image/Millionaire_answer.png��6+   $��
8,0,41,4.6q�6q   �� 5>      -   5        $
 AnswerD_Text��5>      $�� ��6.   
�������$ Center=�6N   $��Center=�6e   $��27,-55,58,0q�6�   6�   5?      �� 5A   
   -   5        $ Restart��5A      $ Vertical=�6&   $��4��6=   $��0��6J   .+�5B      �� -   5        $ Restart_Image��5B      $B��</VPet.Plugin.FunGames;Component/Image/Millionaire_answer.png��6+   $��	150,0,8,0q�6q   �� 5C      -   5        $
 Restart_Text��5C      
�������$ Center=�6`   $��Center=�6w   $��150,-28,8,0q�6�   ��6.     *	Restart+6�   5D      �� 5F   
   -   5        $
 Exit��5F      $ Vertical=�6#   $��4��6:   $��1��6G   .+�5G      �� -   5        $ 
Exit_Image��5G      $B��</VPet.Plugin.FunGames;Component/Image/Millionaire_answer.png��6(   $��	8,0,150,0q�6n   �� 5H      -   5        $
 	Exit_Text��5H      
�������$ Center=�6Z   $��Center=�6q   $��8,-28,150,0q�6�   ��6+     *Exit+6�   5I      5J      5K      