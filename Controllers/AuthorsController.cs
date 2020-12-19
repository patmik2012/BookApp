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
        
        //api/authors/getall
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Author>>> GetAll()
        {
            var result = await _authorsService.GetAll();
            return Ok(result);
        }

        //api/authors/get/1
        [HttpGet("{AuthorId}")]
//        [AllowAnonymous]
        [Authorize(Roles = "Administrator, User")]
        public ActionResult<IEnumerable<Author>> Get(int AuthorId)
        {
            return Ok(_authorsService.Get(AuthorId));
        }

        //api/authors/create
        [HttpPost]
//        [AllowAnonymous]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([FromBody] Author newAuthor)
        {
            var author = _authorsService.Create(newAuthor);
            return Created($"{author.Id}", author);
        }

        //api/authors/update
        [HttpPut]
//        [AllowAnonymous]
        [Authorize(Roles = "Administrator")]
        public IActionResult Update(int AuthorId, [FromBody] Author updatedAuthor)
        {
            return Ok(_authorsService.Update(AuthorId, updatedAuthor));
        }

        //api/authors/GetAllByName/testauthor
        [HttpGet("{authorName}")]
//        [AllowAnonymous]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllByNameAsync(string authorName)
        {
            var result = await _authorsService.GetAllByNameAsync(authorName);
            return Ok(result);
        }

        //api/authors/delete/4
        [HttpDelete("{authorId}")]
//        [AllowAnonymous]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int authorId)
        {
            return Ok(_authorsService.DeleteAsync(authorId));
        }
    }
}
