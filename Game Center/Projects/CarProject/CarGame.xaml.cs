using Game_Center.Projects.CarProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Game_Center.Projects.CarProject
{
    /// <summary>
    /// Interaction logic for CarGame.xaml
    /// </summary>
    public partial class CarGame : Window
    {
            private PlayerCar playerCar;
            private List<Obstacle> obstacles;
            private Random random;
            private DispatcherTimer gameTimer = new DispatcherTimer();
            private int score = 0;
            private int scorecounter = 0;
            bool gameOver = false;

        public CarGame()
        {
            InitializeComponent();
            playerCar = new PlayerCar(200, 300, 10, playerCarImage);
            obstacles = new List<Obstacle>();
            random = new Random();
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
        }
        private void playBtnClick(object sender, RoutedEventArgs e)
        {
            uiCanvas.Visibility = Visibility.Collapsed;
            backgroundVideo.Source = new Uri("Media/CarVideo.mp4", UriKind.Relative);
            backgroundMusic.Source = new Uri("./Media/Cipher2.mp3", UriKind.RelativeOrAbsolute);
            backgroundVideo.Play();
            backgroundMusic.Play();
            gameCanvas.Visibility = Visibility.Visible;
            gameTimer.Start();
            playBtn.Visibility = Visibility.Collapsed;
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            backgroundVideo.Position = TimeSpan.Zero;
            backgroundVideo.Play();
        }

        private void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }


        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Left:
                    playerCar.LeftKeyPressed = true;
                    break;
                case Key.Right:
                    playerCar.RightKeyPressed = true;
                    break;
            }
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Left:
                    playerCar.LeftKeyPressed = false;
                    break;
                case Key.Right:
                    playerCar.RightKeyPressed = false;
                    break;
            }
        }

        private void GameLoop(object sender, EventArgs e)
        {
            playerCar.Move();
            scorecounter++;
            if (scorecounter == 50)
            {
                scorecounter = 0;
                scoreText.Text = "Score: " + score++;
            }
            foreach (var item in obstacles)
            {
                item.Move();
            }
            if (random.Next(0, 50) == 1)
            {
                Image obstacleImage = new Image();
                obstacleImage.Source = new BitmapImage(new Uri("/Images/Bomb.png", UriKind.Relative));
                obstacleImage.Width = 50;
                obstacleImage.Height = 50;
                Obstacle obstacle = new Obstacle(random.Next(0, (int)Width), 0, 2, obstacleImage);
                obstacles.Add(obstacle);
                gameCanvas.Children.Add(obstacleImage);
            }
            foreach (Obstacle item in obstacles)
            {
                if (playerCar.Hitbox.IntersectsWith(item.Hitbox))
                {
                    gameOver = true;
                    break;
                }
            }
            //this way we shouldnt get an error for clearing the list while iterating through it

            if (gameOver)
            {
                backgroundMusic.Stop();
                backgroundVideo.Stop();
                gameTimer.Stop();
                if (MessageBox.Show("Game Over! Your score was " + score + ". Would you like to try again?", "Game Over", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    gameOver = false;
                    score = 0;
                    scoreText.Text = "Score: ";
                    scorecounter = 0;
                    backgroundMusic.Play();
                    backgroundVideo.Play();
                    gameTimer.Start();
                    playerCar.X = 200;
                    playerCar.LeftKeyPressed = false;
                    playerCar.RightKeyPressed = false;
                    Type t = typeof(Obstacle);
                    for (int i = gameCanvas.Children.Count - 1; i >= 0; i--)
                    {
                        if (gameCanvas.Children[i] is Image img)
                        {
                            if (img.Width == 50)
                            {
                                gameCanvas.Children.RemoveAt(i);
                            }
                        }
                    }
                    obstacles.Clear();
                }
                else
                {
                    Close();
                }
            }
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show($"Failed to load image: {e.ErrorException.Message}");
        }

    }
}
