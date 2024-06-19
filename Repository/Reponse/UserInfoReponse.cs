using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Reponse
{
    public class UserInfoReponse
    {
        public int AccountID { get; set; }
        public string UserName { get; set; }
        public User User { get; set; }
    }
}
