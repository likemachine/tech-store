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
    public void Can_Use_Null_Catalog()
    {
        // Arrange - создание макетов интерфейса и контроллера
        Mock<IAllProducts> mockAllProducts = new Mock<IAllProducts>();
        HomeController controller = new HomeController(mockAllProducts.Object);

        // Act - вызов метода контроллера и получение модели представления
        ViewResult result = controller.List(null);
        HomeViewModel viewModel = result.Model as HomeViewModel;
        
        // Assert - проверки успешного возрвата модели представления с пустым каталогом
        Assert.NotNull(result.Model);
        Assert.Null(viewModel.allProducts.FirstOrDefault()?.Id);
    }

    [Fact]
    public void Can_Add_Product()
    {
        // Arrange - создание макетов и добавление товара в список
        Mock<IAllProducts> mockAllProducts = new Mock<IAllProducts>();

        mockAllProducts.Setup(m => m.Products).Returns(new List<Product> {
            new Product {Brand = "B1"}
            });

        HomeController controller = new HomeController(mockAllProducts.Object);

        // Act
        ViewResult result = controller.List(null);
        HomeViewModel viewModel = result.Model as HomeViewModel;
        
        // Assert - проверка соответствия ожидаемого брэнда продукта
        Assert.Equal("B1", viewModel.allProducts.FirstOrDefault()?.Brand);
    }

    [Fact]
    public void Can_Add_Multiple_Products()
    {
        // Arrange
        Mock<IAllProducts> mockAllProducts = new Mock<IAllProducts>();

        // Cоздание 5 объектов каталога
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
        // Arrange - создание макетов
        Mock<IAllProducts> mockAllProducts = new Mock<IAllProducts>();
        HomeController controller = new HomeController(mockAllProducts.Object);

        // Act - вызов метода контроллера с заданной категорией
        ViewResult resultType = controller.List("Servers");
        HomeViewModel modelType = resultType.Model as HomeViewModel;

        // Assert - проверка переключения категории
        Assert.Equal("Серверы", modelType.currType);
    }
}