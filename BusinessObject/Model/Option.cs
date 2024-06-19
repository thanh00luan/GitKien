using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Option
    {
        public int OptionId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public string Status { get; set; }
    }
}
