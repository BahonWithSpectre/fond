using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fond.DbFolder
{
    public class EventImage
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }



        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
