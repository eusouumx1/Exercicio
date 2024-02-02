using Exercicio.Entities;

namespace Exercicio.Repository
{
    public class ProductRepository
    {

        private List<Product> _products = new()
        {
             new Product { Id = 1, ProductName = "Apple", Description = "Red Fruit"},
            new Product{ Id = 2, ProductName = "Car", Description = "Thing that moves" }
        };
        
        

        public List<Product> ShowProducts()
        {
            return _products;
        }

        public Product ShowProductById(int id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }

        public Product RegisterProduct(Product newProduct)
        {
            newProduct.Id = _products.Count + 1;
            _products.Add(newProduct);
            return newProduct;
        }

        public Product Edit(Product product, int id)
        {
            var OldProduct = ShowProductById(id);
            OldProduct.ProductName = product.ProductName;
            OldProduct.Description = product.Description;
            return OldProduct;
        }

        public void Delete(int Id)
        {
            var Product = ShowProductById(Id);

            _products.Remove(Product);
        }
    }
}
