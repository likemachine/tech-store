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
        var product = new Product { Id = 1, Brand = "B1", Model = "M1", Price = 10000, Img = "test.png", LongDesc = "Тестовый продукт для inmemoryDB" };
        context.Product.Add(product);
        context.SaveChanges();

        // Create a unique CartId for this test
        var cartId = Guid.NewGuid().ToString();
        var cart = new Cart(context) { CartId = cartId };

        // Add product to cart
        var cartItem = new CartItem { Product = product, Price = product.Price, CartId = cartId };
        context.CartItem.Add(cartItem);
        context.SaveChanges();

        // Create CartController
        var productRepo = new ProductRepo(context);
        var controller = new CartController(productRepo, cart);

        // Act
        var result = controller.addToCart(1) as RedirectToActionResult;

        // Assert
        Assert.Equal("Index", result.ActionName);
        Assert.Null(result.ControllerName); // Проверка переадресации в рамках одного контроллера

        var cartItems = cart.getCartItems();
        Assert.Single(cartItems); //Проверка наличия товара в коллекции корзины
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

     [Fact] 
    public void Can_Check_Items_In_Cart() 
    { 
        // Arrange 
        var options = new DbContextOptionsBuilder<AppDBContent>() 
            .UseInMemoryDatabase(databaseName: "TestDatabase") 
            .Options; 
 
        using (var context = new AppDBContent(options)) 
        { 
            var product = new Product { Id = 3, Brand = "B3", Model = "M3", Price = 30000, Img = "test.png", LongDesc = "Тестовый продукт для inmemoryDB" }; 
            context.Product.Add(product); 
            context.SaveChanges(); 
 
            // Create a unique CartId for this test 
            var cartId = Guid.NewGuid().ToString(); 
            var cart = new Cart(context) { CartId = cartId }; 
 
            // Add product to cart 
            var cartItem = new CartItem { Product = product, Price = product.Price, CartId = cartId }; 
            context.CartItem.Add(cartItem); 
            context.SaveChanges(); 
 
            // Create CartController 
            var productRepo = new ProductRepo(context); 
            var controller = new CartController(productRepo, cart); 
 
            // Act 
            controller.addToCart(1);  // No need to check redirection in this test 
 
            // Assert 
            var cartItems = cart.getCartItems(); 
            Assert.Single(cartItems); 
            Assert.Equal(3, cartItems[0].Product.Id); 
        } 
    }

    [Fact] 
    public void Can_Check_Items_In_Cart_With_Three_Products() 
    { 
        // Arrange 
        var options = new DbContextOptionsBuilder<AppDBContent>() 
            .UseInMemoryDatabase(databaseName: "TestDatabase") 
            .Options; 
 
        using (var context = new AppDBContent(options)) 
        { 
            var product1 = new Product { Id = 4, Brand = "B1", Model = "M1", Price = 10000, Img = "test1.png", LongDesc = "Тестовый продукт 1" }; 
            var product2 = new Product { Id = 5, Brand = "B2", Model = "M2", Price = 15000, Img = "test2.png", LongDesc = "Тестовый продукт 2" }; 
            var product3 = new Product { Id = 6, Brand = "B3", Model = "M3", Price = 12000, Img = "test3.png", LongDesc = "Тестовый продукт 3" }; 
            
            context.Product.AddRange(product1, product2, product3);
            context.SaveChanges(); 
 
            // Create a unique CartId for this test 
            var cartId = Guid.NewGuid().ToString(); 
            var cart = new Cart(context) { CartId = cartId }; 
 
            // Add products to cart 
            var cartItem1 = new CartItem { Product = product1, Price = product1.Price, CartId = cartId }; 
            var cartItem2 = new CartItem { Product = product2, Price = product2.Price, CartId = cartId }; 
            var cartItem3 = new CartItem { Product = product3, Price = product3.Price, CartId = cartId }; 
            
            context.CartItem.AddRange(cartItem1, cartItem2, cartItem3);
            context.SaveChanges(); 
 
            // Create CartController 
            var productRepo = new ProductRepo(context); 
            var controller = new CartController(productRepo, cart); 
 
            // Act 
            // Assuming addToCart method is capable of adding multiple products at once
            controller.addToCart(3);  // No need to check redirection in this test 
 
            // Assert 
            var cartItems = cart.getCartItems(); 
            Assert.Equal(3, cartItems.Count);
            Assert.Contains(cartItems, item => item.Product.Id == 4);
            Assert.Contains(cartItems, item => item.Product.Id == 5);
            Assert.Contains(cartItems, item => item.Product.Id == 6);
        } 
    }
}