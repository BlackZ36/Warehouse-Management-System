using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;
using WMS_DAL;
using WMS_DAL.Interface;

namespace WMS_DAL.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        private CategoryDAO categoryDAO;

        public CategoryRepository()
        {
            categoryDAO = new CategoryDAO();
        }

        public List<Category> GetCategories()
        {
            return categoryDAO.GetCategories();
        }

        public Category GetCategoryById(int id)
        {
            return categoryDAO.GetCategoryAreaByID(id);
        }

        public bool AddCategory(Category category)
        {
            return categoryDAO.AddCategory(category);
        }

        public bool UpdateCategory(Category category)
        {
            return categoryDAO.UpdateCategory(category);
        }

        public List<Category> LoadCategories()
        {
            return categoryDAO.LoadCategories();
        }

        public bool BanCategoryStatus(int id)
        {
            return categoryDAO.BanCategoryStatus(id);
        }
    }
}
