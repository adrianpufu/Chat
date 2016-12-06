using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chat.Models;
using System.Data.Entity;

namespace Chat.DAL
{
    public class UserRepository : GenericRepository<User>, IUserRepository, IDisposable
    {
        public UserRepository(UserContext context) : base(context)
        {
            this.context = context;
        }

        public User GetUserByName(string userName)
        {
            return context.Users.Find(userName);
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }

        //public void DeleteUser(string userName)
        //{
        //    User user = context.Users.Find(userName);
        //    context.Users.Remove(user);
        //}

        //public void InsertUser(User user)
        //{
        //    context.Users.Add(user);
        //}

        //public void Save()
        //{
        //    context.SaveChanges();
        //}

        //public void UpdateUser(User user)
        //{
        //    context.Entry(user).State = EntityState.Modified;
        //}

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}