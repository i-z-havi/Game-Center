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

        public void AddNote(Notes note)
        {
            songNotes.Add(note);
        }

        public void UpdateNote(Notes note) 
        {
            
        }
    }
}
