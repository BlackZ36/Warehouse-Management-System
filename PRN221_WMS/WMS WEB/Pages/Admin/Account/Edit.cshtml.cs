using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WMS_BLL.Models;
using WMS_DAL.Interface;

namespace WMS_WEB.Pages.Admin.Account
{
    public class EditModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        [BindProperty]
        public WMS_BLL.Models.Account Account { get; set; } = default!;

        public EditModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult OnGet(int id)
        {



            if (id <= 0)
            {
                return NotFound();
            }

            Account = _accountRepository.GetAccountById(id);
            if (Account == null)
            {
                return NotFound();
            }


            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {

            // Kiểm tra dữ liệu đầu vào
            if (Account == null || Account.AccountId <= 0)
            {
                ViewData["notification"] = "Invalid account data.";
                return Page();
            }

            bool result = _accountRepository.UpdateAccount(Account);
            if (result == false)
            {
                ViewData["notification"] = "Account is existed";
                return Page();
            }
            else
            {
                ViewData["notification"] = "Account Updated!";
            }
             return RedirectToPage("/Admin/Account/Edit", new { id = Account.AccountId });
        }


    }
}
