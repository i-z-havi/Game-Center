using Game_Center.Projects.MusicProject.Models;
using Game_Center.Projects.ToDoList.Models;
using Game_Center.Projects.ToDoList;
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
using System.IO;
using System.Text.Json;
using System.Collections.ObjectModel;

namespace Game_Center.Projects.MusicProject
{
    /// <summary>
    /// Interaction logic for MusicProject.xaml
    /// </summary>
    public partial class MusicProject : Window
    {
        private Song _song;
        private double _length=200;
        private ObservableCollection<string> _songList = new ObservableCollection<string>();
        private DirectoryInfo _di;

        public MusicProject()
        {
            InitializeComponent();
            InitializeSong();
        }

        private void InitializeSong()
        {
            _song=new Song();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            _song.AddNote();
            SongGUI.ItemsSource = _song.songNotes;
            _di = new DirectoryInfo("songsfolder");
            _di.Create();
            foreach (var item in _di.GetFiles())
            {
                _songList.Add(item.Name);
            }
            LoadSongMenu.ItemsSource= _songList;
        }

        private void PlaySong(object sender, RoutedEventArgs e)
        {
            Song playedSong = sender as Song;
            if (playedSong!=null)
            {
                foreach (Notes note in playedSong.songNotes)
                {
                    if (note.Note != 0)
                    {
                        Console.Beep(note.Note, 200);
                    }
                }
            }
            else
            {
                foreach (Notes note in _song.songNotes)
                {
                    if (note.Note != 0)
                    {
                        Console.Beep(note.Note, 200);
                    }
                }
            }

        }

        private void AddNotes(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 12; i++) 
            {
                _song.AddNote( );
            }
        }

        private void OnNoteClick(object sender, RoutedEventArgs e)
        {
            RadioButton noteToSend = sender as RadioButton;
            StackPanel parent = noteToSend.Parent as StackPanel;
            if (parent.DataContext is Notes noteToUpdate)
            {
                switch (noteToSend.Name)
                {
                    //A = 220,
                    //Asharp = 233,
                    //B = 247,
                    //C = 262,
                    //Csharp = 277,
                    //D = 294,
                    //Dsharp = 311,
                    //E = 330,
                    //F = 349,
                    //Fsharp = 370,
                    //G = 392,
                    //Gsharp = 415,
                    case "_11":
                        _song.UpdateNote(415, noteToUpdate.Id);
                        break;
                    case "_10":
                        _song.UpdateNote(392, noteToUpdate.Id);
                        break;
                    case "_9":
                        _song.UpdateNote(370, noteToUpdate.Id);
                        break;
                    case "_8":
                        _song.UpdateNote(349, noteToUpdate.Id);
                        break;
                    case "_7":
                        _song.UpdateNote(330, noteToUpdate.Id);
                        break;
                    case "_6":
                        _song.UpdateNote(311, noteToUpdate.Id);
                        break;
                    case "_5":
                        _song.UpdateNote(294, noteToUpdate.Id);
                        break;
                    case "_4":
                        _song.UpdateNote(277, noteToUpdate.Id);
                        break;
                    case "_3":
                        _song.UpdateNote(262, noteToUpdate.Id);
                        break;
                    case "_2":
                        _song.UpdateNote(247, noteToUpdate.Id);
                        break;
                    case "1":
                        _song.UpdateNote(233, noteToUpdate.Id);
                        break;
                    case "0":
                        _song.UpdateNote(220, noteToUpdate.Id);
                        break;
                    case "_N1":
                        _song.UpdateNote(0, noteToUpdate.Id);
                        break;
                }
            }
        }

        private void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            SongNamePopup.Visibility = Visibility.Visible;
            SongGUI.IsEnabled=false;
        }

        private void CancelSaveBtn(object sender, RoutedEventArgs e)
        {
            SongNameInput.Text = "";
            SongNamePopup.Visibility = Visibility.Collapsed;
            SongGUI.IsEnabled = true;
        }

        private void ConfirmSaveBtn(object sender, RoutedEventArgs e)
        {
            _song.Name = SongNameInput.Text;
            string fileName = $"{SongNameInput.Text}.txt";
            Directory.CreateDirectory("songsfolder");
            string songtoJson = JsonSerializer.Serialize(_song);
            File.WriteAllText(Path.Combine("songsfolder", fileName),songtoJson);
            _di = new DirectoryInfo("songsfolder");
            _songList.Clear();
            foreach (var item in _di.GetFiles())
            {
                _songList.Add(item.Name);
            }
            SongGUI.IsEnabled = true;
            SongNameInput.Text = "";
            SongNamePopup.Visibility= Visibility.Collapsed;
        }

        private void LoadSong(object sender, RoutedEventArgs e)
        {
            LoadSongPopup.Visibility = Visibility.Visible;
            SongGUI.IsEnabled = false;
        }

        private void CancelLoadBtn(object sender, RoutedEventArgs e)
        {
            LoadSongPopup.Visibility= Visibility.Collapsed;
            SongGUI.IsEnabled = true;
        }

        private void ConfirmLoadBtn(object sender, RoutedEventArgs e)
        {
            if (LoadSongMenu.SelectedItem != null)
            {
                Song loadedsong = JsonSerializer.Deserialize<Song>(File.ReadAllText(Path.Combine("songsfolder", LoadSongMenu.SelectedItem.ToString())));
                SongGUI.IsEnabled = false;
                LoadSongMenu.SelectedItem = null;
                PlaySong(loadedsong, e);

            }
           
        }
    }
}
