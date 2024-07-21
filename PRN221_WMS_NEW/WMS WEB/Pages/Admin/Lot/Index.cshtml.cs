using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WMS_BLL.Models;
using WMS_DAL.Interface;

namespace WMS_WEB.Pages.Admin.Lot
{
    public class IndexModel : PageModel
    {

        private readonly ILotRepository _lotRepository;
        private const int PageSize = 5;

        public IndexModel(ILotRepository lotRepository)
        {
            _lotRepository = lotRepository;
        }

        public IList<WMS_BLL.Models.Lot> Lot { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int curentPage { get; set; } = 1;
        public int pageSize { get; set; } = 5;
        public int count { get; set; }
        public int totalPages => (int)Math.Ceiling(Decimal.Divide(count, pageSize));

        [BindProperty(SupportsGet = true)]
        public string SearchText { get; set; }

        public IActionResult OnGet(int? pageIndex)
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


            if (SearchText != null)
            {

                PRN221_WMSContext context = new PRN221_WMSContext();
                count = context.Lots.ToList()
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                .Count();
                Lot = context.Lots.ToList()
                    .Where(a => a.LotCode.ToUpper().Contains(SearchText.Trim().ToUpper()))
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
            }
            else
            {
                count = _lotRepository.GetAllLots().Count();
                Lot = _lotRepository.GetAllLots()
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
            }

            return Page();
        }
    }
}
