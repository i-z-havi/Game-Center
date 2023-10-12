using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Game_Center.Projects.CarProject.Models
{
    internal class PlayerCar:GameObject
    {
        public bool LeftKeyPressed { get; set; }
        public bool RightKeyPressed { get; set;}
        public PlayerCar(int x, int y, int spd, Image img) : base(x, y, spd, img)
        {
            Hitbox.Width = 60;
            Hitbox.Height = 100;
        }
        public override void Move()
        {
            if (LeftKeyPressed&&X>0)
            {
                X -= Speed;
            }
            if (RightKeyPressed&&X<730)
            {
                X += Speed;
            }
            Draw();
        }
    }
}
