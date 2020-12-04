using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fond.DbFolder
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }

        public string VideoUrl { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
    }
}
