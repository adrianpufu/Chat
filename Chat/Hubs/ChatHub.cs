using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Chat.DAL;
using Chat.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Chat.Hubs
{
    //[Authorize]
    public class ChatHub : Hub
    {
        public void sendToAll(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public void sendMessageToRoom(string roomName, string message)
        {
            SaveMessageToDatabase(Context.User.Identity.Name, message, roomName);

            Clients.Group(roomName).addNewMessageToPage(Context.User.Identity.Name, message);
        }

        private void SaveMessageToDatabase(string author, string content, string roomName)
        {
            using (var db = new UserContext())
            {
                var room = db.Rooms.Where(u => u.RoomName == roomName).FirstOrDefault();

                if(room != null)
                {
                    var message = new Message()
                    {
                        Author = author,
                        Content = content
                    };
                    room.Messages.Add(message);
                    db.Entry(room).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public void sendMessageTo(string who, string message)
        {
            var name = Context.User.Identity.Name;
            using (var db = new UserContext())
            {
                var user = db.Users.Find(who);
                if (user == null)
                {
                    Clients.Caller.showErrorMessage("Could not find that user.");
                }
                else
                {
                    db.Entry(user)
                        .Collection(u => u.Connections)
                        .Query()
                        .Where(c => c.Connected == true)
                        .Load();

                    if (user.Connections == null)
                    {
                        Clients.Caller.showErrorMessage("The user is no longer connected.");
                    }
                    else
                    {
                        var roomName = db.Rooms.Where(x => ((x.Users.Where(y => y.UserName == user.UserName).FirstOrDefault() != null) && (x.Users.Where(y => y.UserName == name).FirstOrDefault() != null))).FirstOrDefault().RoomName;

                        SaveMessageToDatabase(Context.User.Identity.Name, message, roomName);

                        foreach (var connection in user.Connections)
                        {
                            Clients.Client(connection.ConnectionID)
                                .addNewMessageToPage(name + ": " + message);
                        }
                    }
                }
            }
        }
        public override Task OnConnected()
        {
            using (var db = new UserContext())
            {
                var name = Context.User.Identity.Name;

                var user = db.Users
                    .Include(u => u.Rooms)
                    .FirstOrDefault(u => u.UserName == name);

                if (user == null)
                {
                    user = new User()
                    {
                        UserName = name,
                        Connections = new List<Connection>()
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                    //conexiune noua
                    user.Connections.Add(new Connection()
                    {
                        ConnectionID = Context.ConnectionId,
                        UserAgent = Context.Request.Headers["User-Agent"],
                        Connected = true
                    });
                    //room cu el insusi
                    var room = db.Rooms.Where(u => u.RoomName == name).FirstOrDefault();

                    if (room == null)
                    {
                        room = new ConversationRoom()
                        {
                            RoomName = name,
                            Users = new List<User>()
                        };
                        room.Users.Add(user);
                        db.Rooms.Add(room);
                        db.SaveChanges();
                    }

                    Groups.Add(Context.ConnectionId, Context.User.Identity.Name);
                }
                else
                {
                    foreach (var item in user.Rooms)
                    {
                        Groups.Add(Context.ConnectionId, item.RoomName);
                    }
                }
                
                
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            using (var db = new UserContext())
            {
                var connection = db.Connections.Find(Context.ConnectionId);
                connection.Connected = false;
                db.SaveChanges();
            }
            return base.OnDisconnected(stopCalled);
        }

        public void addToRoom(string roomName)
        {
            using (var db = new UserContext())
            {
                var room = db.Rooms.Find(roomName);

                if (room != null)
                {
                    var user = new User() { UserName = Context.User.Identity.Name };
                    db.Users.Attach(user);

                    room.Users.Add(user);
                    db.SaveChanges();
                    Groups.Add(Context.ConnectionId, roomName);
                }
            }
            Clients.Caller.addNewMessageToPage("connected", "sucessfully");
            Clients.Group(roomName).addNewMessageToPage(Context.User.Identity.Name + " joined " + roomName + ".");
        }

        public void leaveRoom(string roomName)
        {
            using (var db = new UserContext())
            {
                var room = db.Rooms.Find(roomName);
                if (room != null)
                {
                    var user = new User() { UserName = Context.User.Identity.Name };
                    db.Users.Attach(user);

                    room.Users.Remove(user);
                    db.SaveChanges();

                    Groups.Remove(Context.ConnectionId, roomName);
                }
            }
        }
    }
}
