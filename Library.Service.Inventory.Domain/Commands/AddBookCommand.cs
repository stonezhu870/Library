using Library.Domain.Core.Commands;
using System;

namespace Library.Service.Inventory.Domain.Commands
{
    public class AddBookCommand : CommonCommand
    {
        private static string AddBookCommandKey = "Command_AddBook";

        public AddBookCommand() : base(AddBookCommandKey)
        {
        }

        public Guid BookId { get; set; }

        public string BookName { get; set; }

        public string ISBN { get; set; }

        public DateTime DateIssued { get; set; }

        public string Description { get; set; }
    }
}