using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WMS_BLL.Models;
using WMS_DAL.Interface;

namespace WMS_WEB.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public Account Account { get; set; } = default!;

        public IndexModel(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string username, string password)
        {
            var account = _accountRepository.GetAccounts().FirstOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));

            if (account == null)
            {
                ViewData["error"] = "Username/Password Incorrect";
                return Page();
            }

            if (account.Status == 0)
            {
                ViewData["error"] = "You Account Is Suspended";
                return Page();
            }
            else if (account.Role == 0 && account.Status == 1)
            {
                HttpContext.Session.SetString("account", "admin");
                HttpContext.Session.SetInt32("accountId", account.AccountId);
                HttpContext.Session.SetString("accountName", account.Name);
                return RedirectToPage("/Admin/Dashboard");
            }
            else if (account.Role == 2 && account.Status == 1)
            {
                HttpContext.Session.SetString("account", "manager");
                HttpContext.Session.SetInt32("accountId", account.AccountId);
                HttpContext.Session.SetString("accountName", account.Name);
                return RedirectToPage("/Manager/Dashboard");
            }
            else if (account.Role == 1 && account.Status == 1)
            {
                HttpContext.Session.SetString("account", "storekeeper");
                HttpContext.Session.SetInt32("accountId", account.AccountId);
                HttpContext.Session.SetString("accountName", account.Name);
                return RedirectToPage("/Storekeeper/Dashboard");
            }
            return Page();

        }

    }
}
