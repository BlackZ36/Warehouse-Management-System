using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WMS_DAL.Interface;

namespace WMS_WEB.Pages.Admin.Account
{
    public class IndexModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public List<WMS_BLL.Models.Account> Accounts { get; set; }

        public IndexModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task OnGetAsync()
        {
            //lấy ra các danh sách cần thiết
            Accounts = _accountRepository.GetAccounts();
        }
    }
}
