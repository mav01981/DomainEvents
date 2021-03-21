using Domain.Infrastructure;
using Xunit;

namespace DomaninEvents.Tests
{
    public class DomainEventsTests
    {
        public DomainEventsTests()
        {
            DomainEvents.ClearCallbacks();
        }

        [Fact]
        public void PlaceOrder_EventRegisterd_Fired()
        {
            //Arrange
            var customerName = string.Empty;
            DomainEvents.Register<PlaceOrder>(p => customerName = p.CustomerName);
            //Act
            DomainEvents.Raise(new PlaceOrder() { CustomerName = "John Doe" });
            //Assert
            Assert.Equal("John Doe", customerName);
        }

        [Fact]
        public void PlaceOrder_EventNotRegisterd_NotFired()
        {
            //Arrange
            var customerName = string.Empty;
            //Act
            DomainEvents.Raise(new PlaceOrder() { CustomerName = "John Doe" });
            //Assert
            Assert.Equal("", customerName);
        }
    }
}
