﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Center.Projects.ToDoList.Models
{
    internal class ToDoListModel
    {
        public ObservableCollection<ToDoTask> Tasks { get; set; }

        public ToDoListModel() 
        {
            Tasks= new ObservableCollection<ToDoTask>();
        }

        public void AddTask(ToDoTask task)
        {
            Tasks.Add(task);
        }

        public void UpdateTask(int taskId, string newDescription)
        {
            ToDoTask task = Tasks.FirstOrDefault(x => x.Id.Equals(taskId));
            if (task!=null)
            {
                task.Description = newDescription;
            }
            else
            {
                throw new Exception("task with this id was not found");
            }
        }

        public void ToggleComplete(int taskId)
        {
            ToDoTask task = Tasks.FirstOrDefault(x => x.Id.Equals(taskId));
            if (task!=null) 
            {
                task.IsComplete = !task.IsComplete;
            }
            else
            {
                throw new Exception("task with this id was not found");
            }
            
        }
    }
}
