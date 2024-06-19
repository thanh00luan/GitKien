using BusinessObject.DTO;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AccountDAO
    {
        private readonly DBContext _context;

        public AccountDAO(DBContext context)
        {
            _context = context;
        }

        public async Task<Account> GetAccountById(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }
        public async Task<bool> Login(string username, string password)
        {
            var check =await _context.Accounts.Where(a => a.UserName == username && a.PassWord == password).SingleOrDefaultAsync();
            if(check != null)
            {
                return true;
            }
            return false;
        }
        public async Task AddAccount(AccountDTO accountDto)
        {
            var random = new Random();

            var user = new User
            {
                FullName = "User" + random.Next(1000), 
                Address = "Address" + random.Next(1000), 
                PhoneNumber = "PhoneNumber" + random.Next(1000), 
                Status = "Active" 
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(); 
            var userId = user.UserId;

            var account = new Account
            {
                UserId = userId,
                UserName = accountDto.UserName,
                PassWord = accountDto.PassWord,
                IsVerify = string.IsNullOrEmpty(accountDto.IsVerify) ? "True" : accountDto.IsVerify,
                Role = string.IsNullOrEmpty(accountDto.Role) ? "1" : accountDto.Role,
                Status = string.IsNullOrEmpty(accountDto.Status) ? "Active" : accountDto.Status
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAccount(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccount(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }
    }
}
