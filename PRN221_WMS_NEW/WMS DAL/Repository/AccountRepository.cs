using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;
using WMS_DAL.DAO;
using WMS_DAL.Interface;

namespace WMS_DAL.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public Account GetAccountById(int id) => AccountDAO.Instance.GetAccountByID(id);

        public List<Account> GetAccounts() => AccountDAO.Instance.GetAccounts();

        public bool AddAccount(Account account) => AccountDAO.Instance.AddAccount(account);

        public bool UpdateAccount(Account account) => AccountDAO.Instance.UpdateAccount(account);

        public bool ChangeStatus(int id) => AccountDAO.Instance.SwitchAccountStatus(id);
    }
}
