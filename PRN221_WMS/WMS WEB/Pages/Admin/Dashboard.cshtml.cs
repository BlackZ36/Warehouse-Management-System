using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WMS_BLL.Models;
using WMS_DAL.DAO;
using WMS_DAL.Interface;

namespace WMS_WEB.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IStorageRepository _storageRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILotRepository _lotRepository;
        private readonly IStockOutRepository _stockOutRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly INotificationRepository _notificationRepository;

        public List<WMS_BLL.Models.Partner> Partners { get; private set; }
        public List<WMS_BLL.Models.Storage> Storages { get; private set; }
        public List<string> StorageNameList { get; private set; }
        public List<decimal> StorageUsageList { get; private set; }
        public List<WMS_BLL.Models.Category> Categories { get; private set; }
        public List<CategoryData> CategoryNameList { get; private set; }
        public List<WMS_BLL.Models.Lot> Lots { get; private set; }
        public List<int> LotCounts { get; private set; }
        public List<WMS_BLL.Models.StockOut> StockOuts { get; private set; }
        public List<int> StockOutCounts { get; private set; }
        public List<CombinedItem> CombinedList { get; set; } = new List<CombinedItem>();
        public List<WMS_BLL.Models.Product> Products { get; private set; }
        public List<WMS_BLL.Models.Notification> Notifications { get; private set; }
        public List<WMS_BLL.Models.Account> Accounts { get; private set; }

        public string RecentDates { get; set; }


        public DashboardModel(
            IPartnerRepository partnerRepository,
            IStorageRepository storageRepository,
            ICategoryRepository categoryRepository,
            ILotRepository lotRepository,
            IStockOutRepository stockOutRepository,
            IProductRepository productRepository,
            IAccountRepository accountRepository,
            INotificationRepository notificationRepository)
        {
            _partnerRepository = partnerRepository;
            _storageRepository = storageRepository;
            _categoryRepository = categoryRepository;
            _lotRepository = lotRepository;
            _stockOutRepository = stockOutRepository;
            _productRepository = productRepository;
            _accountRepository = accountRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task OnGetAsync()
        {
            //lấy ra danh sách các entity cần dùng
            Partners = _partnerRepository.GetPartners();
            Storages = _storageRepository.GetStorages();
            Categories = _categoryRepository.GetCategories();
            Notifications = _notificationRepository.GetNotifications();
            Lots = _lotRepository.GetAllLots();
            StockOuts = _stockOutRepository.GetStockOuts();
            Products = _productRepository.GetProducts();
            Accounts = _accountRepository.GetAccounts();

            //add combine lot
            GetCombinedList(5);

            //Stock Lot Chart
            RecentDates = GetRecentDate(5);
            LotCounts = GetLotCounts(5);
            StockOutCounts = GetStockOutCounts(5);

            //Category Chart
            CategoryNameList = GetCategoryDataList();

            //Storage Usage Chart
            StorageNameList = GetStorageNameList();
            StorageUsageList = GetStorageUsageList();
        }

        public List<CategoryData> GetCategoryDataList()
        {
            // Giả sử _categoryRepository có phương thức GetCategories() để lấy danh mục và số lượng
            return _categoryRepository.GetCategories()
                .Select(c => new CategoryData
                {
                    Name = c.Name,
                    Value = GetCategoryValue(c.CategoryId) // Phương thức giả định để lấy số lượng
                })
                .ToList();
        }


        public int GetCategoryValue(int categoryId)
        {
            return _productRepository.GetProducts().Where(p => p.CategoryId == categoryId).Count();
        }

        private string CalculateTimeAgo(DateTime? createdAt)
        {
            if (createdAt == null)
                return "Unknown";

            var currentTime = DateTime.Now; // Lấy ngày hiện tại
            var timeSpan = currentTime - createdAt.Value;

            if (timeSpan.TotalDays > 1)
                return $"{(int)timeSpan.TotalDays} days ago";
            if (timeSpan.TotalHours > 1)
                return $"{(int)timeSpan.TotalHours} hours ago";
            if (timeSpan.TotalMinutes > 1)
                return $"{(int)timeSpan.TotalMinutes} minutes ago";
            return "Just now";
        }

        private void GetCombinedList(int limit)
        {
            foreach (var lot in Lots)
            {
                var timeAgo = CalculateTimeAgo(lot.CreatedAt);
                CombinedItem item = new CombinedItem();
                item.Id = lot.LotId;
                item.Date = lot.CreatedAt;
                item.Type = 1;
                item.Code = lot.LotCode;
                item.AccountName = lot.Account.Name;
                item.PartnerName = lot.Partner.Name;
                item.Description = "Đã thực hiện nhập lô hàng từ";
                item.TimeAgo = timeAgo;
                CombinedList.Add(item);
            }
            //add combit stockout
            foreach (var stockout in StockOuts)
            {
                var timeAgo = CalculateTimeAgo(stockout.CreatedAt);
                CombinedItem item = new CombinedItem();
                item.Id = stockout.StockOutId;
                item.Date = stockout.CreatedAt;
                item.Type = 2;
                item.Code = stockout.StockOutCode;
                item.AccountName = stockout.Account.Name;
                item.PartnerName = stockout.Partner.Name;
                item.Description = "Đã thực hiện xuất lô hàng đến";
                item.TimeAgo = timeAgo;
                CombinedList.Add(item);
            }
            CombinedList = CombinedList.OrderBy(item => item.Date).Take(limit).ToList();
        }

        private string GetRecentDate(int date)
        {
            var currentDate = DateTime.Now;

            // Tạo danh sách x ngày gần nhất
            var recentDates = Enumerable.Range(0, date)
                .Select(offset => $"'{currentDate.AddDays(-offset).ToString("d/M")}'")
                .Reverse()
                .ToList();

            // Chuyển danh sách ngày thành chuỗi, với các ngày được phân cách bởi dấu phẩy
            var result = string.Join(", ", recentDates);

            // Quay về kết quả dưới dạng chuỗi
            return $"{result}";
        }

        private List<int> GetLotCounts(int date)
        {
            var currentDate = DateTime.Now.Date;
            return Enumerable.Range(0, date)
                .Select(offset => currentDate.AddDays(-offset))
                .Select(date => _lotRepository.GetAllLots()
                    .Count(lot => lot.CreatedAt.HasValue && lot.CreatedAt.Value.Date == date.Date))
                .Reverse() // Đảo ngược danh sách để có thứ tự từ xa nhất đến gần nhất
                .ToList();
        }

        private List<int> GetStockOutCounts(int date)
        {
            var currentDate = DateTime.Now.Date;
            return Enumerable.Range(0, date)
                .Select(offset => currentDate.AddDays(-offset))
                .Select(date => _stockOutRepository.GetStockOuts()
                    .Count(lot => lot.CreatedAt.HasValue && lot.CreatedAt.Value.Date == date.Date))
                .Reverse() // Đảo ngược danh sách để có thứ tự từ xa nhất đến gần nhất
                .ToList();
        }

        private List<string> GetStorageNameList()
        {
            return _storageRepository.GetStorages().Select(s => s.Name).ToList();
        }

        public List<decimal> GetStorageUsageList()
        {
            var storages = _storageRepository.GetStorages();
            List<decimal> result = new List<decimal>();

            foreach (var item in storages)
            {
                decimal usage = (decimal)((decimal)item.CurrentCapacity / item.MaxCapacity * 100);
                usage = Math.Round(usage, 1);
                result.Add(usage);
            }

            return result;
        }

        public int GetActiveAccount()
        {
            return _accountRepository.GetAccounts().Count(a => a.Status == 1);
        }

        public int GetBanAccount()
        {
            return _accountRepository.GetAccounts().Count(a => a.Status == 0);
        }

    }
}
