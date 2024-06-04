using ShoppingCart.Services;

namespace ShoppingCart.Models
{
    public class Basket
    {
        private readonly List<Product> _products = new List<Product>();
        private readonly IDiscountService _discountService;
        private string _discountCode;

        public Basket(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void AddProducts(IEnumerable<Product> products)
        {
            _products.AddRange(products);
        }

        public void RemoveProduct(Product product) {
            _products.Remove(product);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _products;
        }

        public decimal GetTotalCost()
        {
            return _products.Sum(p => p.Price);
        }

        public void ApplyDiscountCode(string code)
        {
            _discountCode = code;
        }

        public decimal ApplyDiscount()
        {
            var total = GetTotalCost();
            if (!string.IsNullOrEmpty(_discountCode))
            {
                var discountValue = _discountService.GetDiscount(_discountCode);
                return total * (1 - discountValue / 100);
            }
            return total;
        }
    }
}
