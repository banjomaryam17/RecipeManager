namespace RecipeManager.API.Data;

using Microsoft.EntityFrameworkCore;
using RecipeManager.API.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite key for the join table
        modelBuilder.Entity<RecipeIngredient>()
            .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

        // Seed data
        modelBuilder.Entity<User>().HasData(
            new User { 
                Id = 1, 
                Username = "admin", 
                PasswordHash = "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy" 
            }
        );

        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 1, Name = "Flour" },
            new Ingredient { Id = 2, Name = "Eggs" },
            new Ingredient { Id = 3, Name = "Milk" },
            new Ingredient { Id = 4, Name = "Butter" },
            new Ingredient { Id = 5, Name = "Sugar" }
        );

        modelBuilder.Entity<Recipe>().HasData(
            new Recipe { Id = 1, Title = "Pancakes", Description = "Fluffy pancakes", UserId = 1 },
            new Recipe { Id = 2, Title = "Omelette", Description = "Simple egg omelette", UserId = 1 }
        );

        modelBuilder.Entity<RecipeIngredient>().HasData(
            new RecipeIngredient { RecipeId = 1, IngredientId = 1, Quantity = "200g" },
            new RecipeIngredient { RecipeId = 1, IngredientId = 2, Quantity = "2" },
            new RecipeIngredient { RecipeId = 1, IngredientId = 3, Quantity = "300ml" },
            new RecipeIngredient { RecipeId = 2, IngredientId = 2, Quantity = "3" },
            new RecipeIngredient { RecipeId = 2, IngredientId = 4, Quantity = "10g" }
        );
    }
}