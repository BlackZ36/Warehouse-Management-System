using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.Interface
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategoryById(int id);
        bool AddCategory(Category category);
        bool UpdateCategory(Category category);
        bool BanCategoryStatus(int id);
        List<Category> LoadCategories();
    }
}
