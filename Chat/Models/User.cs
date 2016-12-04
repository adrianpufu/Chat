using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chat.Models
{
    public class User
    {
        [Key]
        public string UserName { get; set; }

        public ICollection<Connection> Connections { get; set; }

        public virtual ICollection<ConversationRoom> Rooms { get; set; }
    }
}