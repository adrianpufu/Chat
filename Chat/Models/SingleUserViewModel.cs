using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Models
{
    public class SingleUserViewModel
    {
        public User User { get; set; }

        public ConversationRoom Room { get; set; }
    }
}