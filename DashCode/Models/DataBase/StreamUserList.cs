using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DashCode.Models.DataBase
{
    public class StreamUserList
    {
        [Key]
        public int StreamID { get; set; }
        [Key]
        public int UserID { get; set; }
    }
}