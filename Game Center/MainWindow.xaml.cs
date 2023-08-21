using Game_Center.Projects.CurrencyConverter;
using Game_Center.Projects.Project_1;
using Game_Center.Projects.SimonSays;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game_Center
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            Image image = (sender as Image)!;// ! gets rid of "nullable type" warning
            image.Opacity= 0.6;
            GameText.Content = (image.Name) switch
            {
                "Image1" => "a User Management System",
                "Image2" => "To Do List",
                "Image3" => "Currency Exchange Rate",
                "Image4" => "Simon Says",
                "Image5" => "Game No. 5 is a game about lorm ipsum & happy birthday",
                "Image6" => "Game No. 6 is a game about lorm ipsum & happy birthday",
                _ => "please pick a game"
            };
        }

        private void OnMouseExit(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 1;
        }

        private void OnImage1Click(object sender, MouseButtonEventArgs e)
        {
            Window1 window1 = new();
            Hide();
            window1.ShowDialog();
            Show();
        }

        private void OnImage2MouseClick(object sender, MouseButtonEventArgs e)
        {
            ToDoList todoListProject = new();
            Hide();
            todoListProject.ShowDialog();
            Show();
        }

        private void OnImage3Click(object sender, MouseButtonEventArgs e)
        {
            CurrencyConverterView currencyConverter = new();
            Hide();
            currencyConverter.ShowDialog();
            Show();
        }

        private void OnImage4Click(object sender, MouseButtonEventArgs e)
        {
            Simon simon = new Simon();
            Hide();
            simon.ShowDialog();
            Show();
        }
    }
}
