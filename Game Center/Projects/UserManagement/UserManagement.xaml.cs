using Game_Center.Projects.UserManagement.Models;
using Game_Center.Projects.UserManagement.Utils;
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

namespace Game_Center.Projects.UserManagement
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        private User _selectedUser;

            List<User> users = new List<User>
            {
                new User("Bpb", "Bob@email.com", "Abc123!"),
                new User("John", "John@email.com", "Abc123!"),
                new User("Doe", "Doe@email.com", "Abc123!"),
                new User("Sil", "Sil@email.com", "Abc123!"),
            };
        public UserManagement()
        {
            InitializeComponent();
            UpdateDataGrid();
 
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            if (Validate.UserName(Input_User))
            {
                users.Add(new User(Input_User.Text, Input_Email.Text, "Abc123!"));
                UpdateDataGrid();
            }
        }

        private void UpdateDataGrid()
        {
            DataGrid1.ItemsSource = users.ToList();
        }

        private void RowSelected(object sender, SelectedCellsChangedEventArgs e)
        {
            if (DataGrid1.SelectedCells.Count!=0)
            {
                _selectedUser = DataGrid1.SelectedCells[0].Item as User;
                Input_Email.Text = _selectedUser.Email;
            }
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            users.Remove(_selectedUser);
            UpdateDataGrid();
        }

        private void RemovePasswords(object sender, RoutedEventArgs e)
        {
            DataGrid1.Columns[3].Visibility = Visibility.Hidden;
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            PassPanel.Visibility = Visibility.Visible;
        }
    }
}
