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
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO productDAO;

        public ProductRepository()
        {
            productDAO = new ProductDAO();
        }
        public List<Product> GetProducts() => productDAO.GetProducts();
        public Product AddProduct(Product product) => productDAO.AddProduct(product);
        public Product GetProductByID(int? productId) => productDAO.GetProductByID(productId);
        public Product UpdateProduct(Product product) => productDAO.UpdateProduct(product);
        public Product DeleteProduct(int? product) => productDAO.DeleteProduct(product);
    }
}
