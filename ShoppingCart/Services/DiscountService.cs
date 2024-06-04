namespace ShoppingCart.Services
{
    public interface IDiscountService
    {
        decimal GetDiscount(string code);
    }

    public class DiscountService : IDiscountService
    {
        private readonly Dictionary<string, decimal> _discounts = new()
        {
            { "DISCOUNT10", 10 },
            { "DISCOUNT20", 20 },
            { "DISCOUNT30", 30 }
        };

        public decimal GetDiscount(string code)
        {
            var discount = _discounts.GetValueOrDefault(code);
            return discount;
        }
    }
}
