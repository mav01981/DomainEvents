namespace Domain.Infrastructure
{
    public interface IHandles<T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}
