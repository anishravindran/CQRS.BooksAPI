using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CQRS.BooksAPI.Data;
using CQRS.BooksAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.BooksAPI.Features.Queries
{
    public class GetBooks
    {
        public class Query : IRequest<IEnumerable<Book>>
        {
            
        }
        public class QueryHandler : IRequestHandler<Query, IEnumerable<Book>>
        {
            private readonly AppDbContext _context;

            public QueryHandler(AppDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Book>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Books.ToListAsync(cancellationToken);
            }
        }

    }
}
