using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class UserAnswer
    {
        public int UserAnswerId { get; set; }
        public int UserExamId { get; set; }
        public int QuestionId { get; set; }
        public int ChosenOptionId { get; set; }
        public string Status { get; set; }
    }
}
