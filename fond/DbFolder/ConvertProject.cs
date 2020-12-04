using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fond.DbFolder
{
    public class ConvertProject
    {
        public int Id { get; set; }


        public int ConvertId { get; set; }
        public Convert Convert { get; set; }


        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
