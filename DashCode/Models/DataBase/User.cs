using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DashCode.Models.DataBase
{
    public class User
    {
        [Key]
        public int UsetID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public virtual ICollection<MsgStream> Streams { get; set; }
    }
}