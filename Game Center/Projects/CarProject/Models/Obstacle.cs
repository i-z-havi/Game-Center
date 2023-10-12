using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game_Center.Projects.CarProject.Models
{
    internal class Obstacle : GameObject
    {
        public int Direction { get; set; }
        public Obstacle(int x, int y, int spd, Image img) : base(x, y, spd, img)
        {
            Direction = new Random().Next(-1, 2);
            Hitbox.Width = 50;
            Hitbox.Height = 50;
        }

        public override void Move()
        {
            Y += Speed;
            X += Speed * Direction;
            Draw();
        }
    }
}
