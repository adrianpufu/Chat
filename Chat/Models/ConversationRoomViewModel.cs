using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Models
{
    public class ConversationRoomViewModel
    {
        public IEnumerable<ConversationRoom> ConversationRooms { get; set; }
    }
}