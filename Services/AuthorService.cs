using BookApp.Models;
using BookApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Services
{
    public interface IAuthorService
    {
        void Log(string message);
        Task<IEnumerable<Author>> GetAll();
        Author Get(int authorId);
        Author Create(Author newAuthor);
        Author Update(int authorId, Author updatedAuthor);
        Task<Author> GetAllByNameAsync(string authorName);
        Task<IEnumerable<Author>> DeleteAsync(int authorId);


    };
    public class AuthorService : AbstractService, IAuthorService
    {
        private readonly ILogger<AuthorService> _logger;
        public AuthorService(IUnitOfWork unitOfWork, ILogger<AuthorService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }
        public void Log(string message)
        {
            _logger.LogInformation("AuthorService log: " + message);
        }

        //api/authors/getall
        public async Task<IEnumerable<Author>> GetAll()
        {
            Log("GetAll");
            return UnitOfWork.GetRepository<Author>()
                .GetAll().Where(a => !a.Deleted);
        }
        //api/authors/get/1
        public Author Get(int authorId)
        {
            Log("Get(" + authorId + ")");
            return UnitOfWork.GetRepository<Author>()
                .GetAsQueryable(a => a.Id == authorId && !a.Deleted).FirstOrDefault();
        }

        //api/authors/create
        public Author Create(Author newAuthor)
        {
            Log("Create");
            newAuthor.Id = 0;
            UnitOfWork.GetRepository<Author>().Create(newAuthor);
            UnitOfWork.SaveChanges();
            return newAuthor;
        }

        //api/authors/update
        public Author Update(int authorId, Author updatedAuthor)
        {
            Log("Update (" + authorId + ")");
            UnitOfWork.GetRepository<Author>().Update(authorId, updatedAuthor);
            UnitOfWork.SaveChanges();
            return updatedAuthor;
        }

        //api/authors/testauthor
        public async Task<Author> GetAllByNameAsync(string authorName)
        {
            Log("GetAllByName");
            return await UnitOfWork.GetRepository<Author>().GetAsQueryable()
                .Include(author => author.Books)
                .FirstOrDefaultAsync(author => author.Name == authorName);
        }

        //api/authors/delete/4
        public async Task<IEnumerable<Author>> DeleteAsync(int authorId)
        {
            Log("Delete(" + authorId + ")");
            var author = UnitOfWork.GetRepository<Author>()
                .GetAsQueryable(a => a.Id == authorId).FirstOrDefault();
            author.Deleted = true;
            UnitOfWork.SaveChanges();
            return await GetAll();
        }
    }
}
