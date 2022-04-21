using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glocomx.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string StreamId { get; set; }
        public string Message { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        
    }
}
