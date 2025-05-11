using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object padlock = new object();
        private PRN221_WMSContext _context;

        private ProductDAO()
        {
            _context = new PRN221_WMSContext();
        }

        public static ProductDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Product> GetProducts()
        {
            try
            {
                return _context.Products
                    .Include(p => p.Category)  // Eager load related Category entity
                    .Include(p => p.Storage)   // Eager load related Storage entity
                    .ToList();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error retrieving products with related entities", ex);
            }
        }

        public Product GetProductById(int productId)
        {
            try
            {
                return _context.Products
                    .Include(p => p.Category)  // Eager load related Category entity
                    .Include(p => p.Storage)   // Eager load related Storage entity
                    .SingleOrDefault(p => p.ProductId == productId)
                    ?? throw new Exception($"Product with ID {productId} not found.");
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error retrieving product with ID {productId}", ex);
            }
        }

        public bool Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error creating product", ex);
            }
        }

        public bool Update(Product product)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var existing = db.Products.SingleOrDefault(p => p.ProductId == product.ProductId);
                    if (existing != null)
                    {
                        // Update only non-null properties
                        existing.ProductCode = product.ProductCode ?? existing.ProductCode;
                        existing.CategoryId = product.CategoryId; // Assuming CategoryId is always provided
                        existing.StorageId = product.StorageId; // Assuming StorageId is always provided
                        existing.Name = product.Name ?? existing.Name;
                        existing.Quantity = product.Quantity ?? existing.Quantity;
                        existing.Unit = product.Unit ?? existing.Unit;
                        existing.Status = product.Status ?? existing.Status;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Product not found for updating.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error updating product", ex);
            }
        }

        public bool Delete(int productId)
        {
            try
            {
                using (var db = new PRN221_WMSContext())
                {
                    var product = db.Products.SingleOrDefault(p => p.ProductId == productId);
                    if (product != null)
                    {
                        db.Products.Remove(product);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Product not found for deletion.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error deleting product with ID {productId}", ex);
            }
        }


    }
}
