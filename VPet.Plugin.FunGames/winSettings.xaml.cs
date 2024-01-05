using LinePutScript.Localization.WPF;
using Panuon.WPF.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace VPet.Plugin.FunGames
{
    public partial class winSettings : WindowX
    {
        private FunGames games = null;

        public winSettings(FunGames games)
        {
            this.games = games;
            this.InitializeComponent();
            this.ImportConfig_TicTacToe();
            this.ImportConfig_Hangman();
            this.ImportConfig_Millionaire();
            this.ImportConfig_Minesweeper();
        }

        private void OpenFile_CustomWords(object sender, RoutedEventArgs e)
        {
            string path = this.games.path + "\\config\\Hangman_custom.txt";
            if (!File.Exists(path)) return;
            Process.Start(path);
        }

        private void Window_Closed(object sender, EventArgs e) => this.games.settingsPanel = (winSettings) null;

        private void ImportConfig_TicTacToe()
        {
            try
            {
                string[] lines = File.ReadAllLines(this.games.path + "\\config\\TicTacToe.lps");
                if (lines.Length <= 0) return;

                foreach (string line in lines)
                {
                    string name = line.Split('=')[0];
                    string value = line.Split('=')[1];
                    if (name == null || value == null) continue;

                    if (name == "difficulty")
                        this.Difficulty_ttt.SelectedIndex = int.Parse(value);
                    else if (name == "chance_win")
                        this.Chance_ttt_win.Value = double.Parse(value);
                    else if (name == "chance_loss")
                        this.Chance_ttt_loss.Value = double.Parse(value);
                    else if (name == "chance_draw")
                        this.Chance_ttt_draw.Value = double.Parse(value);
                    else if (name == "chance_start")
                        this.Chance_ttt_start.Value = double.Parse(value);
                    else if (name == "chance_wait")
                        this.Chance_ttt_wait.Value = double.Parse(value);
                    else if (name == "chance_think")
                        this.Chance_ttt_think.Value = double.Parse(value);
                }
            }
            catch (IOException e)
            {
            }
        }

        private void SaveConfig_TicTacToe(object sender, RoutedEventArgs e)
        {
            string content =
                "difficulty=" + this.Difficulty_ttt.SelectedIndex.ToString() +
                "\nchance_win=" + this.Chance_ttt_win.Value.ToString() +
                "\nchance_loss=" + this.Chance_ttt_loss.Value.ToString() +
                "\nchance_draw=" + this.Chance_ttt_draw.Value.ToString() +
                "\nchance_start=" + this.Chance_ttt_start.Value.ToString() +
                "\nchance_wait=" + this.Chance_ttt_wait.Value.ToString() +
                "\nchance_think=" + this.Chance_ttt_think.Value.ToString();

            this.SaveToFile("TicTacToe", content, this.games.ticTacToeboard, 1);
        }

        private void ImportConfig_Hangman()
        {
            try
            {
                string[] lines = File.ReadAllLines(this.games.path + "\\config\\Hangman.lps");
                if (lines.Length <= 0) return;

                foreach (string line in lines)
                {
                    string name = line.Split('=')[0];
                    string value = line.Split('=')[1];
                    if (name == null || value == null) continue;

                    if (name == "lives")
                        this.Lives_hm.Value = int.Parse(value);
                    else if (name == "isOnlyCustom")
                        this.Custom_words.IsChecked = bool.Parse(value);
                    else if (name == "chance_win")
                        this.Chance_hm_win.Value = double.Parse(value);
                    else if (name == "chance_loss")
                        this.Chance_hm_loss.Value = double.Parse(value);
                    else if (name == "chance_start")
                        this.Chance_hm_start.Value = double.Parse(value);
                    else if (name == "chance_correct")
                        this.Chance_hm_correct.Value = double.Parse(value);
                    else if (name == "chance_incorrect")
                        this.Chance_hm_incorrect.Value = double.Parse(value);
                }
            }
            catch (IOException e)
            {
            }
        }

        private void SaveConfig_Hangman(object sender, RoutedEventArgs e)
        {
            string content =
                "lives=" + this.Lives_hm.Value.ToString() +
                "\nisOnlyCustom=" + this.Custom_words.IsChecked.ToString() +
                "\nchance_win=" + this.Chance_hm_win.Value.ToString() +
                "\nchance_loss=" + this.Chance_hm_loss.Value.ToString() +
                "\nchance_start=" + this.Chance_hm_start.Value.ToString() +
                "\nchance_correct=" + this.Chance_hm_correct.Value.ToString() +
                "\nchance_incorrect=" + this.Chance_hm_incorrect.Value.ToString();

            this.SaveToFile("Hangman", content, this.games.hangmanBoard, 2);
        }

        private void ImportConfig_Millionaire()
        {
            try
            {
                string[] lines = File.ReadAllLines(this.games.path + "\\config\\Millionaire.lps");
                if (lines.Length <= 0) return;

                foreach (string line in lines)
                {
                    string name = line.Split('=')[0];
                    string value = line.Split('=')[1];
                    if (name == null || value == null) continue;

                    if (name == "song")
                        this.Mil_SongEnbaled.IsChecked = bool.Parse(value);
                    else if (name == "difficulty") {
                        int val = 0;
                        if(value == "easy") val = 1;
                        if (value == "medium") val = 2;
                        if (value == "hard") val = 3;
                        if (value == "real") val = 4;
                        this.Difficulty_mil.SelectedIndex = val;
                    }
                    else if (name == "category")
                        this.Category_mil.SelectedIndex = value == "0" ? 0 : int.Parse(value) - 8;
                    else if (name == "database")
                        this.Databases_mil.SelectedIndex = int.Parse(value);
                    else if (name == "chance_win")
                        this.Chance_mil_win.Value = double.Parse(value);
                    else if (name == "chance_loss")
                        this.Chance_mil_loss.Value = double.Parse(value);
                    else if (name == "chance_start")
                        this.Chance_mil_start.Value = double.Parse(value);
                    else if (name == "chance_wait")
                        this.Chance_mil_wait.Value = double.Parse(value);
                }
            }
            catch (IOException e)
            {
            }
        }

        private void SaveConfig_Millionaire(object sender, RoutedEventArgs e)
        {
            string content =
                "song=" + this.Mil_SongEnbaled.IsChecked.ToString() +
                "\ndifficulty=" + this.Difficulty_mil.SelectedIndex.ToString().Replace("0", "any").Replace("1", "easy").Replace("2", "normal").Replace("3", "hard").Replace("4", "real") +
                "\ncategory=" + (this.Category_mil.SelectedIndex == 0 ? 0 : 8 + this.Category_mil.SelectedIndex) +
                "\ndatabase=" + this.Databases_mil.SelectedIndex.ToString() +
                "\nchance_win=" + this.Chance_mil_win.Value.ToString() +
                "\nchance_loss=" + this.Chance_mil_loss.Value.ToString() +
                "\nchance_start=" + this.Chance_mil_start.Value.ToString() +
                "\nchance_wait=" + this.Chance_mil_wait.Value.ToString();

            this.SaveToFile("Millionaire", content, this.games.millionaireBoard, 3);
        }

        private void ImportConfig_Minesweeper()
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

                    if (name == "difficulty")
                        this.Mine_difficulty.SelectedIndex = int.Parse(value);
                    else if (name == "rows")
                        this.Rows_mine.Value = int.Parse(value);
                    else if (name == "cols")
                        this.Cols_mine.Value = int.Parse(value);
                    else if (name == "mines")
                        this.Mines_mine.Value = int.Parse(value);
                    else if (name == "cell_size")
                        this.CellSize_mine.Value = int.Parse(value);
                    else if (name == "chance_win")
                        this.Chance_mine_win.Value = double.Parse(value);
                    else if (name == "chance_loss")
                        this.Chance_mine_loss.Value = double.Parse(value);
                    else if (name == "chance_start")
                        this.Chance_mine_start.Value = double.Parse(value);
                    else if (name == "chance_wait")
                        this.Chance_mine_wait.Value = double.Parse(value);
                    else if (name == "isTimerSound")
                        this.Mine_isTimerSound.IsChecked = bool.Parse(value);
                    else if(name == "isMarkEnabled")
                        this.Mine_isMarkEnabled.IsChecked = bool.Parse(value);
                }                
            }
            catch (IOException e)
            {
            }
        }

        private void SaveConfig_Minesweeper(object sender, RoutedEventArgs e)
        {
            string content =
                "difficulty=" + this.Mine_difficulty.SelectedIndex.ToString() +
                "\nrows=" + this.Rows_mine.Value.ToString().Replace('.', '\0') +
                "\ncols=" + this.Cols_mine.Value.ToString().Replace('.', '\0') +
                "\nmines=" + this.Mines_mine.Value.ToString().Replace('.', '\0') +
                "\ncell_size=" + this.CellSize_mine.Value.ToString() +
                "\nchance_win=" + this.Chance_mine_win.Value.ToString() +
                "\nchance_loss=" + this.Chance_mine_loss.Value.ToString() +
                "\nchance_start=" + this.Chance_mine_start.Value.ToString() +
                "\nchance_wait=" + this.Chance_mine_wait.Value.ToString() +
                "\nisTimerSound=" + this.Mine_isTimerSound.IsChecked.ToString() +
                "\nisMarkEnabled=" + this.Mine_isMarkEnabled.IsChecked.ToString();

            this.SaveToFile("Minesweeper", content, this.games.minesweeperBoard, 4);
        }

        private void SaveToFile(string filename, string content, WindowX board, int gameId)
        {
            if (board == null)
            {
                try
                {
                    File.WriteAllText(this.games.path + "\\config\\" + filename + ".lps", content);
                }
                catch (Exception ex) { }
                return;
            }

            if (MessageBoxX.Show("An active game has been detected!\nDo you still want to overwrite the settings?".Translate(), "Restart the board".Translate(), MessageBoxButton.YesNo, MessageBoxIcon.Warning) == MessageBoxResult.No)
                return;

            try
            {
                File.WriteAllText(this.games.path + "\\config\\" + filename + ".lps", content);
            }
            catch (Exception ex) { }

            board.Close();

            if (gameId == 1)
                this.games.ticTacToeboard = null;
            else if (gameId == 2)
                this.games.hangmanBoard = null;
            else if (gameId == 3)
                this.games.millionaireBoard = null;
            else if ( gameId == 4)
                this.games.minesweeperBoard = null;

            this.games.ShowPanel(gameId);
        }

        private void Mine_Slider_Custom(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            if (this.Cols_mine is Slider && this.Rows_mine is Slider && this.Mines_mine is Slider)
            {
                double sliderRows = this.Rows_mine.Value;
                double sliderCols = this.Cols_mine.Value;
                double sliderMines = this.Mines_mine.Value;
                string difficultyTag = sliderCols + "_" + sliderRows + "_" + sliderMines;

                if (slider.Name != "Mines_mine")
                    this.Mines_mine.Maximum = Math.Round((double)(sliderRows * sliderCols) / (10.0 / 3.0), 2);

                if(difficultyTag == "9_9_10")
                    this.Mine_difficulty.SelectedIndex = 0;
                else if (difficultyTag == "16_16_40")
                    this.Mine_difficulty.SelectedIndex = 1;
                else if (difficultyTag == "30_16_99" || difficultyTag == "16_30_99")
                    this.Mine_difficulty.SelectedIndex = 2;
                else if (difficultyTag == "30_24_160" || difficultyTag == "24_30_160")
                    this.Mine_difficulty.SelectedIndex = 3;
                else if (difficultyTag == "30_30_270")
                    this.Mine_difficulty.SelectedIndex = 4;
                else
                    this.Mine_difficulty.SelectedIndex = 5;
            }
        }

        private void ChangeDifficulty(object sender, SelectionChangedEventArgs e)
        {
            if(this.Mine_difficulty.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Tag == null || selectedItem.Name == "Mine_CustomItem") return;
                string[] param = selectedItem.Tag.ToString().Split('_');

                if (param.Length != 3) return;
                if (this.Rows_mine is Slider)
                    this.Rows_mine.Value = int.Parse(param[1]);

                if (this.Cols_mine is Slider)
                    this.Cols_mine.Value = int.Parse(param[0]);

                if (this.Cols_mine is Slider && this.Rows_mine is Slider && this.Mines_mine is Slider)
                {
                    this.Mines_mine.Maximum = Math.Round((double)(this.Rows_mine.Value * this.Cols_mine.Value) / (10.0 / 3.0), 2);
                    this.Mines_mine.Value = int.Parse(param[2]);
                }
            }
        }
    }
}
