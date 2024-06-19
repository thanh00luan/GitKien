using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class ExamUserDTO
    {
        public int UserExamId { get; set; }
        public int UserId { get; set; }
        public int ExamId { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Score { get; set; }
        public string Status { get; set; }
    }
}
