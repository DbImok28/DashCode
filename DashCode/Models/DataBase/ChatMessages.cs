using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DashCode.Models.DataBase
{
    public partial class ChatMessages
    {
        public int ChatMessagesId { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string MessageText { get; set; }
        public DateTime SendDate { get; set; }

        public virtual Chats Chat { get; set; }
        public virtual Users User { get; set; }
    }
}
