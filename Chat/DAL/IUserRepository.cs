using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chat.Models;

namespace Chat.DAL
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserByName(string userName);
        //void InsertUser(User user);
        //void DeleteUser(string userName);
        //void UpdateUser(User user);
        //void Save();
    }
}