using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object padlock = new object();
        private PRN221_WMSContext _context;

        private CategoryDAO()
        {
            _context = new PRN221_WMSContext();
        }

        public static CategoryDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Category> GetCategories()
        {
            try
            {
                return _context.Categories.ToList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error retrieving categories", ex);
            }
        }

        public Category GetCategoryById(int categoryId)
        {
            try
            {
                return _context.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving category with ID {categoryId}", ex);
            }
        }

        public bool Create(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error creating category", ex);
            }
        }

        public bool Update(Category category)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var existing = db.Categories.SingleOrDefault(c => c.CategoryId == category.CategoryId);
                    if (existing != null)
                    {
                        // Update only non-null properties
                        existing.CategoryCode = category.CategoryCode;
                        existing.Name = category.Name;
                        existing.Description = category.Description;
                        existing.UpdatedAt = DateTime.Now;
                        existing.Status = category.Status;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Category not found for updating.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error updating category", ex);
            }
        }

        public bool Delete(int categoryId)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var category = db.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
                    if (category != null)
                    {
                        db.Categories.Remove(category);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Category not found for deletion.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error deleting category with ID {categoryId}", ex);
            }
        }


    }
}
