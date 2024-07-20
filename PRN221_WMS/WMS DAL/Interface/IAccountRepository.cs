using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.Interface
{
    public interface IAccountRepository
    {
        Account GetAccountById(int id);
        List<Account> GetAccounts();
        bool AddAccount(Account account);
        bool UpdateAccount(Account account);
        bool ChangeStatus(int id);
    }
}
