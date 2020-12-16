using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookApp.DBContext;
using BookApp.Models;
using BookApp.Services;

namespace BookApp.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _booksService;


        public BooksController(IBookService bookService)
        {
            _booksService = bookService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            var result = await _booksService.GetAll();
            return Ok(result);
        }

        [HttpGet("{BkId}")]
        public ActionResult<IEnumerable<Book>> Get(int BkId)
        {
            return Ok(_booksService.Get(BkId));
        }
        [HttpGet("{authorId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllByAuthor(int authorID)
        {
            var result = await _booksService.GetAllByAuthor(authorID);
            return Ok(result);
        }
        [HttpGet("{title}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetByTitle(string title)
        {
            var result = await _booksService.GetByTitle(title);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            var book = _booksService.Create(newBook);
            return Created($"{book.Id}", book);
        }

        [HttpPut]
        public IActionResult Update(int BkId, [FromBody] Book updatedBook) 
        {
            return Ok(_booksService.Update(BkId, updatedBook));
        }

        [HttpDelete]
        public IActionResult Delete(int BkId)
        {
            return Ok(_booksService.DeleteAsync(BkId));
        }

        [HttpGet("{year}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllByPublishedYear(int year)
        {
            var result = await _booksService.GetAllByPublishedYear(year);
            return Ok(result);
        }


        /*============================
        //InMemory DB
        private List<Book> _books; //In-Memory

        public BooksController()
        {
            _books = CreateExampleBooks();
        }
        

        //GET /api/books/getall
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            return Ok(_books);
        }

        //GET api/books/get/1
        [HttpGet("{Id}")]
        public ActionResult<Book> Get(int Id)
        {
            var book = _books.FirstOrDefault(b => b.Id == Id);
            if (book != null)
            {
                return Ok(book);
            }
            return StatusCode(500);
        }

        //POST api/books/create
        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            _books.Add(newBook);
            return Created($"{newBook.Id}", newBook);
        }

        //PUT /api/books/update/1
        [HttpPut("{BkId}")]
        public IActionResult Update(int BkId, [FromBody] Book updatedBook)
        {
            var book = _books.FirstOrDefault(b => b.Id == BkId);
            if (book!=null)
            {
                book = updatedBook;
                book.Id = BkId;
                return Ok(_books);
            }
            return StatusCode(500);
        }

        //DELETE api/books/delete/1
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _books.RemoveAll(b => b.Id == Id);
            return Ok(_books);
        }

        private List<Book> CreateExampleBooks()
        {
            var books = new List<Book>();

            books.Add(new Book
            {
                Id=1,
                Title = "Első könyv címe",
                //Author = "",
                AuthorId = 1,
                PublishedYear = 2000,
                PageNumber = 134,
                ISBN = "12414134"
            });

            books.Add(new Book
            {
                Id=2,
                Title = "Második könyv címe",
                //Author="",
                AuthorId = 2,
                PublishedYear = 2002,
                PageNumber = 234,
                ISBN = "432432"
            });

            return books;
        }
        */
    }
}


