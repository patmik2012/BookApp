using BookApp.DBContext;
using BookApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApp.UnitOfWork;
using BookApp.Auth;

namespace BookApp.Services
{

    public interface IBookService
    {
        void Log(string message);
        //List<Book> CreateExampleBooks();

        Task<IEnumerable<Book>> GetAll();
        Task<IEnumerable<Book>> GetAllByAuthor(int authorId);
        Book Get(int id);
        Task<IEnumerable<Book>> GetByTitle(string ttl);
        Book Create(Book newBook);
        Book Update(int BkId, Book updatedBook);
        Task<IEnumerable<Book>> DeleteAsync(int BkId);
        Task<IEnumerable<Book>> GetAllByPublishedYear(int year);
        Task<IEnumerable<Book>> GetAllByAgeLimit();
    }

    public class BookService : AbstractService, IBookService
    {
        private readonly ILogger<BookService> _logger;
        //public List<Book> _books;
        //private readonly BookDbContext _bookDbContext;
        //        public BookService(IUnitOfWork unitOfWork, ILogger<BookService> logger, BookDbContext bookDbContext)
        public BookService(IUnitOfWork unitOfWork, ILogger<BookService> logger) : base(unitOfWork)
        {
            _logger = logger;
            //_books = CreateExampleBooks();
            //_bookDbContext = bookDbContext;
        }

        public void Log(string message)
        {
            _logger.LogInformation("BookService log: " + message);
        }
        /*
        public List<Book> CreateExampleBooks()
        {
            var books = new List<Book>();

            books.Add(new Book
            {
                Id = 1,
                Title = "Első könyv címe",
                //Author = "",
                AuthorId = 1,
                PublishedYear = 2000,
                PageNumber = 134,
                ISBN = "12414134"
            });

            books.Add(new Book
            {
                Id = 2,
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


        //api/books/getall
        public async Task<IEnumerable<Book>> GetAll()
        {
            Log("GetAll");
            //return _books;
            //return await _bookDbContext.Books.ToListAsync();
            return UnitOfWork.GetRepository<Book>().GetAll();
        }


        //api/books/get/5
        public Book Get(int id)
        {
            Log("Get("+id+")");
            //return _books.FirstOrDefault(b => b.Id == id);
            //return _bookDbContext.Books.FirstOrDefault(b => b.Id == id);
            return UnitOfWork.GetRepository<Book>()
                .GetByIdWithInclude(id, src => src.Include(bk => bk.Author));
        }

        //api/books/create
        public Book Create(Book newBook)
        {
            Log("Create");
            //newBook.Id = _books.Select(b => b.Id).Max() + 1;
            //_books.Add(newBook);
            newBook.Id = 0;
            //_bookDbContext.Books.Add(newBook);
            //_bookDbContext.SaveChanges();
            UnitOfWork.GetRepository<Book>().Create(newBook);
            UnitOfWork.SaveChanges();
            return newBook;
        }

        //api/books/update/10
        public Book Update(int BkId, Book updatedBook)
        {
            Log("Update (" + BkId + ")");
            //var book = _books.FirstOrDefault(b => b.Id == BkId);
            /*var book = _bookDbContext.Books.FirstOrDefault(b => b.Id == BkId);
            if (book!=null)
            {
                book = updatedBook;
                book.Id = BkId;
                _bookDbContext.Books.Update(book);
                _bookDbContext.SaveChanges();
            }
            return book;
            */
            UnitOfWork.GetRepository<Book>().Update(BkId, updatedBook);
            UnitOfWork.SaveChanges();
            return updatedBook;
        }
        /*
        public Task<IEnumerable<Book>> IBookService.Delete(int BkId)
        {
            Log("Delete(" + BkId + ")");
            //_books.RemoveAll(b => b.Id == BkId);
            //return _books;
            //var book = Get(BkId);
            //_bookDbContext.Books.Remove(book);
            //_bookDbContext.SaveChanges();
            //return _bookDbContext.Books;
        }
        */

        //api/books/GetByTitle/test
        public async Task<IEnumerable<Book>> GetByTitle(string ttl)
        {
            Log("GetByTitle(" + ttl + ")");
            //return await _bookDbContext.Books.Where(b => b.Title == title).ToListAsync();
            return UnitOfWork.GetRepository<Book>()
                .GetAsQueryable(b => b.Title == ttl,
                src => src.Include(book => book.Author),
                src => src.OrderBy(a => a.Title));
    
        }

        //api/books/GetByAuthor/1
        public async Task<IEnumerable<Book>> GetAllByAuthor(int authorId)
        {
            Log("GetAllByAuthor(" + authorId + ")");
            return UnitOfWork.GetRepository<Book>()
                .GetAsQueryable(b => b.AuthorId == authorId)
                .OrderBy(book => book.PublishedYear);
        }

        //api/books/GetAllByPublishedYear/2001
        public async Task<IEnumerable<Book>> GetAllByPublishedYear(int year)
        {
            Log("GetAllByPublishedYear(" + year + ")");
            return UnitOfWork.GetRepository<Book>()
                .GetAsQueryable(b => b.PublishedYear == year)
                .OrderBy(book => book.PublishedYear);
        }

        //api/books/delete/10
        public async Task<IEnumerable<Book>> DeleteAsync(int BkId)
        {
            Log("Delete(" + BkId + ")");
            var book = UnitOfWork.GetRepository<Book>()
                .GetByIdWithInclude(BkId, src => src.Include(bk => bk.Author));
            book.Deleted = true;
            //await UnitOfWork.GetRepository<Book>().Delete(BkId);            
            UnitOfWork.SaveChanges();
            return await GetAll();
        }

        public async Task<IEnumerable<Book>> GetAllByAgeLimit()
        {
            Log("GetAllByAgeLimit");
            return UnitOfWork.GetRepository<Book>()
                .GetAsQueryable(b => b.AgeLimit > Policies.AgeLimit);

        }
    }
}
