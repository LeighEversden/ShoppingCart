// BasketService.cs
using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public class BasketService
    {
        private readonly Basket _basket;
        public event Action OnChange;

        public BasketService(Basket basket)
        {
            _basket = basket;
        }

        public void AddProduct(Product product)
        {
            _basket.AddProduct(product);
            NotifyStateChanged();
        }

        public void AddProducts(IEnumerable<Product> products)
        {
            _basket.AddProducts(products);
            NotifyStateChanged();
        }

        public void RemoveProduct(Product product)
        {
            _basket.RemoveProduct(product);
            NotifyStateChanged();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _basket.GetProducts();
        }

        public decimal GetTotalCost()
        {
            return _basket.GetTotalCost();
        }

        public decimal GetTotalDiscounted()
        {
            var total = _basket.GetTotalCost();
            return total - _basket.ApplyDiscount();
        }

        public decimal ApplyDiscount()
        {
            var totalWithDiscount = _basket.ApplyDiscount();
            NotifyStateChanged();
            return totalWithDiscount;
        }

        public void ApplyDiscountCode(string code)
        {
            _basket.ApplyDiscountCode(code);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
