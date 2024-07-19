using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WMS_BLL.Models;
using WMS_DAL.Implement;
using WMS_DAL.Interface;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BL3w_GroupProject.Pages.Admin.ManageLotDetail
{
    public class ListLotDetailModel : PageModel
    {
        private readonly ILotRepository _lotRepository;
        private const int PageSize = 5;
        public ListLotDetailModel()
        {
            _lotRepository = new LotRepository();
        }


        [BindProperty(SupportsGet = true)]
        public int curentPage { get; set; } = 1;
        public int pageSize { get; set; } = 5;
        public int count { get; set; }
        public int totalPages => (int)Math.Ceiling(Decimal.Divide(count, pageSize));


        [BindProperty(SupportsGet = true)]
        public string SearchText { get; set; }


        public IList<LotDetail> LotDetail { get; set; } = default!;

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
                count = _lotRepository.GetAllLotDetail()
                    .Where(a => a.Lot.LotCode.ToUpper().Equals(SearchText.ToUpper().Trim()) || a.Product.ProductCode.ToUpper().Equals(SearchText.ToUpper().Trim()))
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .Count();
                LotDetail = _lotRepository.GetAllLotDetail()
                    .Where(a => a.Lot.LotCode.ToUpper().Equals(SearchText.ToUpper().Trim()) || a.Product.ProductCode.ToUpper().Equals(SearchText.ToUpper().Trim()))
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
            }
            else
            {
                count = _lotRepository.GetAllLotDetail().Count();
                LotDetail = _lotRepository.GetAllLotDetail()
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
            }
            return Page();
        }
    }
}
