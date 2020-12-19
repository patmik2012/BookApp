using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class BooksInStoresController : ControllerBase
    {

        private readonly IBooksInStoresService _booksInStoresService;
        public BooksInStoresController(IBooksInStoresService booksInStoresService)
        {
            _booksInStoresService = booksInStoresService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BooksInStores>>> GetAll()
        {
            var result = await _booksInStoresService.GetAll();
            return Ok(result);
        }
        [HttpPost]
//        [AllowAnonymous]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([FromBody] BooksInStores newBooksInStores)
        {
            var BooksInStores = _booksInStoresService.Create(newBooksInStores);
            return Created($"{BooksInStores.Id}", BooksInStores);
        }
    }
}
