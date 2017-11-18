using Library.Domain.Core;
using Library.Domain.Core.Messaging;
using Library.Service.Inventory.Domain.DataAccessors;
using Library.Service.Inventory.Domain.DTOs;
using Library.Service.Inventory.Domain.Events;
using System.Threading.Tasks;

namespace Library.Service.Inventory.Domain.EventHandlers
{
    public class BookAddedEventHandler : IEventHandler<BookAddedEvent>
    {
        private IInventoryReportDataAccessor _reportDataAccessor = null;
        private ICommandTracker _commandTracker = null;

        public BookAddedEventHandler(IInventoryReportDataAccessor reportDataAccessor, ICommandTracker commandTracker)
        {
            _reportDataAccessor = reportDataAccessor;
            _commandTracker = commandTracker;
        }

        public void Handle(BookAddedEvent evt)
        {
            _reportDataAccessor.AddBook(new AddBookDTO
            {
                BookId = evt.AggregateId,
                BookName = evt.BookName,
                Description = evt.Description,
                ISBN = evt.ISBN,
                DateIssued = evt.DateIssued
            });

            _reportDataAccessor.Commit();
            _commandTracker.DirectFinish(evt.CommandUniqueId);
        }

        public Task HandleAsync(BookAddedEvent evt)
        {
            return Task.Factory.StartNew(() =>
            {
                Handle(evt);
            });
        }
    }
}