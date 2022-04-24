using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DashCode.Models.DataBase
{
    public class MsgStream
    {
        [Key]
        public int StreamID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MsgList> Messages { get; set; }
        public virtual ICollection<StreamUserList> Users { get; set; }
    }
}