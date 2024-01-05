using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using Panuon.WPF.UI;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows;
using LinePutScript.Localization.WPF;
using System.Windows.Threading;
using System.IO;
using System.Linq;

namespace VPet.Plugin.FunGames
{
    public partial class MillionaireBoard : WindowX
    {
        private bool isSong = true;
        private string difficulty = "easy";
        private int category = 0;
        private int database = 0;
        private double chance_win = 0.5;
        private double chance_lose = 0.5;
        private double chance_start = 1;
        private double chance_wait = 1;
        private bool isRealDifficult = false;

        List<Question> dbSnapshot = new List<Question>();
        private bool obtainPrize = false;
        private bool hasLive = true;
        private int currentState = 0;
        private FunGames games = null;
        private Random random = null;
        private Image correctAnswerImage = null;
        private List<TextBlock> answersText = null;
        private DispatcherTimer inactivityTimer = null;
        private DialogueForMillionaire dialogue = new DialogueForMillionaire();
        private int[] statePrize = { 100, 200, 300, 500, 1000, 2000, 4000, 8000, 16000, 32000, 64000, 125000, 250000, 500000, 1000000 };
        private string[] stateText = { "1 ✦ $100", "2 ✦ $200", "3 ✦ $300", "4 ✦ $500", "5 ✦ $1 000", "6 ✦ $2 000", "7 ✦ $4 000", "8 ✦ $8 000", "9 ✦ $16 000", "10 ✦ $32 000", "11 ✦ $64 000", "12 ✦ $125 000", "13 ✦ $250 000", "14 ✦ $500 000", "15 ✦ $1 000 000" };

        private BitmapImage answer_default = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Millionaire_answer.png", UriKind.Relative));
        private BitmapImage answer_selected = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Millionaire_answer_selected.png", UriKind.Relative));
        private BitmapImage answer_correct = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Millionaire_answer_correct.png", UriKind.Relative));
        private BitmapImage answer_disabled = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Millionaire_answer_disabled.png", UriKind.Relative));

        private bool hasLifelines5050 = true;
        private bool hasLifelinesAskHost = true;
        private bool hasLifelinesSwitchQuestion = true;

        private BitmapImage lifelines_50_50 = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Millionaire_50_50.png", UriKind.Relative));
        private BitmapImage lifelines_askHost = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Millionaire_askHost.png", UriKind.Relative));
        private BitmapImage lifelines_switchQuestion = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Millionaire_switch.png", UriKind.Relative));

        private BitmapImage lifelines_50_50_used = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Millionaire_50_50_used.png", UriKind.Relative));
        private BitmapImage lifelines_askHost_used = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Millionaire_askHost_used.png", UriKind.Relative));
        private BitmapImage lifelines_switchQuestion_used = new BitmapImage(new Uri("/VPet.Plugin.FunGames;Component/Image/Millionaire_switch_used.png", UriKind.Relative));

