using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;
using WMS_DAL.DAO;
using WMS_DAL.Interface;

namespace WMS_DAL.Repository
{
    public class StockOutDetailRepository : IStockOutDetailRepository
    {
        public List<StockOutDetail> GetStockOutDetails() => StockOutDetailDAO.Instance.GetStockOuts();

        public StockOutDetail GetStockOutDetailById(int id) => StockOutDetailDAO.Instance.GetStockOutById(id);

        public bool CreateStockOutDetail(StockOutDetail stockOutDetail) => StockOutDetailDAO.Instance.Create(stockOutDetail);

        public bool UpdateStockOutDetail(StockOutDetail stockOutDetail) => StockOutDetailDAO.Instance.Update(stockOutDetail);

        public bool DeleteStockOutDetail(int id) => StockOutDetailDAO.Instance.Delete(id);
    }
}
