using System.Text;
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

namespace HomeworkLab15
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private float totalSeconds;
        private float currentSeconds;
        public MainWindow()
        {
            InitializeComponent();
            TimerSelect.Items.Add(15);
            TimerSelect.Items.Add(25);
            TimerSelect.Items.Add(45);

        }
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            InitializeTimer();
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetTimer();
        }

        private void TimerSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TimerSelect.SelectedItem != null)
            {
                int minutes = (int)TimerSelect.SelectedItem;
                totalSeconds = minutes * 60;
                currentSeconds = totalSeconds;
                UpdateTimerDisplay();
            }
        }
        private void InitializeTimer()
        {
            _timer.Interval = TimeSpan.FromSeconds(.1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if(currentSeconds > 0)
            {
                currentSeconds -= 0.1f;
                UpdateTimerDisplay();
            }
            else
            {
                _timer.Stop();
                MessageBox.Show("Время вышло!", "Таймер", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void UpdateTimerDisplay()
        {
            int minutes = (int)(currentSeconds / 60);
            int seconds = (int)(currentSeconds % 60);
            Timer.Text = $"{minutes:D2}:{seconds:D2}";
        }
        private void ResetTimer()
        {
            _timer.Stop();
            currentSeconds = totalSeconds;
            UpdateTimerDisplay();
        }
    }
}