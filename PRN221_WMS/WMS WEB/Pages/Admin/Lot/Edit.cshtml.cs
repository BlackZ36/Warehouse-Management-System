using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMS_BLL.Models;
using WMS_DAL.Interface;

namespace WMS_WEB.Pages.Admin.Lot
{
    public class EditModel : PageModel
    {
        private readonly ILotRepository _lotRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IPartnerRepository _partnerRepositorye;

        public EditModel(ILotRepository lotRepository, IAccountRepository accountRepository, IPartnerRepository partnerRepositorye)
        {
            _lotRepository = lotRepository;
            _accountRepository = accountRepository;
            _partnerRepositorye = partnerRepositorye;
        }

        [BindProperty]
        public WMS_BLL.Models.Lot Lot { get; set; } = default!;
        [BindProperty]
        public List<LotDetail> LotDetail { get; set; } = new List<LotDetail>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (HttpContext.Session.GetString("account") is null)
            {
                return RedirectToPage("/Login");
            }

            var role = HttpContext.Session.GetString("account");

            if (role != "admin")
            {
                return RedirectToPage("/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var lot = _lotRepository.GetLotById((int)id);
            List<LotDetail> lotdetails = _lotRepository.GetListLotDetailById((int)id);
            if (lot == null)
            {
                return NotFound();
            }
            Lot = lot;
            LotDetail = lotdetails;
            ViewData["AccountId"] = new SelectList(accountService.GetAccounts(), "AccountId", "Email");
            ViewData["PartnerId"] = new SelectList(partnerService.GetPartners(), "PartnerId", "Name");
            return Page();
        }
    }
}
