using Microsoft.EntityFrameworkCore;
using TechStore.Repository;
using TechStore.Models;
using TechStore.Controllers;
using Microsoft.AspNetCore.Mvc;
using TechStore;
using TechStore.interfaces;
using TechStore.ViewModels;

namespace TechStoreTests;

public class InMemoryCartControllerTests
{
    [Fact]
    public void Can_Redirect_To_Cart()
    {
    // Arrange
        var options = new DbContextOptionsBuilder<AppDBContent>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        using (var context = new AppDBContent(options))
        {
        var product = new Product { Id = 2, Brand = "B1", Model = "M1", Price = 10000, Img = "test.png", LongDesc = "Тестовый продукт для inmemoryDB" };
        context.Product.Add(product);
        context.SaveChanges();

        var cart = new Cart(context);
        var productRepo = new ProductRepo(context);
        var controller = new CartController(productRepo, cart);

        // Act
        var result = controller.addToCart(1) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
        Assert.Null(result.ControllerName); // Проверка переадресации в рамках одного контроллера
        }
    }

    [Fact]
    public void Can_View_Cart_When_Empty()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDBContent>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        using (var context = new AppDBContent(options))
        {
        var cart = new Cart(context);
        var producrRepo = new ProductRepo(context);
        var controller = new CartController(producrRepo, cart);

        // Act
        var result = controller.Index();

        // Assert
        var model = Assert.IsType<CartViewModel>(result.Model);
        Assert.Empty(model.Cart.ListCartItems);
        }
    }
}