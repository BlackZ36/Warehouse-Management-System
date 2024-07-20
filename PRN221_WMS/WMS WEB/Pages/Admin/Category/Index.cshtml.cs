using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WMS_DAL.Interface;

namespace WMS_WEB.Pages.Admin.Category
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;

        public IndexModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IList<WMS_BLL.Models.Category> Category { get; set; } = default!;

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
                Category = _categoryRepository.GetCategories()
                    .Where(a => a.CategoryCode.ToUpper().Contains(SearchText.Trim().ToUpper()) || a.Name.ToLower().Contains(SearchText.Trim().ToLower()))
                    .Skip((curentPage - 1) * pageSize).Take(pageSize)
                    .ToList();
            }
            else
            {
                count = _categoryRepository.GetCategories().Count();
                Category = _categoryRepository.GetCategories()
                            .Skip((curentPage - 1) * pageSize).Take(pageSize)
                            .ToList();
            }
            return Page();
        }
    }
}
