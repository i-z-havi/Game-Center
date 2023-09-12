using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Game_Center.Projects.LeapFrogProject.Models
{
    class OuterBounds
    {
        public static Rect CreateRect(int left)
        {
            return new Rect(left, 0, 20, 400);
        }

        public static Image OuterBoundsRect(Rect r)
        {
            Image sprite = new Image();
            sprite.Width = r.Width;
            sprite.Height = r.Height;
            Canvas.SetLeft(sprite, r.Left);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("/Images/Spikes.png", UriKind.Relative);
            if (r.Left != 0)
            {
                bi.Rotation = Rotation.Rotate180;
            }
            bi.EndInit();
            sprite.Source = bi;
            return sprite;
        }
    }
}
