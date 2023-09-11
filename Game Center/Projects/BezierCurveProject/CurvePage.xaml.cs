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

namespace Game_Center.Projects.BezierCurveProject
{
    /// <summary>
    /// Interaction logic for CurvePage.xaml
    /// </summary>
    // TODO: 
    //  -maybe build around having window open all the way?
    public partial class CurvePage : Window
    {
        private bool _vectorStarted = false;
        private SolidColorBrush _colorBrush = new SolidColorBrush(Colors.White);
        private Line _line = new Line();
        private Point _referencePoint = new Point();
        private Point _clickedPoint = new Point();
        private Vector _vector = new Vector();
        public CurvePage()
        {

            InitializeComponent();
            _line.Stroke = _colorBrush;
            WindowCanvas.Children.Add(_line );            
        }

        private void StartCubeClicked(object sender, MouseButtonEventArgs e)
        {
            //rectangle only had click work when the actual rectangle was clicked! image works much better for clicking in a specific area
            _vectorStarted = true;
            _clickedPoint = e.GetPosition(this);
        }

        private void VectorEnded(object sender, MouseButtonEventArgs e)
        {
            _vectorStarted=false;
            _line.Stroke = Brushes.Black;
            if (e.GetPosition(this).X < _clickedPoint.X && e.GetPosition(this).Y > _clickedPoint.Y)
            {
                _vector.X=_clickedPoint.X-e.GetPosition(this).X;
                _vector.Y=_clickedPoint.Y-e.GetPosition(this).Y;
                _vector *= 8;
                Path path = new Path();
                path.Stroke= _colorBrush;
                path.StrokeThickness = 3;

                PathGeometry pGeometry = new();

                PathFigure pFigure = new();
                pFigure.StartPoint = _clickedPoint;

                Point controlPoint = new Point();
                controlPoint = Point.Add(_clickedPoint,_vector);

                Point endPoint = new Point(controlPoint.X+_vector.X, _clickedPoint.Y);
                QuadraticBezierSegment qBezierSwgment = new QuadraticBezierSegment(controlPoint, endPoint, true);
                pFigure.Segments.Add(qBezierSwgment);
                pGeometry.Figures.Add(pFigure);
                path.Data = pGeometry;
                WindowCanvas.Children.Add(path);
            }

        }

        private void DrawVector(object sender, MouseEventArgs e)
        {
            if (_vectorStarted)
            {
                _line.Stroke = Brushes.White;
                _referencePoint =e.GetPosition(this);
                _line.X1 = _clickedPoint.X;
                _line.Y1 = _clickedPoint.Y;
                _line.X2 = _referencePoint.X;
                _line.Y2 = _referencePoint.Y;
            }
        }

        private void ChangeColor(object sender, MouseButtonEventArgs e)
        {
            Rectangle colorRect = sender as Rectangle;
            if(colorRect != null )
            {
                _colorBrush = colorRect.Stroke as SolidColorBrush;
            }
        }

        private void EraseLines(object sender, RoutedEventArgs e)
        {
            Type t = typeof(Path);
            for (int i = WindowCanvas.Children.Count - 1; i >= 0; i--)
            {
                if (WindowCanvas.Children[i].GetType() == t)
                {
                    WindowCanvas.Children.RemoveAt(i);
                }          
            }
        }
    }
}
