using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DashCode.Models.DataBase
{
    public partial class Chats
    {
        public Chats()
        {
            ChatMessages = new HashSet<ChatMessages>();
            ChatUser = new HashSet<ChatUser>();
        }

        public int ChatId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ChatMessages> ChatMessages { get; set; }
        public virtual ICollection<ChatUser> ChatUser { get; set; }
    }
}
