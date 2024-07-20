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
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories() => CategoryDAO.Instance.GetCategories();

        public Category GetCategoryById(int id) => CategoryDAO.Instance.GetCategoryById(id);

        public bool CreateCategory(Category category) => CategoryDAO.Instance.Create(category);

        public bool UpdateCategory(Category category) => CategoryDAO.Instance.Update(category);

        public bool DeleteCategory(int id) => CategoryDAO.Instance.Delete(id);
    }
}
