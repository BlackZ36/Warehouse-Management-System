using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WMS_DAL.Interface;

namespace WMS_WEB.Pages.Admin.Account
{
    public class AddModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        // Constructor duy nhất
        public AddModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public WMS_BLL.Models.Account Account { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {

            bool result = _accountRepository.AddAccount(Account);
            if (result == false)
            {
                ViewData["notification"] = "Account is existed";
                return Page();
            }
            else { }
            return RedirectToPage("/Admin/Account/Index");
        }
    }
}
