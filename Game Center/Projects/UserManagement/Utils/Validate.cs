using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game_Center.Projects.UserManagement.Utils
{
    internal class Validate
    {
        public static bool UserName(TextBox box)
        {
            Regex regex = new(@"^[A-Za-z].{2,20}");//@"" allows us to use characters within string, NOT as string. ^ means must include, [] has allowed range, {} includes min/max amount of chars
            Match match = regex.Match(box.Text);
            if (!match.Success)
            {
                MessageBox.Show("please enter at least 3 characters");
                box.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            box.BorderBrush = new SolidColorBrush(Colors.Black);
            return true;
        }

        public static bool Email(TextBox box) 
        {
            Regex regex = new("^\\S+@\\S+\\.\\S+$");
            Match match = regex.Match(box.Text);
            if (!match.Success) 
            {
                MessageBox.Show("please enter a valid email");
                box.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            box.BorderBrush = new SolidColorBrush(Colors.Black);
            return true;
        }
         
    }
}
