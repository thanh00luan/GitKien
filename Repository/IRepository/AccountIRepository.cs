using BusinessObject.DTO;
using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface AccountIRepository
    {
        Task<Account> GetAccountById(int accountId);
        Task<List<Account>> GetAllAccounts();
        Task AddAccount(AccountDTO account);
        Task<bool> Login(string username, string password);
        Task UpdateAccount(Account account);
        Task DeleteAccount(int accountId);
    }
}
