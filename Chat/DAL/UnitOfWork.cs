using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chat.Models;

namespace Chat.DAL
{
    public class UnitOfWork : IDisposable
    {
        private UserContext context = new UserContext();
        private GenericRepository<ConversationRoom> roomRepository;
        private GenericRepository<User> userRepository;

        public GenericRepository<ConversationRoom> RoomRepository
        {
            get
            {

                if (this.roomRepository == null)
                {
                    this.roomRepository = new RoomRepository(context);
                }
                return roomRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

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