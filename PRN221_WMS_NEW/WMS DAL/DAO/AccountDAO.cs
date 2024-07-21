using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;

        public AccountDAO() { }

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public List<Account> GetAccounts()
        {
            List<Account> account;
            try
            {
                var context = new PRN221_WMSContext();
                account = context.Accounts
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public Account GetAccountByID(int id)
        {
            Account account = null;
            try
            {
                var db = new PRN221_WMSContext();
                account = db.Accounts.SingleOrDefault(u => u.AccountId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public bool AddAccount(Account account)
        {
            try
            {
                bool existingAccount = GetAccounts()
                    .Any(a => a.AccountCode.ToLower().Equals(account.AccountCode.ToLower()) ||
                              a.Username.ToLower().Equals(account.Username.ToLower()));

                // Nếu tài khoản không tồn tại thì thêm mới
                if (existingAccount != true)
                {
                    account.Status = 1; // Thiết lập trạng thái mặc định cho tài khoản mới

                    using (var db = new PRN221_WMSContext())
                    {
                        //account.AccountId = 0;
                        // Thêm tài khoản mới vào cơ sở dữ liệu
                        db.Accounts.Add(account);
                        db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                    }
                    return true; // Thêm thành công
                }
                return false; // Tài khoản đã tồn tại
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và ném lỗi lên cho gọi hàm
                throw new Exception($"Error in Add Account: {ex.Message}", ex);
            }
        }

        public bool UpdateAccount(Account account)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var existing = db.Accounts.SingleOrDefault(x => x.Username == account.Username);
                    if (existing != null)
                    {
                        existing.Name = account.Name;
                        existing.Email = account.Email;
                        existing.Phone = account.Phone;
                        existing.Role = account.Role;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Account not found for updating.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAccount: {ex.Message}", ex);
                return false;
            }
        }

        public bool SwitchAccountStatus(int id)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    Account u = db.Accounts.SingleOrDefault(a => a.AccountId == id);

                    if (u != null)
                    {
                        u.Status = (u.Status == 0) ? 1 : 0;

                        db.Entry(u).State = EntityState.Modified;
                        db.SaveChanges();
                        Console.WriteLine("Account status updated successfully!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Account does not exist!");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SwitchAccountStatus: {ex.Message}");
                return false;
            }
        }

    }
}
