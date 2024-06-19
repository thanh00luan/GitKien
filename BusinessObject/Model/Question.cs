using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int ExamId { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}