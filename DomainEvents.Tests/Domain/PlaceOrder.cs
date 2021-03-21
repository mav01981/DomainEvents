using Domain.Infrastructure;

namespace DomaninEvents.Tests
{
    public class PlaceOrder: IDomainEvent
    {
        public string CustomerName { get; set; }
    }
}
