﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BookApp.DBContext
{
    public class BookDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    //: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookStore> BookStores { get; set; }
        public DbSet<BooksInStores> BooksInStores { get; set; }
        public DbSet <ApplicationUser> ApplicationUsers { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(a => a.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<BooksInStores>()
                .HasKey(bis => new { bis.BookId, bis.BookStoreId });

            modelBuilder.Entity<BooksInStores>()
                .HasOne<Book>(BooksInStores => BooksInStores.Book)
                .WithMany(b => b.BooksInStores)
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<BooksInStores>()
                .HasOne<BookStore>(BooksInStores => BooksInStores.bookStore)
                .WithMany(b => b.BooksInStores)
                .HasForeignKey(b => b.BookStoreId);

            modelBuilder.Entity<ApplicationUser>().HasKey(appuser => appuser.Id);

            base.OnModelCreating(modelBuilder);

        }
    }

    

}
