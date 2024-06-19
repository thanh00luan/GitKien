using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
   
    public interface UserIRepository
    {
        Task<User> GetUserById(int userId);
        Task<List<User>> GetAllUsers();
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int userId);
    }
}
