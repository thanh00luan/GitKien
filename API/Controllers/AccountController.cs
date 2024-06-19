using BusinessObject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Đảm bảo thêm using directive này
using System;
using System.Linq;
using DataAccess;
using System.Security.Principal;
using BusinessObject.DTO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountIRepository _accountRepository;
        private readonly DBContext _dBContext;

        public AccountController(AccountIRepository accountRepository, DBContext dBContext)
        {
            _accountRepository = accountRepository;
            _dBContext = dBContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountById(int id)
        {
            var account = await _accountRepository.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAllAccounts()
        {
            var accounts = await _accountRepository.GetAllAccounts();
            return Ok(accounts);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var check = await _accountRepository.Login(username, password);
            if (check)
            {
                var acc = await _dBContext.Accounts.Where(a => a.UserName == username).FirstOrDefaultAsync();
                if (acc != null)
                {
                    var us = await _dBContext.Users.Where(a => a.UserId == acc.UserId).FirstOrDefaultAsync();
                    if (us != null)
                    {
                        LoginModel ls = new LoginModel();
                        ls.UserId = acc.UserId;
                        ls.fullname = us.FullName;
                        ls.UserName = acc.UserName;
                        ls.Role = acc.Role;
                        ls.Status = acc.Status;
                        ls.IsVerify = acc.IsVerify;

                        return Ok(ls);
                    }

                }

            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<ActionResult<Account>> AddAccount(AccountDTO account)
        {
            await _accountRepository.AddAccount(account);
            return Ok();
        }
    }
    public class LoginModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string IsVerify { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string fullname { get; set; }
    }
}
