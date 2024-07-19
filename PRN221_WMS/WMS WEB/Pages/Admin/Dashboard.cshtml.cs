using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WMS_BLL.Models;
using WMS_DAL.Interface;

namespace WMS_WEB.Pages.Admin
{
    public class DashboardModel : PageModel
    {

        private readonly IProductRepository _productRepository;
        private readonly ILotRepository _lotRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IPartnerRepository _partnerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStockOutRepository _stockOutRepository;
        private readonly IStorageRepository _storageRepository;

        public DashboardModel(IProductRepository productRepository, ILotRepository lotRepository, IAccountRepository accountRepository, IPartnerRepository partnerRepository, ICategoryRepository categoryRepository, IStockOutRepository stockOutRepository, IStorageRepository storageRepository)
        {
            _productRepository = productRepository;
            _lotRepository = lotRepository;
            _accountRepository = accountRepository;
            _partnerRepository = partnerRepository;
            _categoryRepository = categoryRepository;
            _stockOutRepository = stockOutRepository;
            _storageRepository = storageRepository;
        }


        public int ProductCount { get; private set; }
        public int LotCount { get; private set; }
        public int AccountCount { get; private set; }
        public int PartnerCount { get; private set; }
        public int CategoryCount { get; private set; }
        public int StockOutCount { get; private set; }
        public int StorageCount { get; private set; }
        public int Account1 { get; private set; }
        public int Account0 { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {

            // Lấy thông tin từ session
            var account = HttpContext.Session.GetString("account");
            var accountId = HttpContext.Session.GetString("accountId");
            var accountName = HttpContext.Session.GetString("accountName");

            // Kiểm tra nếu các giá trị session thỏa mãn điều kiện
            if (account == "Administrator" && !string.IsNullOrEmpty(accountId) && !string.IsNullOrEmpty(accountName))
            {

                //Lấy ra dữ liệu cho chart
                ProductCount = _productRepository.GetProducts().Count();
                LotCount = _lotRepository.GetAllLots().Count();
                AccountCount = _accountRepository.GetAccounts().Count();
                PartnerCount = _partnerRepository.GetPartners().Count();
                CategoryCount = _categoryRepository.GetCategories().Count();
                StockOutCount = _stockOutRepository.GetStockOuts().Count();
                StorageCount = _storageRepository.GetStorageAreas().Count();

                Account1 = _accountRepository.GetAccounts().Where(s => s.Status == 1).ToList().Count();
                Account0 = _accountRepository.GetAccounts().Where(s => s.Status == 0).ToList().Count();

                // Trả về trang hiện tại nếu thỏa mãn điều kiện
                return Page();
            }
            else
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/Login");
            }

            


        }
    }
}
