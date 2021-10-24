using System;
using System.Collections.Generic;
using System.Linq;
using MovieList.Models;

namespace MovieList.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password){
            var user = new List<User>{
                new User{Id = 1, Username = "Saulo", Password = "1234", Role = "manager"},
                new User{Id = 2, Username = "Godoy", Password = "1234", Role = "employee"}
            };
            return user.FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase) && x.Password == password);
        }
    }
}