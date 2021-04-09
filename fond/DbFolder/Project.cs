using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace fond.DbFolder
{
    public class Project
    {
        public int Id { get; set; }

        [DisplayName("Жоба аты")]
        public string ProjectName { get; set; }

        [DisplayName("Бастапқы сурет")]
        public string Photo { get; set; }

        [DisplayName("Жоба видео")]
        public string VideoUrl { get; set; }

        [DisplayName("Бастапқы ақпарат")]
        public string Title { get; set; }

        [DisplayName("Қосымша толық ақпарат")]
        public string Information { get; set; }
    }
}
