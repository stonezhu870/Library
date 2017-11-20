using System.Threading.Tasks;

namespace Library.Domain.Core
{
    public interface IEventHandler<TEvent> where TEvent : IDomainEvent
    {
        void Handle(TEvent evt);

        Task HandleAsync(TEvent evt);
    }
}