using Game_Center.Projects.ToDoList.Models;
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

namespace Game_Center.Projects.ToDoList
{
    /// <summary>
    /// Interaction logic for ToDoList.xaml
    /// </summary>
    public partial class ToDoList : Window
    {
        private ToDoListModel _todolist;
        public ToDoList()
        {
            InitializeComponent();
            InitializeTasks();
        }

        private void InitializeTasks()
        {
            _todolist=new ToDoListModel();
            _todolist.AddTask(new ToDoTask(1, "Homework"));
            _todolist.AddTask(new ToDoTask(2, "Dishes"));
            listTasks.ItemsSource = _todolist.Tasks;

        }

        private void OnTaskToggled(object sender, RoutedEventArgs e)
        {
            if(sender is CheckBox checkBox && checkBox.DataContext is ToDoTask task)
            {
                _todolist.ToggleComplete(task.Id);
            }
        }

        private void OnTextBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                TextBlock textBlock = sender as TextBlock;
                StackPanel parent = textBlock.Parent as StackPanel;
                TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
                Button btnSave = parent.FindName("btnSave") as Button;
                textBlock.Visibility = Visibility.Collapsed;
                editTextBox.Visibility = Visibility.Visible;
                editTextBox.Text = textBlock.Text;
                btnSave.Visibility = Visibility.Visible;
            }
        }

        private void OnSaveEdit(object sender, RoutedEventArgs e)
        {
            Button btnSave = sender as Button;
            StackPanel parent = btnSave.Parent as StackPanel;
            TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
            TextBlock textBlock = parent.FindName("txtTaskDescription") as TextBlock;
            btnSave.Visibility = Visibility.Collapsed;
            editTextBox.Visibility = Visibility.Collapsed;
            textBlock.Visibility = Visibility.Visible;
            textBlock.Text=editTextBox.Text;
            if(textBlock.DataContext is ToDoTask task)
            {
                _todolist.UpdateTask(task.Id, textBlock.Text);
            }
        }

        private void OnAddTask(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewTask.Text))
            {
                _todolist.AddTask(new ToDoTask(_todolist.Tasks.Count, txtNewTask.Text));
                txtNewTask.Clear();
            }
        }
    }
}
