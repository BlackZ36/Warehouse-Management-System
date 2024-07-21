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
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetProducts() => ProductDAO.Instance.GetProducts();

        public Product GetProductById(int id) => ProductDAO.Instance.GetProductById(id);

        public bool CreateProduct(Product product) => ProductDAO.Instance.Create(product);

        public bool UpdateProduct(Product product) => ProductDAO.Instance.Update(product);

        public bool DeleteProduct(int id) => ProductDAO.Instance.Delete(id);
    }
}
