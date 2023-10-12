using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Game_Center.Projects.CarProject.Models
{
    internal abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public Image Sprite { get; set; }
        public Rect Hitbox = new Rect(0,0,0,0);

        public GameObject(int x, int y, int speed, Image img) 
        {
            Hitbox.X = x;
            Hitbox.Y = y;
            X = x;
            Y = y;
            Speed=speed;
            Sprite = img;
        }
        public abstract void Move();

        public void Draw() 
        { 
            Sprite.Margin = new Thickness(X,Y,0,0);
            Hitbox.X = X;
            Hitbox.Y = Y;
        }
    }
}
