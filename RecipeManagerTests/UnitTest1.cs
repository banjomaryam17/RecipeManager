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
        // Arrange - set up the test
        var context = GetInMemoryContext();
        context.Users.Add(new User { Id = 1, Username = "testuser", PasswordHash = "hash" });
        context.Recipes.Add(new Recipe { Id = 1, Title = "Pancakes", Description = "Fluffy", UserId = 1 });
        context.Recipes.Add(new Recipe { Id = 2, Title = "Omelette", Description = "Eggy", UserId = 1 });
        await context.SaveChangesAsync();

        var repo = new RecipeRepository(context);

        // Act - run the thing we're testing
        var result = await repo.GetAllAsync();

        // Assert - check the result is what we expect
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetById_ReturnsCorrectRecipe()
    {
        // Arrange
        var context = GetInMemoryContext();
        context.Users.Add(new User { Id = 1, Username = "testuser", PasswordHash = "hash" });
        context.Recipes.Add(new Recipe { Id = 1, Title = "Pancakes", Description = "Fluffy", UserId = 1 });
        await context.SaveChangesAsync();

        var repo = new RecipeRepository(context);

        // Act
        var result = await repo.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Pancakes", result.Title);
    }

    [Fact]
    public async Task GetById_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var context = GetInMemoryContext();
        var repo = new RecipeRepository(context);

        // Act
        var result = await repo.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddRecipe_IncreasesCount()
    {
        // Arrange
        var context = GetInMemoryContext();
        context.Users.Add(new User { Id = 1, Username = "testuser", PasswordHash = "hash" });
        await context.SaveChangesAsync();

        var repo = new RecipeRepository(context);
        var newRecipe = new Recipe { Title = "New Recipe", Description = "Test", UserId = 1 };

        // Act
        await repo.AddAsync(newRecipe);
        var all = await repo.GetAllAsync();

        // Assert
        Assert.Single(all);
    }

    [Fact]
    public async Task DeleteRecipe_RemovesFromDatabase()
    {
        // Arrange
        var context = GetInMemoryContext();
        context.Users.Add(new User { Id = 1, Username = "testuser", PasswordHash = "hash" });
        context.Recipes.Add(new Recipe { Id = 1, Title = "Pancakes", Description = "Fluffy", UserId = 1 });
        await context.SaveChangesAsync();

        var repo = new RecipeRepository(context);

        // Act
        await repo.DeleteAsync(1);
        var result = await repo.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }
}