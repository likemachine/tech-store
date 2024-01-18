using Moq;
using TechStore.Repository;
using TechStore.interfaces;
using TechStore.Models;
using TechStore.Controllers;
using Microsoft.AspNetCore.Mvc;
using TechStore.ViewModels;
using TechStore;
using Microsoft.EntityFrameworkCore;
using Tech_Store.Migrations;

namespace TechStoreTests;

public class OrderControllerTests
{
    [Fact]
    public void Checkout_ReturnsView()
    {
        // Arrange
        var orderController = new OrderController(null, null);

        // Act
        var result = orderController.Checkout();

        // Assert
        Assert.IsType<ViewResult>(result); //Проверяем отображение checkout
    }
    
    [Fact]
    public void Checkout_WithEmptyCart_ReturnsModelError()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDBContent>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        using (var context = new AppDBContent(options))
        {
            var cart = new Cart(context);
            var mockAllOrders = new Mock<IAllOrders>();
            var orderController = new OrderController(mockAllOrders.Object, cart);
        
            // Act
            var result = orderController.Checkout(new Order());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.ViewData.ModelState.ContainsKey(""));
            var modelStateEntry = viewResult.ViewData.ModelState[""];
            Assert.Equal("Корзина пуста", modelStateEntry.Errors[0].ErrorMessage);
        }
    }
    
    // [Fact]
    // public void Checkout_WithInvalidOrderModel_ReturnsViewWithModelError()
    // {
    //     // Arrange
    //     var options = new DbContextOptionsBuilder<AppDBContent>()
    //     .UseInMemoryDatabase(databaseName: "TestDatabase")
    //     .Options;

    //     using (var context = new AppDBContent(options))
    //     {
    //         // Populate in-memory database with a product
    //         var product = new Product { Id = 1, Brand = "B1", Model = "M1", Price = 10000, Img = "test.png", LongDesc = "Test Product" };
    //         context.Product.Add(product);
    //         context.SaveChanges();

    //         // Create a unique CartId for this test
    //         var cartId = Guid.NewGuid().ToString();
    //         var cart = new Cart(context) { CartId = cartId };

    //         // Add product to cart
    //         var cartItem = new CartItem { Product = product, Price = product.Price, CartId = cartId };
    //         context.CartItem.Add(cartItem);
    //         context.SaveChanges();

    //         var orderRepo = new OrderRepo(context, cart);
    //         var orderController = new OrderController(orderRepo, cart);

    //         // Invalid order with missing required fields
    //         var invalidOrder = new Order()
    //         {
    //             Name = "0",
    //             Surname = "0",
    //             Adress = "0",
    //             Phone = 0,
    //             Email = " "
    //         };

    //         // Act
    //         var result = orderController.Checkout(invalidOrder);

    //         var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
    //         Assert.Equal("Complete", redirectToActionResult.ActionName);
    //     }
    // }
    
    [Fact]
    public void Checkout_WithValidOrderAndNonEmptyCart_RedirectsToComplete()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDBContent>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        using (var context = new AppDBContent(options))
        {
            // Populate in-memory database with a product
            var product = new Product { Id = 2, Brand = "B2", Model = "M2", Price = 20000, Img = "test.png", LongDesc = "Test Product" };
            context.Product.Add(product);
            context.SaveChanges();

            // Create a unique CartId for this test
            var cartId = Guid.NewGuid().ToString();
            var cart = new Cart(context) { CartId = cartId };

            // Add product to cart
            var cartItem = new CartItem { Product = product, Price = product.Price, CartId = cartId };
            context.CartItem.Add(cartItem);
            context.SaveChanges();

            var mockAllOrders = new Mock<IAllOrders>();
            var orderController = new OrderController(mockAllOrders.Object, cart);

            // Valid order
            var validOrder = new Order
            {
                Name = "John",
                Surname = "Doe",
                Adress = "123 Main St",
                Phone = "555-1234",
                Email = "john.doe@example.com"
            };

            // Act
            var result = orderController.Checkout(validOrder);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Complete", redirectToActionResult.ActionName);
    }
}

}

