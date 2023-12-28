using DotnetUnitTesting;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    public class Tests
    {
        private UserAccount user;
        private Product product;

        [SetUp]
        public void Setup()
        {
            //Arrange
            user = new UserAccount("Buyer", "BuyerSurname", "12.11.2000");
            product = new Product(1, "Milk", 30, 1);
        }

        [Test]
        public void AddingProductToTheShoppingCartByUser()
        {
            //Act
            user.ShoppingCart.AddProductToCart(product);

            //Assert
            Assert.That(user.ShoppingCart.Products.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddingTheSameProductToTheShoppingCartByUserSeveralTimes()
        {
            //Act
            user.ShoppingCart.AddProductToCart(product);
            user.ShoppingCart.AddProductToCart(product);

            //Assert
            Assert.That(user.ShoppingCart.Products.Count, Is.EqualTo(1));
            Assert.That(user.ShoppingCart.GetProductById(product.Id).Quantity, Is.EqualTo(2));
        }

        [Test]
        public void GettingTotalPriceOfTheShoppingCart()
        {
            //Act
            user.ShoppingCart.AddProductToCart(product);

            //Assert
            Assert.That(user.ShoppingCart.GetCartTotalPrice(), Is.EqualTo(30));
        }

        [Test]
        public void GettingUnexistedProductFromTheShoppingCart()
        {
            //Assert
            var ex = Assert.Throws<ProductNotFoundException>(() => user.ShoppingCart.GetProductById(1));
            Assert.That(ex.Message, Is.EqualTo($"Product with {1} ID is not found in the shopping cart!"));
        }

        [Test]
        public void RemovingProductFromTheShoppingCartByUser()
        {
            //Arrange
            user.ShoppingCart.AddProductToCart(product);

            //Act
            user.ShoppingCart.RemoveProductFromCart(product);

            //Assert
            Assert.That(user.ShoppingCart.Products.Contains(product), Is.False);
        }

        [Test]
        public void MockCalculateDiscount()
        {
            //Assert
            var user = new UserAccount("John", "Smith", "1990/10/10");
            var mockDiscount = new Mock<IDiscountUtility>();
            mockDiscount.Setup(md => md.CalculateDiscount(user)).Returns(3);
            var orderService = new OrderService(mockDiscount.Object);

            //Act
            var discount = Math.Abs(orderService.GetOrderPrice(user));

            //Assert
            Assert.That(discount, Is.EqualTo(3));
            mockDiscount.Verify(md => md.CalculateDiscount(user), Times.Once);
            mockDiscount.VerifyNoOtherCalls();
        }
    }
}