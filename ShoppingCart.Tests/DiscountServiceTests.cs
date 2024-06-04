using ShoppingCart.Services;

public class DiscountServiceTests
{
    [Theory]
    [InlineData("DISCOUNT10", 10)]
    [InlineData("DISCOUNT20", 20)]
    [InlineData("DISCOUNT30", 30)]
    [InlineData("INVALID", 0)]
    public void GetDiscount_ShouldReturnCorrectDiscount(string code, decimal expectedDiscount)
    {
        // Arrange
        var discountService = new DiscountService();

        // Act
        var discount = discountService.GetDiscount(code);

        // Assert
        Assert.Equal(expectedDiscount, discount);
    }

    [Fact]
    public void GetDiscount_ShouldReturnZeroForInvalidCode()
    {
        // Arrange
        var discountService = new DiscountService();

        // Act
        var discount = discountService.GetDiscount("INVALID");

        // Assert
        Assert.Equal(0, discount);
    }

    [Fact]
    public void GetDiscount_ShouldReturnCorrectDiscountForValidCode()
    {
        // Arrange
        var discountService = new DiscountService();

        // Act
        var discount = discountService.GetDiscount("DISCOUNT10");

        // Assert
        Assert.Equal(10, discount);
    }

}
