using Exercicio.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Exercicio.Repository
{
    public class PurchaseRepository()
    {
        private List<Product> _cartProducts = new List<Product>();
        
        

        public List<Product> ShowCart()
        {
            return _cartProducts;
        }
        public void PutInCart(int id, ProductRepository repository)
        {
            var productId = repository.ShowProductById(id);

            _cartProducts.Add(productId);

        }
        public void TakeOffCart(int id)
        {
            var productId = _cartProducts.FirstOrDefault(x => x.Id == id);
         
            _cartProducts.Remove(productId);
        }
    }
}
