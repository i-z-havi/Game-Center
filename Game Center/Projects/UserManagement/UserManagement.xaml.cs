using Game_Center.Projects.UserManagement.Models;
using Game_Center.Projects.UserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<User> users = DataHandler.GetUserList();

        public UserManagement()
        {
            InitializeComponent();
            if (users == null||users.Count==0)
            {
                ObservableCollection<User> initialList = new()
                {
                    new User("Bob", "bob@email.com", "Qaz123!123Qaz"),
                    new User("Sara", "Sara@email.com", "Qaz123!123Qaz"),
                    new User("Neomi", "Neomi@email.com", "Qaz123!123Qaz"),
                    new User("Abed", "Abed@email.com", "Qaz123!123Qaz")
                };      
                users = DataHandler.UpdateList(initialList);
            }
            DataGrid1.ItemsSource = users;
        }

        private void UpdateJsonData()
        {
            ObservableCollection<User> tempList = users;
            users = DataHandler.UpdateList(tempList);
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            if (Validate.UserName(Input_User)&&Validate.Email(Input_Email))
            {
                users.Add(new User(Input_User.Text, Input_Email.Text, "Abc123!"));
                UpdateJsonData();
            }
        }

        private void RowSelected(object sender, SelectedCellsChangedEventArgs e)
        {
            if (DataGrid1.SelectedCells.Count != 0)
            {
                _selectedUser = DataGrid1.SelectedCells[0].Item as User;
                Input_User.Text = _selectedUser.Name;
                Input_Email.Text = _selectedUser.Email;
            }
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            users.Remove(_selectedUser);
            UpdateJsonData();
            DataGrid1.SelectedItem = null;
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            if (Input_User.Text=="admin"&&Input_Pass.Text=="abc123")
            {
                Mail_Panel.Visibility = Visibility.Visible;
                PostLogPanel.Visibility = Visibility.Visible;
                DataGrid1.Visibility = Visibility.Visible;
                Pass_Panel.Visibility= Visibility.Collapsed;
                LogInBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("account is not an admin!","Error",MessageBoxButton.OK);
            }
        }
    }
}
