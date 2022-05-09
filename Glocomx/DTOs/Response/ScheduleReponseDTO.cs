using Glocomx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glocomx.DTOs.Response
{
    public class ScheduleReponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LiveSessionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public ICollection<Tags> Tags { get; set; }

        public UserResponseDTO Host { get; set; }
    }
}
