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
    public class StockOutRepository : IStockOutRepository
    {
        public List<StockOut> GetStockOuts() => StockOutDAO.Instance.GetList();

        public StockOut GetStockOutById(int id) => StockOutDAO.Instance.GetById(id);

        public bool CreateStockOut(StockOut stockOut) => StockOutDAO.Instance.Create(stockOut);

        public bool UpdateStockOut(StockOut stockOut) => StockOutDAO.Instance.Update(stockOut);

        public bool DeleteStockOut(int id) => StockOutDAO.Instance.Delete(id);
    }
}
