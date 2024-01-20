using Moq;
using TechStore.interfaces;
using TechStore.Models;
using TechStore.Controllers;
using Microsoft.AspNetCore.Mvc;
using TechStore;
using Microsoft.EntityFrameworkCore;

namespace TechStoreTests;

public class OrderControllerTests
{
    [Fact]
    public void Checkout_ReturnsView()
    {
        // Arrange - создание контроллера заказов
        var orderController = new OrderController(null, null);

        // Act - вызов метода контроллера
        var result = orderController.Checkout();

        // Assert - проверка отображение checkout
        Assert.IsType<ViewResult>(result);
    }
    
    [Fact]
    public void Checkout_WithEmptyCart_ReturnsModelError()
    {
        // Arrange - создание временной бд в памяти
        var options = new DbContextOptionsBuilder<AppDBContent>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        // создание корзины и макета заказа
        using var context = new AppDBContent(options);
        var cart = new Cart(context);
        var mockAllOrders = new Mock<IAllOrders>();
        var orderController = new OrderController(mockAllOrders.Object, cart);

        // Act - вызов метода контроллера с заказом
        var result = orderController.Checkout(new Order());

        // Assert проверка наличия ошибки пустой корзины
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.True(viewResult.ViewData.ModelState.ContainsKey(""));
        var modelStateEntry = viewResult.ViewData.ModelState[""];
        Assert.Equal("Корзина пуста", modelStateEntry.Errors[0].ErrorMessage);
    }
    
    [Fact]
    public void Checkout_WithValidOrderAndNonEmptyCart_RedirectsToComplete()
    {
        // Arrange - создание временной бд в памяти
        var options = new DbContextOptionsBuilder<AppDBContent>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        // Сохраняем продукт в бд
        using var context = new AppDBContent(options);
        var product = new Product { Id = 2, Brand = "B2", Model = "M2", Price = 20000, Img = "test.png", LongDesc = "Test Product" };
        context.Product.Add(product);
        context.SaveChanges();

        // Создаем уникальную корзину
        var cartId = Guid.NewGuid().ToString();
        var cart = new Cart(context) { CartId = cartId };

        // Добавляем продукт в корзину и сохраняем в бд
        var cartItem = new CartItem { Product = product, Price = product.Price, CartId = cartId };
        context.CartItem.Add(cartItem);
        context.SaveChanges();

        // Создание макета формы заказа и контроллера заказа с макетом и корзиной
        var mockAllOrders = new Mock<IAllOrders>();
        var orderController = new OrderController(mockAllOrders.Object, cart);

        // Создаем валидный заказ
        var validOrder = new Order
        {
            Name = "John",
            Surname = "Doe",
            Adress = "123 Main St",
            Phone = "555-1234",
            Email = "john.doe@example.com"
        };

        // Вызов метода контроллера, который перенаправляет на страницу подтверждения при валидности формы заказа и наличия товара 
        var result = orderController.Checkout(validOrder);

        // Assert - проверка соответствия модели вывода "Complete" 
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Complete", redirectToActionResult.ActionName);
    }

}

