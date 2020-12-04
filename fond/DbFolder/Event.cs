using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fond.DbFolder
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
        public string VideoUrl { get; set; }

        public bool? CMI { get; set; }


        public int? ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
