using Microsoft.EntityFrameworkCore.Migrations;
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
    public class HistoryRepository : Interface.IHistoryRepository
    {
        public List<History> GetHistories(int id) => HistoryDAO.Instance.GetHistories();

        public History GetHistoryById(int id) => HistoryDAO.Instance.GetHistoryById(id);

        public bool CreateHistory(History history) => HistoryDAO.Instance.Create(history);

        public bool DeleteHistory(int id) => HistoryDAO.Instance.Delete(id);
    }
}
