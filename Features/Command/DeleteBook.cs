using System;
using System.Threading;
using System.Threading.Tasks;
using CQRS.BooksAPI.Data;
using CQRS.BooksAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.BooksAPI.Features.Command
{
    public class DeleteBook 
    {
        public class Command : IRequest<int>
        {
            public int Id { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command , int>
        {
            private readonly AppDbContext _context;

            public CommandHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var book = _context.Books.Find(request.Id);
                if (book != null)
                {
                    _context.Books.Remove(book);
                    return await _context.SaveChangesAsync();
                }else{
                    return -1;
                }
            }
        }
    }
}
