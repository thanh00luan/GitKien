using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Account
    {
        public int AccountID { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string IsVerify { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}
