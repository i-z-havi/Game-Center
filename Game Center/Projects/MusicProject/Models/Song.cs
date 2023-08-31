using Game_Center.Projects.ToDoList.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Center.Projects.MusicProject.Models
{
    internal class Song
    {
        public string Name { get; set; }
        public ObservableCollection<Notes> songNotes { get; set; }

        public Song(string name = "")
        {
            Name = name;
            songNotes = new ObservableCollection<Notes>();
        }

        public void AddNote()
        {
            songNotes.Add(new Notes(songNotes.Count));
        }

        public void UpdateNote(int note, int id) 
        {
            Notes noteToUpdate = songNotes.FirstOrDefault(x => x.Id.Equals(id));
            if (noteToUpdate != null)
            {
                noteToUpdate.Note = note;
            }
            else
            {
                throw new Exception("note with this id was not found");
            }
        }
    }
}
