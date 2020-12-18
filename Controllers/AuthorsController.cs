using BookApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookApp.DBContext;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class AuthorsController : ControllerBase
    {


        private readonly IAuthorService _authorsService;


        public AuthorsController(IAuthorService authorService)
        {
            _authorsService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAll()
        {
            var result = await _authorsService.GetAll();
            return Ok(result);
        }

        [HttpGet("{AuthorId}")]
        public ActionResult<IEnumerable<Author>> Get(int AuthorId)
        {
            return Ok(_authorsService.Get(AuthorId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Author newAuthor)
        {
            var author = _authorsService.Create(newAuthor);
            return Created($"{author.Id}", author);
        }

        [HttpPut]
        public IActionResult Update(int AuthorId, [FromBody] Author updatedAuthor)
        {
            return Ok(_authorsService.Update(AuthorId, updatedAuthor));
        }

        [HttpGet("{authorName}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllByNameAsync(string authorName)
        {
            var result = await _authorsService.GetAllByNameAsync(authorName);
            return Ok(result);
        }
    }
}
