using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class UserExam
    {
        public int UserExamId { get; set; }
        public int UserId { get; set; }
        public int ExamId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Score { get; set; }
        public string Status { get; set; }
    }
}
