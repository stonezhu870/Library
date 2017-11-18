using Library.Domain.Core;
using Library.Domain.Core.Messaging;
using Library.Service.Inventory.Domain.DataAccessors;
using Library.Service.Inventory.Domain.Events;
using System.Threading.Tasks;

namespace Library.Service.Inventory.Domain.EventHandlers
{
    public class BookInventoryCreatedEventHandler : IEventHandler<BookInventoryCreatedEvent>
    {
        private IInventoryReportDataAccessor _reportDataAccessor = null;
        private ICommandTracker _commandTracker = null;

        public BookInventoryCreatedEventHandler(IInventoryReportDataAccessor reportDataAccessor, ICommandTracker commandTracker)
        {
            _reportDataAccessor = reportDataAccessor;
            _commandTracker = commandTracker;
        }

        public void Handle(BookInventoryCreatedEvent evt)
        {
            _reportDataAccessor.AddBookInventory(evt.BookId, evt.AggregateId, BookInventoryStatus.InStore, evt.Notes);
            _reportDataAccessor.Commit();
            _commandTracker.DirectFinish(evt.CommandUniqueId);
        }

        public Task HandleAsync(BookInventoryCreatedEvent evt)
        {
            return Task.Factory.StartNew(() =>
            {
                Handle(evt);
            });
        }
    }
}