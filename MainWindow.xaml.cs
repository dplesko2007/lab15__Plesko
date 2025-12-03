using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace lab15__Plesko
{
    public partial class MainWindow : Window
    {
        private const int TotalPairs = 10;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private int tenthOfSeconds;
        private int matchesFound;
        private readonly Random _random = new Random();
        TextBlock _lastTextBlockClicked;
        private bool _findingMatch = false;
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            SetUpGame();
        }

        private void InitializeTimer()
        {
            _timer.Interval = TimeSpan.FromSeconds(.1);
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSeconds++;
            timerTick.Text = $"{tenthOfSeconds/10F:0.0s}";
            if (matchesFound == TotalPairs)
            {
                _timer.Stop();
                timerTick.Text += "Play again";
            }
        }
        private readonly List<String> animalEmoji = new List<String>() {
                "🐶","🐶",
                "🐵","🐵",
                "🐱","🐱",
                "🦊","\U0001f98a",
                "🐭","🐭",
                "🐉","🐉",
                "😺","😺",
                "🐨","🐨",
                "🦧","\U0001f9a7",
                "🐒","🐒"

            };
        private void SetUpGame()
        {
            AssignRandomEmojis();
            _timer.Start();
            tenthOfSeconds = 0;
            matchesFound = 0;

        }
        

        private void TextBlock(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is TextBlock clickedTextBlock && clickedTextBlock.Visibility == Visibility.Visible))
            {
                return;
            }
            if (!_findingMatch)
            {
                clickedTextBlock.Visibility = Visibility.Hidden;
                _lastTextBlockClicked = clickedTextBlock;
                _findingMatch = true;
                return;
            }
            if (clickedTextBlock == _lastTextBlockClicked)
            {
                return;
            }
            if (clickedTextBlock.Text == _lastTextBlockClicked.Text)
            {
                clickedTextBlock.Visibility = Visibility.Hidden;
                matchesFound++;
            }

            else
            {
                _lastTextBlockClicked.Visibility = Visibility.Visible;
            }
            _findingMatch = false;
        }

        private void Timer_Click(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == TotalPairs)
            {
                SetUpGame();
            }
        }

        private void AssignRandomEmojis()
        {
            foreach (var textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timerTick")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = _random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }

            }
        }
    }
}
