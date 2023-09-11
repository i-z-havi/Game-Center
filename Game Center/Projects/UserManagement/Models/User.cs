using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Center.Projects.UserManagement.Models
{
    public class User //will be used in all of the project, so its public
    {
        public static int Count=0;
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime timeCreated { get; set; }

        public User(string name, string email, string password)
        {
            User.Count++;
            Id = Count;
            Name = name;
            Email = email;
            Password = password;
            timeCreated = DateTime.Now;
        }
    }
}
