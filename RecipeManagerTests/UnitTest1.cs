using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeManager.API.Controllers;
using RecipeManager.API.Data;
using RecipeManager.API.DTOs;
using RecipeManager.API.Models;
using RecipeManager.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace RecipeManagerTests;

public class RecipeTests
{
    // Helper method to create a fresh in-memory database for each test
    private AppDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetAll_ReturnsAllRecipes()
    {
        var context = GetInMemoryContext();
        context.Users.Add(new User { Id = 1, Username = "testuser", PasswordHash = "hash" });
        context.Recipes.Add(new Recipe { Id = 1, Title = "Pancakes", Description = "Fluffy", UserId = 1 });
        context.Recipes.Add(new Recipe { Id = 2, Title = "Omelette", Description = "Eggy", UserId = 1 });
        await context.SaveChangesAsync();

        var repo = new RecipeRepository(context);
        var result = await repo.GetAllAsync();
        Assert.Equal(2, result.Count());
    }
    
    [Fact]
    public async Task GetById_ReturnsNull_WhenNotFound()
    {
        var context = GetInMemoryContext();
        var repo = new RecipeRepository(context);
        
        var result = await repo.GetByIdAsync(999);
        
        Assert.Null(result);
    }
    [Fact]
    public async Task GetById_ReturnsCorrectRecipe()
    {
        var context = GetInMemoryContext();
        context.Users.Add(new User { Id = 1, Username = "testuser", PasswordHash = "hash" });
        context.Recipes.Add(new Recipe { Id = 1, Title = "Pancakes", Description = "Fluffy", UserId = 1 });
        await context.SaveChangesAsync();
        var repo = new RecipeRepository(context);
        var result = await repo.GetByIdAsync(1);
        Assert.NotNull(result);
        Assert.Equal("Pancakes", result.Title);
    }

    [Fact]
    public async Task DeleteRecipe_RemovesFromDatabase()
    {
        var context = GetInMemoryContext();
        context.Users.Add(new User { Id = 1, Username = "testuser", PasswordHash = "hash" });
        context.Recipes.Add(new Recipe { Id = 1, Title = "Pancakes", Description = "Fluffy", UserId = 1 });
        await context.SaveChangesAsync();

        var repo = new RecipeRepository(context);
        
        await repo.DeleteAsync(1);
        var result = await repo.GetByIdAsync(1);
        Assert.Null(result);
    }
    
    [Fact]
    public async Task AddRecipe_IncreasesCount()
    {
        var context = GetInMemoryContext();
        context.Users.Add(new User { Id = 1, Username = "testuser", PasswordHash = "hash" });
        await context.SaveChangesAsync();

        var repo = new RecipeRepository(context);
        var newRecipe = new Recipe { Title = "New Recipe", Description = "Test", UserId = 1 };
        
        await repo.AddAsync(newRecipe);
        var all = await repo.GetAllAsync();
        
        Assert.Single(all);
    }
}