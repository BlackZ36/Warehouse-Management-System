using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;
using WMS_DAL;
using WMS_DAL.Interface;

namespace WMS_DAL.Implement
{
    public class AccountRepository : IAccountRepository
    {
        private AccountDAO accountDAO;
        public AccountRepository()
        {
            accountDAO = new AccountDAO();
        }

        public bool AddAccount(Account account) => accountDAO.AddAccount(account);

        public bool BanAccount(int id) => accountDAO.BanAccount(id);

        public Account GetAccountByID(int id) => accountDAO.GetAccountByID(id);

        public List<Account> GetAccounts() => accountDAO.GetAccounts();

        public bool UpdateAccount(Account account) => accountDAO.UpdateAccount(account);
    }
}
