using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chat.Models;
using System.Data.Entity;

namespace Chat.DAL
{
    public class RoomRepository : GenericRepository<ConversationRoom>, IRoomRepository, IDisposable
    {

        public RoomRepository(UserContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<ConversationRoom> GetRooms()
        {
            return context.Rooms.ToList();
        }

        public ConversationRoom GetRoomByName(string roomName)
        {
            return context.Rooms.Find(roomName);
        }

        //public void InsertRoom(ConversationRoom room)
        //{
        //    context.Rooms.Add(room);
        //}

        //public void DeleteRoom(string roomName)
        //{
        //    ConversationRoom room = context.Rooms.Find(roomName);
        //    context.Rooms.Remove(room);
        //}

        //public void UpdateRoom(ConversationRoom room)
        //{
        //    context.Entry(room).State = EntityState.Modified;
        //}

        //public void Save()
        //{
        //    context.SaveChanges();
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