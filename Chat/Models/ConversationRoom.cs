using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chat.Models
{
    public class ConversationRoom
    {
        [Key]
        public string RoomName { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}