        public MillionaireBoard(FunGames games)
        {
            this.games = games;
            this.ImportConfig();

            if (this.database == 1) {
                if (MessageBoxX.Show("This mini game requires an internet connection,\nquestions are obtained from: https://opentdb.com".Translate(), "Internet connection required".Translate(), MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    this.games.millionaireBoard = (MillionaireBoard)null;
                    this.games.petBoard.SetVisibility(false);
                    this.Close();
                    return;
                }
            }
            else
                this.GenerateJsonDatabase();

            this.games.SendRandomMsg(this.dialogue.start, chance_start);

            if (this.difficulty == "real")
            {
                this.difficulty = "easy";
                this.isRealDifficult = true;
            }
            else if (this.difficulty == "any")
                this.difficulty = "0";

            this.InitializeComponent();
            this.AnswerA.MouseDown += (sender, e) => { this.CheckAnswer(this.AnswerA_Image); };
            this.AnswerB.MouseDown += (sender, e) => { this.CheckAnswer(this.AnswerB_Image); };
            this.AnswerC.MouseDown += (sender, e) => { this.CheckAnswer(this.AnswerC_Image); };
            this.AnswerD.MouseDown += (sender, e) => { this.CheckAnswer(this.AnswerD_Image); };

            this.inactivityTimer = new DispatcherTimer();
            this.inactivityTimer.Interval = TimeSpan.FromSeconds(45);
            this.inactivityTimer.Tick += this.InactivityTimer;

            this.GetQustion();
        }

        private void ImportConfig()
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
                        this.isSong = bool.Parse(value);
                    else if (name == "difficulty")
                        this.difficulty = value;
                    else if (name == "category")
                        this.category = int.Parse(value);
                    else if (name == "database")
                        this.database = int.Parse(value);
                    else if (name == "chance_win")
                        this.chance_win = double.Parse(value);
                    else if (name == "chance_loss")
                        this.chance_lose = double.Parse(value);
                    else if (name == "chance_start")
                        this.chance_start = double.Parse(value);
                    else if (name == "chance_wait")
                        this.chance_wait = double.Parse(value);
                }
            }
            catch (IOException e)
            {
            }
        }

        private void GenerateJsonDatabase()
        {
            try
            {
                string[] lines = File.ReadAllLines(this.games.path + "\\config\\Millionaire_questions.lps");
                if (lines.Length <= 0) return;

                foreach (string line in lines)
                {
                    string[] questionElements = line.Split(new string[] { ":|" }, StringSplitOptions.None);
                    if (questionElements == null) continue;
                    this.dbSnapshot.Add(new Question { 
                        tagId = int.Parse(questionElements[0]),
                        difficulty = questionElements[1],
                        question = questionElements[2],
                        correct_answer = questionElements[3],
                        incorrect_answer_1 = questionElements[4],
                        incorrect_answer_2 = questionElements[5],
                        incorrect_answer_3 = questionElements[6],
                    });
                }
            }
            catch (IOException e)
            {
            }
        }

        public void GetQustion()
        {
            if(this.isRealDifficult)
            {
                if(this.currentState >= 9)
                    this.difficulty = "hard";
                else if (this.currentState >= 4)
                    this.difficulty = "medium";
            }

            if (this.database == 1)
                this.GetQustion_Online();
            else
                this.GetQustion_Offline();
        }

        public void GetQustion_Offline()
        {
            List<Question> filteredQuestions = new List<Question> ();
            if(this.category == 0 && this.difficulty == "0")
                filteredQuestions = this.dbSnapshot.ToList();
            else if (this.category == 0 && this.difficulty != "0")
                filteredQuestions = this.dbSnapshot.Where(q => q.difficulty == this.difficulty).ToList();
            else if (this.category != 0 && this.difficulty == "0")
                filteredQuestions = this.dbSnapshot.Where(q => q.tagId == this.category).ToList();
            else if (this.category != 0 && this.difficulty != "0")
                filteredQuestions = this.dbSnapshot.Where(q => q.tagId == this.category && q.difficulty == this.difficulty).ToList();

            this.random = new Random();
            int randomIndex = random.Next(0, filteredQuestions.Count);
            Question randomQuestion = filteredQuestions[randomIndex];

            this.ChangeQuestion(randomQuestion.question, randomQuestion.correct_answer, randomQuestion.incorrect_answer_1, randomQuestion.incorrect_answer_2, randomQuestion.incorrect_answer_3);
            this.Prize_Text.Text = this.stateText[this.currentState];
        }

        public async void GetQustion_Online()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://opentdb.com/api.php?amount=1&type=multiple&difficulty=" + this.difficulty + "&category=" + this.category;
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (!response.IsSuccessStatusCode) return;
                        
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    RootObject root = JsonConvert.DeserializeObject<RootObject>(jsonContent);

                    if (root?.results?.Count == 0)
                    {
                        MessageBoxX.Show("Question not found. Change the difficulty level or category.".Translate(), "Question not found".Translate());
                        return;
                    }

                    this.ChangeQuestion(root.results[0].question, root.results[0].correct_answer, root.results[0].incorrect_answers[0], root.results[0].incorrect_answers[1], root.results[0].incorrect_answers[2]);
                    this.Prize_Text.Text = this.stateText[this.currentState];
                }
                catch (Exception ex) { }
            }
        }

        private void ChangeQuestion(string question, string correctAnswer, string incorrectAnswer1, string incorrectAnswer2, string incorrectAnswer3)
        {
            this.inactivityTimer.Start();
            
            if(this.isSong)
                this.games.PlaySFX(2);
            this.AnswerA.IsEnabled = true;
            this.AnswerB.IsEnabled = true;
            this.AnswerC.IsEnabled = true;
            this.AnswerD.IsEnabled = true;

            this.AnswerA_Image.Source = answer_default;
            this.AnswerB_Image.Source = answer_default;
            this.AnswerC_Image.Source = answer_default;
            this.AnswerD_Image.Source = answer_default;
            this.Question_Text.Text = this.WrapText(this.ConvertText(question));

            this.answersText = ShuffleList(new List<TextBlock> { this.AnswerA_Text, this.AnswerB_Text, this.AnswerC_Text, this.AnswerD_Text });

            this.answersText[0].Text = this.ConvertText(correctAnswer);
            this.answersText[1].Text = this.ConvertText(incorrectAnswer1);
            this.answersText[2].Text = this.ConvertText(incorrectAnswer2);
            this.answersText[3].Text = this.ConvertText(incorrectAnswer3);

            if(this.AnswerA_Text.Text == correctAnswer)
                this.correctAnswerImage = this.AnswerA_Image;
            else if (this.AnswerB_Text.Text == correctAnswer)
                this.correctAnswerImage = this.AnswerB_Image;
            else if (this.AnswerC_Text.Text == correctAnswer)
                this.correctAnswerImage = this.AnswerC_Image;
            else if (this.AnswerD_Text.Text == correctAnswer)
                this.correctAnswerImage = this.AnswerD_Image;
        }

        private string ConvertText(string text) {
            return new Regex(@"&[^;]+;").Replace(text.Replace("&quot;", "\""), "").Translate().Replace("&!", "#");
        }

        private string WrapText(string text)
        {
            int maxLength = 75;

            string[] words = text.Split(' ');
            int currentLineLength = 0;
            string wrappedText = "";
            int margin = 31;

            foreach (string word in words)
            {
                if (currentLineLength + word.Length + 1 <= maxLength)
                {
                    wrappedText += word + " ";
                    currentLineLength += word.Length + 1;
                }
                else
                {
                    margin -= 7;
                    wrappedText += "\n" + word + " ";
                    currentLineLength = word.Length + 1;
                }
            }

            this.Question_Text.Margin = new Thickness(58, margin, 58, 0);
            return wrappedText.Trim();
        }

        private async void CheckAnswer(Image myAnswerImage)
        {
            this.inactivityTimer.Stop();
            myAnswerImage.Source = answer_selected;
            this.AnswerA.IsEnabled = false;
            this.AnswerB.IsEnabled = false;
            this.AnswerC.IsEnabled = false;
            this.AnswerD.IsEnabled = false;

            if (this.isSong)
                this.games.PlaySFX(3);

            await Task.Delay(5 * 1000);
            this.correctAnswerImage.Source = answer_correct;

            if (myAnswerImage == this.correctAnswerImage)
            {
                this.currentState++;
                if (this.isSong)
                    this.games.PlaySFX(4);

                await Task.Delay(6 * 1000);
                if (this.currentState == 15)
                    this.WinSalary();
                else
                    this.GetQustion();
            }
            else
            {
                this.hasLive = false;
                if (this.isSong)
                    this.games.PlaySFX(5);
                this.WinSalary();
                this.games.SendRandomMsg(this.dialogue.loss, chance_lose);
            }
        }

        private void WinSalary()
        {
            if (this.currentState == 0 || this.obtainPrize) return;
            int prize;

            if (this.hasLive)
                prize = this.statePrize[this.currentState - 1];
            else
            {
                int supportState = 0;
                if(this.currentState > 9)
                    supportState = 9;
                if (this.currentState > 4)
                    supportState = 4;
                if (supportState == 0) return;
                prize = this.statePrize[supportState];
            }

            if (prize == 0) return;
            this.random = new Random();
            double randomValue = random.NextDouble() * (0.02 - 0.005) + 0.005;
            int actualPrize = (int)(prize * randomValue);

            this.games.MW.Main.Core.Save.Money += actualPrize;
            this.obtainPrize = true;

            if (this.random.NextDouble() < 1 - this.chance_win) return;
            int index = this.random.Next(this.dialogue.win.Count);
            if (index < 0) return;
            string msgContent = this.dialogue.win[index];
            if (msgContent == null) return;

            try
            {
                this.games.MW.Main.Say(msgContent.Translate().Replace("{WIN_PRICE}", prize.ToString()).Replace("{WIN_PRICE_AFTER}", actualPrize.ToString()));
            }
            catch (Exception ex) { }
        }

        private void Lifelines_50_50(object sender, RoutedEventArgs e)
        {
            if (!this.hasLive || !this.hasLifelines5050) return;
            if (this.currentState == 14)
            {
                this.games.SendMsg("You can't use the lifeline on the last question!");
                return;
            }

            if (this.isSong)
                this.games.PlaySFX(6);
            this.hasLifelines5050 = false;
            this.Lifelines_50_50_Image.IsEnabled = false;
            this.Lifelines_50_50_Image.Source = this.lifelines_50_50_used;
            if (this.AnswerA_Image == this.correctAnswerImage || this.AnswerD_Image == this.correctAnswerImage)
            {
                this.AnswerB.IsEnabled = false;
                this.AnswerC.IsEnabled = false;
                this.AnswerB_Image.Source = this.answer_disabled;
                this.AnswerC_Image.Source = this.answer_disabled;
            }
            else if (this.AnswerB_Image == this.correctAnswerImage || this.AnswerC_Image == this.correctAnswerImage)
            {
                this.AnswerA.IsEnabled = false;
                this.AnswerD.IsEnabled = false;
                this.AnswerA_Image.Source = this.answer_disabled;
                this.AnswerD_Image.Source = this.answer_disabled;
            }
        }

        private void Lifelines_SwitchQuestion(object sender, RoutedEventArgs e)
        {
            if (!this.hasLive || !this.hasLifelinesSwitchQuestion) return;
            if (this.currentState == 14)
            {
                this.games.SendMsg("You can't use the lifeline on the last question!");
                return;
            }

            if (this.isSong)
                this.games.PlaySFX(6);
            this.hasLifelinesSwitchQuestion = false;
            this.Lifelines_switch_Image.IsEnabled = false;
            this.Lifelines_switch_Image.Source = this.lifelines_switchQuestion_used;
            this.GetQustion();
        }

        private void Lifelines_AskHost(object sender, RoutedEventArgs e)
        {
            if (!this.hasLive || !this.hasLifelinesAskHost) return;
            if (this.currentState == 14)
            {
                this.games.SendMsg("You can't use the lifeline on the last question!");
                return;
            }

            if (this.isSong)
                this.games.PlaySFX(6);
            this.random = new Random();
            this.hasLifelinesAskHost = false;
            this.Lifelines_askHost_Image.IsEnabled = false;
            this.Lifelines_askHost_Image.Source = this.lifelines_askHost_used;
            List<string> dialogueList = new List<string>();

            if (this.random.NextDouble() > 0.3 || !this.hasLifelines5050)
                dialogueList = this.dialogue.askHost_correct;
            else
                dialogueList = this.dialogue.askHost_wrong;

            int index = this.random.Next(dialogueList.Count);
            if (index < 0) return;
            string msgContent = dialogueList[index];
            if (msgContent == null) return;

            try
            {
                this.games.MW.Main.Say(msgContent.Translate().Replace("{CORRECT_ANSWER}", answersText[0].Text).Replace("{WRONG_ANSWER}", answersText[1].Text));
            } catch (Exception ex) { }

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

        private void Hover_Enbaled(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Hover_Disabled(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void RestartButton(object sender, RoutedEventArgs e)
        {
            this.inactivityTimer.Stop();
            this.WinSalary();
            this.hasLive = true;
            this.currentState = 0;
            this.GetQustion();

            this.hasLifelines5050 = true;
            this.hasLifelinesAskHost = true;
            this.hasLifelinesSwitchQuestion = true;

            this.Lifelines_50_50_Image.IsEnabled = true;
            this.Lifelines_askHost_Image.IsEnabled = true;
            this.Lifelines_switch_Image.IsEnabled = true;

            this.Lifelines_50_50_Image.Source = this.lifelines_50_50;
            this.Lifelines_askHost_Image.Source = this.lifelines_askHost;
            this.Lifelines_switch_Image.Source = this.lifelines_switchQuestion;

            this.obtainPrize = false;
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            this.WinSalary();
            this.inactivityTimer.Stop();
            this.games.millionaireBoard = (MillionaireBoard) null;
            this.games.petBoard.SetVisibility(false);
            this.Close();
        }

        private void InactivityTimer(object sender, EventArgs e)
        {
            this.inactivityTimer.Stop();
            if (this.games.millionaireBoard == null) return;
            this.games.SendRandomMsg(this.dialogue.waiting, this.chance_wait);
            this.inactivityTimer.Start();
        }

        private class Result
        {
            public string category { get; set; }
            public string type { get; set; }
            public string difficulty { get; set; }
            public string question { get; set; }
            public string correct_answer { get; set; }
            public List<string> incorrect_answers { get; set; }
        }

        private class RootObject
        {
            public int response_code { get; set; }
            public List<Result> results { get; set; }
        }

        static List<T> ShuffleList<T>(List<T> inputList)
        {
            List<T> randomList = new List<T>(inputList);
            Random rng = new Random();

            int n = randomList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = randomList[k];
                randomList[k] = randomList[n];
                randomList[n] = value;
            }

            return randomList;
        }
    }
    public class Question
    {
        public int tagId { get; set; }
        public string difficulty { get; set; }
        public string question { get; set; }
        public string correct_answer { get; set;}
        public string incorrect_answer_1 { get; set; }
        public string incorrect_answer_2 { get; set; }
        public string incorrect_answer_3 { get; set; }
    }
}
