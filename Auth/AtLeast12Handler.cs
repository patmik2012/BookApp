using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookApp.Auth
{
    public class AtLeast12Handler : AuthorizationHandler<AtLeast12Requirement>
    {
        private readonly IBookService _bookService;

        public AtLeast12Handler(IBookService bookService)
        {
            _bookService = bookService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            AtLeast12Requirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth &&
                                c.Issuer == Token.Issuer))
            {
                return Task.FromResult(0);
            }

            if (!context.TryGetParamValue<int>("Id", out var Id))
            {
                return Task.FromResult(0);
            }

            Book book = _bookService.Get(Id);
            var atLeast12Book = book.AgeLimit > requirement.RequiredMinimumAge;

            var dateOfBirth = Convert.ToDateTime(
                context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth &&
                                            c.Issuer == Token.Issuer).Value);

            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            if (!atLeast12Book || calculatedAge >= requirement.RequiredMinimumAge)
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);

        }
    }
}