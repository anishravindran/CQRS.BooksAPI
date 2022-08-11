using System;
using System.Threading;
using System.Threading.Tasks;
using CQRS.BooksAPI.Data;
using CQRS.BooksAPI.Models;
using MediatR;

namespace CQRS.BooksAPI.Features.Command
{
    public class AddBooks
    {
        public class Command : IRequest<int>
        {
            public string Name { get; set; }
            public string Author { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly AppDbContext _context;

            public CommandHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
               var book = new Book(){ 
                    Name = request.Name,
                    Author = request.Author
               };

                await _context.Books.AddAsync(book,cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return book.Id;
            }
        }
    }
}
