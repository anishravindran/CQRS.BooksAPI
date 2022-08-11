using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using CQRS.BooksAPI.Data;
using CQRS.BooksAPI.Features.Command;
using CQRS.BooksAPI.Features.Queries;
using CQRS.BooksAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.BooksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public BooksController(AppDbContext context , IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet(Name = "GetBooks")]        
        public async Task<IEnumerable<Book>> GetBooks()
        {
            //var books = _context.Books;
            //return Ok(books);
            return await _mediator.Send(new GetBooks.Query());
        }     

        [HttpGet("{id}" , Name = "GetBookById")]        
        public async Task<Book> GetBookById(int id)
        {           
            return await _mediator.Send(new GetBookByID.Query(){Id = id});
        }     


        [HttpPost]
        public async Task<ActionResult> CreateBook([FromBody] AddBooks.Command command)
        {
            var bookId =  await _mediator.Send(command);        
            Console.WriteLine($"--> Book created Id : {bookId}"); 
            //The new url location will be passed back to header - location         
            return CreatedAtAction(nameof(GetBookById), new {id = bookId}, null);
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult> RemoveBook(int id)
        {            
            if (await _mediator.Send(new DeleteBook.Command(){Id = id}) > 0)
            {
                return Ok("Success");
            }else{
                return NotFound();
            }
        }
        
    }
}
