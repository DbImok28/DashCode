using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DashCode.Models.DataBase
{
    public class MsgList
    {
        [Key]
        public int StreamID { get; set; }
        [Key]
        public int UserID { get; set; }
        public string MessageText { get; set; }
        public DateTime SendDate { get; set; }
        public virtual MsgStream Stream { get; set; }
    }
}