using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMS_BLL.Models;
using WMS_DAL.Implement;
using WMS_DAL.Interface;

namespace BL3w_GroupProject.Pages.Admin.ManageLot
{
    public class LotEditModel : PageModel
    {
        private readonly ILotRepository _lotRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IPartnerRepository _partnerRepository;

        public LotEditModel()
        {
            _lotRepository = new LotRepository();
            _accountRepository = new AccountRepository();
            _partnerRepository = new PartnerRepository();
        }
        [BindProperty]
        public Lot Lot { get; set; } = default!;
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
            ViewData["AccountId"] = new SelectList(_accountRepository.GetAccounts(), "AccountId", "Email");
            ViewData["PartnerId"] = new SelectList(_partnerRepository.GetPartners(), "PartnerId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _lotRepository.UpdateLot(Lot);
                for (int i = 0; i < LotDetail.Count; i++)
                {
                    LotDetail[i].LotDetailId = Convert.ToInt32(Request.Form[$"LotDetail[{i}].LotDetailId"]);
                    LotDetail[i].Quantity = Convert.ToInt32(Request.Form[$"LotDetail[{i}].Quantity"]);
                    LotDetail[i].PartnerId = Lot.PartnerId;
                    _lotRepository.UpdateLotDetail(LotDetail[i]);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LotExists(Lot.LotId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./ListLot");
        }

        private bool LotExists(int id)
        {
            return _lotRepository.GetLotByAccountId((int)id) != null;
        }
    }
}
