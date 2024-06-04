using ShoppingCart.Models;
using ShoppingCart.Services;
using Moq;

public class BasketServiceTests
{
    [Fact]
    public void GetProducts_ShouldReturnProductsInBasket()
    {
        // Arrange
        var mockDiscountService = new Mock<IDiscountService>();
        var basket = new Basket(mockDiscountService.Object);
        var basketService = new BasketService(basket);
        var product1 = new Product { Id = 1, Name = "Test Product 1", Price = 10m };
        var product2 = new Product { Id = 2, Name = "Test Product 2", Price = 20m };
        basketService.AddProduct(product1);
        basketService.AddProduct(product2);

        // Act
        var products = basketService.GetProducts();

        // Assert
        Assert.Collection(products,
            p => Assert.Equal(product1, p),
            p => Assert.Equal(product2, p));
    }

    [Fact]
    public void AddProduct_ShouldAddProductToBasket()
    {
        // Arrange
        var mockDiscountService = new Mock<IDiscountService>();
        var basket = new Basket(mockDiscountService.Object);
        var basketService = new BasketService(basket);
        var product = new Product { Id = 1, Name = "Test Product", Price = 10m };

        // Act
        basketService.AddProduct(product);

        // Assert
        Assert.Collection(basket.GetProducts(),
            p => Assert.Equal(product, p));
    }

    [Fact]
    public void RemoveProduct_shouldRemoveProductFromBasket()
    {
        // Arrange
        var mockDiscountService = new Mock<IDiscountService>();
        var basket = new Basket(mockDiscountService.Object);
        var basketService = new BasketService(basket);
        var product = new Product { Id = 1, Name = "Test Product", Price = 10m };
        basketService.AddProduct(product);

        // Act
        basketService.RemoveProduct(product);

        // Assert
        Assert.Empty(basket.GetProducts());
    }

    [Fact]
    public void GetTotalCost_ShouldReturnZero_WhenBasketIsEmpty()
    {
        // Arrange
        var mockDiscountService = new Mock<IDiscountService>();
        var basket = new Basket(mockDiscountService.Object);
        var basketService = new BasketService(basket);

        // Act
        var totalCost = basketService.GetTotalCost();

        // Assert
        Assert.Equal(0m, totalCost);
    }

    [Fact]
    public void GetTotalCost_ShouldReturnTotalCostOfProducts()
    {
        // Arrange
        var mockDiscountService = new Mock<IDiscountService>();
        var basket = new Basket(mockDiscountService.Object);
        var basketService = new BasketService(basket);
        var product1 = new Product { Id = 1, Name = "Test Product 1", Price = 10m };
        var product2 = new Product { Id = 2, Name = "Test Product 2", Price = 20m };
        basketService.AddProduct(product1);
        basketService.AddProduct(product2);

        // Act
        var totalCost = basketService.GetTotalCost();

        // Assert
        Assert.Equal(30m, totalCost);
    }

    [Fact]
    public void AddProduct_ShouldIncreaseTotalCost()
    {
        // Arrange
        var mockDiscountService = new Mock<IDiscountService>();
        var basket = new Basket(mockDiscountService.Object);
        var basketService = new BasketService(basket);
        var product = new Product { Id = 1, Name = "Test Product", Price = 10m };

        // Act
        basketService.AddProduct(product);

        // Assert
        Assert.Equal(10m, basketService.GetTotalCost());
    }

    [Fact]
    public void RemoveProduct_ShouldDecreaseTotalCost()
    {
        // Arrange
        var mockDiscountService = new Mock<IDiscountService>();
        var basket = new Basket(mockDiscountService.Object);
        var basketService = new BasketService(basket);
        var product = new Product { Id = 1, Name = "Test Product", Price = 10m };
        basketService.AddProduct(product);

        // Act
        basketService.RemoveProduct(product);

        // Assert
        Assert.Equal(0m, basketService.GetTotalCost());
    }

    [Fact]
    public void ApplyDiscount_ShouldReturnDiscountedTotal()
    {
        // Arrange
        var mockDiscountService = new Mock<IDiscountService>();
        mockDiscountService.Setup(ds => ds.GetDiscount(It.IsAny<string>())).Returns(10);
        var basket = new Basket(mockDiscountService.Object);
        var basketService = new BasketService(basket);
        var product = new Product { Id = 1, Name = "Test Product", Price = 100m };
        basketService.AddProduct(product);
        basketService.ApplyDiscountCode("DISCOUNT10");

        // Act
        var discountedTotal = basketService.ApplyDiscount();

        // Assert
        Assert.Equal(90m, discountedTotal); // 10% discount on 100
    }
}
