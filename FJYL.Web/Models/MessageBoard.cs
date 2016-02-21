using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FJYL.Web.Models
{
    public class MessageBoard
    {
        public string Name { get; set; }
        public string Homepage { get; set; }
        public string QQ { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}