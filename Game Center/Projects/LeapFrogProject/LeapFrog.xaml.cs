using Game_Center.Projects.LeapFrogProject.Models;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Game_Center.Projects.LeapFrogProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LeapFrog : Window
    {
        private DispatcherTimer gameTimer = new DispatcherTimer();
        private Line _line = new Line();
        private Point _referencePoint = new Point();
        private Point _clickedPoint = new Point();
        private Vector _vector = new Vector();
        private Point _endPoint = new Point();
        private Rect left = OuterBounds.CreateRect(0);
        private Rect right = OuterBounds.CreateRect(765);
        private bool _isAnimated = false;
        private bool _vectorStarted = false;
        private double score = 0;
        private double tries = 0;

        //todo: graphics, better score system, game over on outer bounds, instructions on presentation page, maybe split into objects?
        public LeapFrog()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            Image leftwall = OuterBounds.OuterBoundsRect(left);
            Image rightwall = OuterBounds.OuterBoundsRect(right);
            WindowCanvas.Children.Add(leftwall);
            WindowCanvas.Children.Add(rightwall);
            _line.Stroke = new SolidColorBrush(Colors.White);
            WindowCanvas.Children.Add(_line);
            gameTimer.Interval = TimeSpan.FromMilliseconds(16.6);
            gameTimer.Tick += CheckOverlap;
            gameTimer.Start();
        }

        private void CheckOverlap(object sender, EventArgs e)
        {
            Rect platformhitbox = new Rect(Canvas.GetLeft(lilyplatform), Canvas.GetTop(lilyplatform), lilyplatform.Width, lilyplatform.Height);
            TranslateTransform cubeTransform = FrogCube.RenderTransform as TranslateTransform;
            if (cubeTransform!=null)
            {
                Rect FrogCubehitbox = new Rect(Canvas.GetLeft(FrogCube)+cubeTransform.X, Canvas.GetTop(FrogCube)+cubeTransform.Y, FrogCube.Width, FrogCube.Height);
                if (left.IntersectsWith(FrogCubehitbox)||right.IntersectsWith(FrogCubehitbox)||FrogCubehitbox.X<left.X||FrogCubehitbox.X>right.X)
                {
                    Canvas.SetLeft(FrogCube, Canvas.GetLeft(FrogCube) + cubeTransform.X);
                    Canvas.SetTop(FrogCube, Canvas.GetTop(FrogCube) + cubeTransform.Y);
                    FrogCube.RenderTransform = null;
                    if (MessageBox.Show("Game over! Would you like to try again?", "Game Over", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {

                        StartOver();
                    }
                    else
                    {
                        Close();
                    }
                }

                if ((platformhitbox.X-24<FrogCubehitbox.X&&FrogCubehitbox.X<platformhitbox.X+50&& (FrogCubehitbox.Y == platformhitbox.Y - 26)|| platformhitbox.IntersectsWith(FrogCubehitbox)))
                {
                    score++;
                    Score.Text = $"Score: {score}";
                    if (score==10)
                    {
                        if(MessageBox.Show("Congratulations! Your score was "+(int)(score/tries*100)+". Would you like to try again?","Game Over", MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                        {
                            StartOver();
                        }
                        else
                        {
                            Close();
                        }
                    }
                    else
                    {
                        Canvas.SetLeft(FrogCube, Canvas.GetLeft(FrogCube) + cubeTransform.X);
                        Canvas.SetTop(FrogCube, platformhitbox.Y - 49);
                        FrogCube.RenderTransform = null;
                        Random rnd = new Random();
                        //checks to see if we move platform left or right
                        if (rnd.Next(2) == 0)
                        {
                            //if we decide to move platform left:
                            //if the platform is too much to the left, we dont want it to move even further left. 
                            if (Canvas.GetLeft(lilyplatform) - 75 < 20)
                            {
                                Canvas.SetLeft(lilyplatform, rnd.Next(((int)Canvas.GetLeft(FrogCube)) + 75, 750));
                            }
                            //otherwise, we move it left as intended
                            else
                            {
                                Canvas.SetLeft(lilyplatform, rnd.Next(20, (int)Canvas.GetLeft(FrogCube) - 50));
                            }
                        }
                        else
                        {
                            //same check but right wise this time
                            if (Canvas.GetLeft(lilyplatform) + 75 > 251)
                            {
                                Canvas.SetLeft(lilyplatform, rnd.Next(20, (int)Canvas.GetLeft(FrogCube) - 50));
                            }
                            //otherwise, we move it right as intended
                            else
                            {
                                Canvas.SetLeft(lilyplatform, rnd.Next(((int)Canvas.GetLeft(FrogCube)) + 75, 750));
                            }
                        }
                        platformhitbox.X = Canvas.GetLeft(lilyplatform);
                    }
                }
                    
            }
        }

        private void FrogCubeClicked(object sender, MouseButtonEventArgs e)
        {
            if (!_isAnimated)
            {
                _vectorStarted = true;
                _clickedPoint = e.GetPosition(this);
            }
        }

        private void VectorEnded(object sender, MouseButtonEventArgs e)
        {
            if (_vectorStarted && e.GetPosition(this).Y > _clickedPoint.Y && !_isAnimated)
            {
                FrogCube.Source = new BitmapImage(new Uri("/Images/FrogJump.png", UriKind.Relative));
                tries++;
                Tries.Text = $"Tries: {tries}";
                _vectorStarted = false;
                _line.Stroke = null;
                //since the initialization is y=0, afterwards endpoint y!=0
                //the second time it IS, so we change it AFTER the animation plays, so that the animation and actual X change dont conflict
                if (_endPoint.Y != 0)
                {
                    Canvas.SetLeft(FrogCube, _endPoint.X - 17.5);
                }
                _vector.X = _clickedPoint.X - e.GetPosition(this).X;
                _vector.Y = _clickedPoint.Y - e.GetPosition(this).Y;
                _clickedPoint = new Point(Canvas.GetLeft(FrogCube) + 17, Canvas.GetTop(FrogCube) + 17);
                _vector *= 5;
                Point controlPoint = Point.Add(_clickedPoint, _vector);
                _endPoint = new Point(controlPoint.X + _vector.X, _clickedPoint.Y);
                DoubleAnimationUsingPathExample();
            }
        }

        private void DrawVector(object sender, MouseEventArgs e)
        {
            if (_vectorStarted&&!_isAnimated)
            {
                _line.Stroke = Brushes.White;
                _referencePoint = e.GetPosition(this);
                _line.X1 = _clickedPoint.X;
                _line.Y1 = _clickedPoint.Y;
                _line.X2 = _referencePoint.X;
                _line.Y2 = _referencePoint.Y;
            }
        }

        private void DoubleAnimationUsingPathExample()
        {
                //set is animated to true so that other functions can check state
                _isAnimated= true;
                PathFigure tempPath = new PathFigure();
                tempPath.StartPoint = new Point(0, 0);
                Point control = new Point();
                control = Point.Add(tempPath.StartPoint, _vector);
                Point endPoint = new Point(control.X + _vector.X, 0);
                QuadraticBezierSegment qBezierSegment = new QuadraticBezierSegment(control, endPoint, true);
                tempPath.Segments.Add(qBezierSegment);
                NameScope.SetNameScope(this, new NameScope());

                // Create a transform. This transform
                // will be used to move the rectangle.
                TranslateTransform animatedTranslateTransform = new TranslateTransform();

                // Register the transform's name with the page
                // so that they it be targeted by a Storyboard.
                this.RegisterName("AnimatedTranslateTransform", animatedTranslateTransform);
                FrogCube.RenderTransform = animatedTranslateTransform;

                //create pathgeometry
                PathGeometry animationPath = new PathGeometry();
                animationPath.Figures.Add(tempPath);

                // Create a DoubleAnimationUsingPath to move the
                // rectangle horizontally along the path by animating
                // its TranslateTransform.
                DoubleAnimationUsingPath translateXAnimation = new DoubleAnimationUsingPath();
                translateXAnimation.PathGeometry = animationPath;
                translateXAnimation.Duration = TimeSpan.FromSeconds(2);

                // Set the Source property to X. This makes
                // the animation generate horizontal offset values from
                // the path information.
                translateXAnimation.Source = PathAnimationSource.X;

                // Set the animation to target the X property
                // of the TranslateTransform named "AnimatedTranslateTransform".
                Storyboard.SetTargetName(translateXAnimation, "AnimatedTranslateTransform");
                Storyboard.SetTargetProperty(translateXAnimation, new PropertyPath(TranslateTransform.XProperty));

                // Create a DoubleAnimationUsingPath to move the
                // rectangle vertically along the path by animating
                // its TranslateTransform.
                DoubleAnimationUsingPath translateYAnimation = new DoubleAnimationUsingPath();
                translateYAnimation.PathGeometry = animationPath;
                translateYAnimation.Duration = TimeSpan.FromSeconds(2);

                // Set the Source property to Y. This makes
                // the animation generate vertical offset values from
                // the path information.
                translateYAnimation.Source = PathAnimationSource.Y;

                // Set the animation to target the Y property
                // of the TranslateTransform named "AnimatedTranslateTransform".
                Storyboard.SetTargetName(translateYAnimation, "AnimatedTranslateTransform");
                Storyboard.SetTargetProperty(translateYAnimation, new PropertyPath(TranslateTransform.YProperty));

                // Create a Storyboard to contain and apply the animations.
                Storyboard pathAnimationStoryboard = new Storyboard();
                pathAnimationStoryboard.Children.Add(translateXAnimation);
                pathAnimationStoryboard.Children.Add(translateYAnimation);

                // Start the storyboard.
                pathAnimationStoryboard.Completed += PathAnimationStoryboard_Completed;
                pathAnimationStoryboard.Begin(this);
        }

        private void PathAnimationStoryboard_Completed(object? sender, EventArgs e)
        {
            _isAnimated = false;
            FrogCube.Source = new BitmapImage(new Uri("/Images/Frog.png", UriKind.Relative));
        }

        private void StartOver()
        {

            //cube 
            FrogCube.RenderTransform = null;
            PathAnimationStoryboard_Completed(null,new EventArgs());
            Canvas.SetLeft(FrogCube, 45);
            Canvas.SetTop(FrogCube, 351);
            _endPoint.Y = 0;

            //platform
            Canvas.SetLeft(lilyplatform, 107);
            Canvas.SetTop(lilyplatform, 396);

            //score 
            Score.Text = $"Score: {score=0}";
            Tries.Text = $"Tries: {tries=0}";

            
        }

        private void ResetGame(object sender, RoutedEventArgs e)
        {
            StartOver();
        }
    }
}
