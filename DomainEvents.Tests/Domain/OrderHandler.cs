using Domain.Infrastructure;

namespace DomaninEvents.Tests
{
    public class OrderHandler : IHandles<PlaceOrder>
    {
        public void Handle(PlaceOrder args)
        {
           //do stuff.
        }
    }
}
