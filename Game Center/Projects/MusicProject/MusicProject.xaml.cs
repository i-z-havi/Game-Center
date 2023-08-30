using Game_Center.Projects.MusicProject.Models;
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
using System.Windows.Shapes;

namespace Game_Center.Projects.MusicProject
{
    /// <summary>
    /// Interaction logic for MusicProject.xaml
    /// </summary>
    public partial class MusicProject : Window
    {
        private Song _song;

        public MusicProject()
        {
            InitializeComponent();
            InitializeSong();
        }

        private void InitializeSong()
        {
            _song=new Song();
            playBtn.Content = "test two";
            _song.AddNote(new Notes());
            _song.AddNote(new Notes());
            _song.AddNote(new Notes());
            SongGUI.ItemsSource = _song.songNotes;
        }

        private void OnNoteClick(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
        }
    }
}
