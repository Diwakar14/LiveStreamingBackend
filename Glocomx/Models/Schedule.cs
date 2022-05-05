using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glocomx.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public ICollection<Tags> Tags { get; set; }
        public string Thumbnail { get; set; }

        public string HostId { get; set; }

        public Schedule()
        {
            this.Tags = new List<Tags>();
        }
    }
}
