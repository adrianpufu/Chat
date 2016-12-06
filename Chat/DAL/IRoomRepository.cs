using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chat.Models;

namespace Chat.DAL
{
    public interface IRoomRepository : IDisposable
    {
        IEnumerable<ConversationRoom> GetRooms();
        ConversationRoom GetRoomByName(string roomName);
        //void InsertRoom(ConversationRoom room);
        //void DeleteRoom(string roomName);
        //void UpdateRoom(ConversationRoom room);
        //void Save();
    }
}