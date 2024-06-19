using BusinessObject.Model;
using DataAccess;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repository.Repository
{
    public class UserRepository : UserIRepository
    {
        private readonly UserDAO _userDao;

        public UserRepository(UserDAO userDao)
        {
            _userDao = userDao;
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userDao.GetUserById(userId);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userDao.GetAllUsers();
        }

        public async Task AddUser(User user)
        {
            await _userDao.AddUser(user);
        }

        public async Task UpdateUser(User user)
        {
            await _userDao.UpdateUser(user);
        }

        public async Task DeleteUser(int userId)
        {
            await _userDao.DeleteUser(userId);
        }
    }

}
