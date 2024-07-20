using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WMS_DAL.Interface;

namespace WMS_WEB.Pages.Admin.Account
{
    public class UpdateStatusModel : PageModel
    {   
        private readonly IAccountRepository _accountRepository;

        public UpdateStatusModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult OnGet(int id)
        {
            if(id <= 0)
            {
                return RedirectToPage("/Error");
            }

            bool remove = _accountRepository.ChangeStatus(id);
            if(remove)
            {
                return RedirectToPage("/Admin/Account/Edit", new { id = id });
            }
            else
            {
                return RedirectToPage("/Error");
            }

        }
    }
}
