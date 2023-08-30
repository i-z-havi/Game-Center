using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Center.Projects.MusicProject.Models
{
    internal class Notes
    {
        public int Note { get; set; }
        public DateTime Id { get; set; }
        public Notes() 
        {
            Note = -1;
            Id = DateTime.Now;
        }
    }
}
