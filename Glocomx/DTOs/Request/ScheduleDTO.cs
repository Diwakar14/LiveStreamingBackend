using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Glocomx.DTOs.Request
{
    public class ScheduleDTO
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Thumbnail { get; set; }

        [Required]
        public string HostId { get; set; }

    }
}
