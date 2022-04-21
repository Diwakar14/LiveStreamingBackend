using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glocomx.DTOs.Request
{
    public class ChatDTO
    {
        public string StreamId { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
    }
}
