﻿using Microsoft.EntityFrameworkCore;
using TodoApp.DAL.Entities;
using TodoApp.DAL.InitialData;

namespace TodoApp.DAL
{
    public class TodoAppContext: DbContext
    {
        public TodoAppContext(DbContextOptions<TodoAppContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; } 
        public DbSet<TodoItem> TodoItems { get; set; } 
        public DbSet<Image> Images { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasData(UsersInitialDataSeed.Accounts);

            modelBuilder.Entity<TodoItem>()
                .HasData(TodoItemsInitialDataSeed.TodoItems);

            modelBuilder.Entity<Image>()
                .HasData(ImagesInitialDataSeed.Images);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.TodoItem)
                .WithMany(t => t.Images)
                .HasForeignKey(i => i.TodoItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
