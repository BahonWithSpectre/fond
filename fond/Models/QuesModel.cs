using fond.DbFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fond.Models
{
    public class QuesModel
    {
        public IEnumerable<QuestionType> QuestionTypes { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}
