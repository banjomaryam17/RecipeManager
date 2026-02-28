using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeManager.API.DTOs;
using RecipeManager.API.Models;
using RecipeManager.API.Repositories;

namespace RecipeManager.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeRepository _repo;

    public RecipesController(IRecipeRepository repo)
    {
        _repo = repo;
    }

    // GET api/recipes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var recipes = await _repo.GetAllAsync();
        var result = recipes.Select(r => new RecipeDto
        {
            Id = r.Id,
            Title = r.Title,
            Description = r.Description,
            CreatedByUsername = r.User?.Username ?? "Unknown",
            Ingredients = r.RecipeIngredients.Select(ri => ri.Ingredient.Name).ToList()
        });
        return Ok(result);
    }

    // GET api/recipes/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var recipe = await _repo.GetByIdAsync(id);
        if (recipe == null) return NotFound();

        var result = new RecipeDto
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Description = recipe.Description,
            CreatedByUsername = recipe.User?.Username ?? "Unknown",
            Ingredients = recipe.RecipeIngredients.Select(ri => ri.Ingredient.Name).ToList()
        };
        return Ok(result);
    }

    // POST api/recipes
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRecipeDto dto)
    {
        var recipe = new Recipe
        {
            Title = dto.Title,
            Description = dto.Description,
            UserId = 1, // hardcoded for now, will come from JWT later
            RecipeIngredients = dto.IngredientIds.Select(id => new RecipeIngredient
            {
                IngredientId = id
            }).ToList()
        };

        await _repo.AddAsync(recipe);
        return CreatedAtAction(nameof(GetById), new { id = recipe.Id }, new RecipeDto
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Description = recipe.Description,
            CreatedByUsername = "testuser",
            Ingredients = new List<string>()
        });
    }

    // PUT api/recipes/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateRecipeDto dto)
    {
        var recipe = await _repo.GetByIdAsync(id);
        if (recipe == null) return NotFound();

        recipe.Title = dto.Title;
        recipe.Description = dto.Description;

        await _repo.UpdateAsync(recipe);
        return NoContent();
    }

    // DELETE api/recipes/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}