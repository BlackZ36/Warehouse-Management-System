using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_BLL.Models;

namespace WMS_DAL.Interface
{
    public interface IProductRepository
    {
        public List<Product> GetProducts();
        public Product AddProduct(Product product);
        public Product GetProductByID(int? productId);
        public Product UpdateProduct(Product product);
        public Product DeleteProduct(int? product);
    }
}
