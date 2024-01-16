using Xunit;
using Moq;
using TechStore.interfaces;
using TechStore.Models;
using TechStore.Controllers;
using Microsoft.AspNetCore.Mvc;
using TechStore.ViewModels;

namespace TechStoreTests;

public class HomeControllerTests
{
    [Fact]
    public void Can_Use_Product_Repo()
    {
        // Arrange
        Mock<IAllProducts> mockAllProducts = new Mock<IAllProducts>();

        // Настройка макетов
        mockAllProducts.Setup(m => m.Products).Returns(new List<Product> {});
        HomeController controller = new HomeController(mockAllProducts.Object);

        // Act
        ViewResult result = controller.List(null);
        HomeViewModel viewModel = result.Model as HomeViewModel;
        
        // Assert
        Assert.NotNull(result.Model);
        Assert.Null(viewModel.allProducts.FirstOrDefault()?.Id); // Проверка возможности использовать пустые макеты в контроллере
    }

    [Fact]
    public void Can_Add_Product()
    {
        // Arrange
        Mock<IAllProducts> mockAllProducts = new Mock<IAllProducts>();

        // Настройка макетов
        mockAllProducts.Setup(m => m.Products).Returns(new List<Product> {new Product {Brand = "B1"}});
        HomeController controller = new HomeController(mockAllProducts.Object);

        // Act
        ViewResult result = controller.List(null);
        HomeViewModel viewModel = result.Model as HomeViewModel;
        
        // Assert
        Assert.Equal("B1", viewModel.allProducts.FirstOrDefault()?.Brand); // Проверка соответствия ожидаемого названия брэнда продукта
    }

    [Fact]
    public void Can_Add_Multiple_Products()
    {
        // Arrange
        Mock<IAllProducts> mockAllProducts = new Mock<IAllProducts>();

        // Настройка макетов и создание 5 объектов
        mockAllProducts.Setup(m => m.Products).Returns(new List<Product> 
        {
            new Product {Brand = "B1", Model = "M1", Price = 10000},
            new Product {Brand = "B2", Price = 20000, Type = new TechStore.Models.Type {TypeName = "Компьютеры"}},
            new Product {Brand = "B3", Model = "M3", Price = 30000, Type = new TechStore.Models.Type {TypeName = "Серверы"}},
            new Product {Brand = "B4", Img = "/img/Acer.jpg", Price = 40000, Type = new TechStore.Models.Type {TypeName = "Компьютеры"}},
            new Product {Brand = "B5"}
        });

        HomeController controller = new HomeController(mockAllProducts.Object);

        // Act
        ViewResult result = controller.List(null);
        HomeViewModel viewModel = result.Model as HomeViewModel;

        // Проверки соозданных продуктов
        Assert.Equal("M1", viewModel.allProducts.ElementAt(0).Model);
        Assert.Equal(20000, viewModel.allProducts.ElementAt(1).Price);
        Assert.Equal("Серверы", viewModel.allProducts.ElementAt(2).Type.TypeName);
        Assert.Equal("/img/Acer.jpg", viewModel.allProducts.ElementAt(3).Img);
        Assert.Equal("B5", viewModel.allProducts.ElementAt(4).Brand);
        Assert.Equal(5, viewModel.allProducts.Count()); //Общее число ожидаемых объектов
    }

    [Fact]
    public void Can_Switch_To_Product_Type_Category()
    {
        // Arrange
        Mock<IAllProducts> mockAllProducts = new Mock<IAllProducts>();
        HomeController controller = new HomeController(mockAllProducts.Object);

        // Act
        ViewResult resultType = controller.List("Servers");
        HomeViewModel modelType = resultType.Model as HomeViewModel;

        // Assert - проверка переключения категории
        Assert.Equal("Серверы", modelType.currType);
    }
}