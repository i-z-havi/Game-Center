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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game_Center.Projects.SimonSays
{
    /// <summary>
    /// Interaction logic for Simon.xaml
    /// </summary>
    public partial class Simon : Window
    {
        private List<string> _gamepattern = new();
        private List<string> _userpattern = new();
        private string[] _buttons = new string[] {"Blue","Yellow","Red","Green" };
        private int _stage = 0;
        private int _score = 0;
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        private bool _isStarted = false;
        private bool _isRunning=false;

        public Simon()
        {
            InitializeComponent(); 
        }
        private void StartGame(object sender, RoutedEventArgs e)
        {
            InstructionBlurb.Content = $"Level {_stage}";
            StartBtn.IsEnabled = false;
            StartBtn.Visibility = Visibility.Collapsed;
            _isStarted=true;
            NextBatch();
        }

        private void CheckAnswer(object sender, MouseButtonEventArgs e)
        {
            if (_isStarted)
            {
                Image img = (Image)sender;
                _userpattern.Add(img.Name);
                if (_gamepattern[_stage] == _userpattern[_stage])
                {
                    _stage++;
                    if (_gamepattern.Count == _userpattern.Count)
                    {
                        img.Opacity = 1;
                        NextBatch();
                    }
                    
                }
                else
                {
                    StartBtn.Content = "Try Again?";
                    StartBtn.Visibility = Visibility.Visible;
                    StartBtn.IsEnabled = true;
                    InstructionBlurb.Content = $"Game Over! Your score was {_score+1}. Press the button to try again!";
                    _stage = 0;
                    _gamepattern.Clear();
                    _userpattern.Clear();
                    _isStarted = false;
                }
            }
        }

        private async void NextBatch()
        {
            _isRunning=true;
            _userpattern.Clear();
            InstructionBlurb.Content = $"Level {_stage}";
            Random rnd = new Random();
            int newButton = rnd.Next(0, 4);
            _gamepattern.Add(_buttons[newButton]);
            foreach (string button in _gamepattern)
            {
                switch (button)
                {
                    case "Yellow":
                        Yellow.Opacity = 0.3;
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        Yellow.Opacity = 1;
                        break;
                    case "Blue":
                        Blue.Opacity = 0.3;
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        Blue.Opacity = 1;
                        break;
                    case "Red":
                        Red.Opacity = 0.3;
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        Red.Opacity = 1;
                        break;
                    case "Green":
                        Green.Opacity = 0.3;
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        Green.Opacity = 1;
                        break;
                }
                await Task.Delay(TimeSpan.FromSeconds(0.3));
            }
            _score = _stage;
            _stage = 0;
            _isRunning = false;
        }

        private void Mouse_Entered(object sender, MouseEventArgs e)
        {
            if (!_isRunning) { (sender as Image)!.Opacity = 0.5; }
 
        }

        private void Mouse_Exit(object sender, MouseEventArgs e)
        {
            if (!_isRunning) { (sender as Image)!.Opacity = 1; }
        }
    }
}
