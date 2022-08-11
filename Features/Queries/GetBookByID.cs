using System;
using System.Threading;
using System.Threading.Tasks;
using CQRS.BooksAPI.Data;
using CQRS.BooksAPI.Models;
using MediatR;

namespace CQRS.BooksAPI.Features.Queries
{
    public class GetBookByID 
    {
        public class Query: IRequest<Book>
        {
            public int Id { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Book>
        {
            private readonly AppDbContext _context;

            public QueryHandler(AppDbContext context)
            {
                _context = context;         
            }

            public async Task<Book> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Books.FindAsync(request.Id);
            }
        }

    }
}
