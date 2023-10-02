using Game_Center.Projects.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Game_Center.Projects.UserManagement.Utils
{
    class DataHandler
    {
        static readonly string directory = Directory.GetParent(Environment.CurrentDirectory)!.ToString();
        static readonly string path = directory + @"../../../Projects/UserManagement/Data/users.json";

        public static ObservableCollection<User> GetUserList()
        {
            string jsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<ObservableCollection<User>>(jsonString)!;
        }

        public static ObservableCollection<User> UpdateList(ObservableCollection<User> userlist)
        {
            var serialized =  JsonSerializer.Serialize(userlist);
            File.WriteAllText(path, serialized);
            return userlist;
        }
    }
}
