using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DashCode.Models.DataBase
{
    public partial class Users
    {
        public Users()
        {
            ChatMessages = new HashSet<ChatMessages>();
            ChatUser = new HashSet<ChatUser>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public byte[] Photo { get; set; }

        public virtual ICollection<ChatMessages> ChatMessages { get; set; }
        public virtual ICollection<ChatUser> ChatUser { get; set; }
    }
}
