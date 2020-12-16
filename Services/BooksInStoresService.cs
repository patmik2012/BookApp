using BookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookApp.Services
{
    public interface IBooksInStoresService
    {
        void Log(string message);
        Task<IEnumerable<BooksInStores>> GetAll();
        BooksInStores Create(BooksInStores newBooksInStoresService);

    };
    public class BooksInStoresService : AbstractService, IBooksInStoresService
    {
        private readonly ILogger<BooksInStoresService> _logger;
        public BooksInStoresService(IUnitOfWork unitOfWork, ILogger<BooksInStoresService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }
        public void Log(string message)
        {
            _logger.LogInformation("BooksInStoresService log: " + message);
        }

        //api/BooksInStores/getall
        public async Task<IEnumerable<BooksInStores>> GetAll()
        {
            Log("GetAll");
            return UnitOfWork.GetRepository<BooksInStores>()
                .GetAll();
        }
        //api/BooksInStores/create
        public BooksInStores Create(BooksInStores newBooksInStores)
        {
            Log("Create");
            newBooksInStores.Id = 0;
            UnitOfWork.GetRepository<BooksInStores>().Create(newBooksInStores);
            UnitOfWork.SaveChanges();
            return newBooksInStores;
        }

    }
}
