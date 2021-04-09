using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fond.DbFolder
{
    public class ProjectImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }




        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
