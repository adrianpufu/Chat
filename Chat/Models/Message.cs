﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }
    }
}