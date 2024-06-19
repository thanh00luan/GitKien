using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.DAO;
using Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AccountRepository : AccountIRepository
    {
        private readonly AccountDAO _accountDAO;

        public AccountRepository(AccountDAO accountDAO)
        {
            _accountDAO = accountDAO;
        }

        public async Task<Account> GetAccountById(int accountId)
        {
            return await _accountDAO.GetAccountById(accountId);
        }
        public async Task<bool> Login(string user, string pass)
        {
            return await _accountDAO.Login(user, pass);
        }
        public async Task<List<Account>> GetAllAccounts()
        {
            return await _accountDAO.GetAllAccounts();
        }

        public async Task AddAccount(AccountDTO account)
        {
            await _accountDAO.AddAccount(account);
        }

        public async Task UpdateAccount(Account account)
        {
            await _accountDAO.UpdateAccount(account);
        }

        public async Task DeleteAccount(int accountId)
        {
            await _accountDAO.DeleteAccount(accountId);
        }
    }
}
