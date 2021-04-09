using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fond.DbFolder
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionName { get; set; }

        public string Answer { get; set; }

        public int QuestionTypeId { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
