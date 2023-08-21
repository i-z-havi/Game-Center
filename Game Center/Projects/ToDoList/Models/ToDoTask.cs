using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Center.Projects.ToDoList.Models
{
    internal class ToDoTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsComplete { get; set; }

        public ToDoTask(int id, string descript, bool isComp=false) 
        {
            Id = id;
            Description = descript;
            CreatedDate = DateTime.Now;
            IsComplete = isComp; // allows more flexibility in creating tasks
        
        }
    }
}
