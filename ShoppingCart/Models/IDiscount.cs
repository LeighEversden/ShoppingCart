namespace ShoppingCart.Models
{
    public interface IDiscount
    {
        decimal Apply(decimal total);
    }

    public class PercentageDiscount : IDiscount
    {
        public decimal Percentage { get; set; }

        public decimal Apply(decimal total)
        {
            return total * (1 - Percentage / 100);
        }
    }
}
