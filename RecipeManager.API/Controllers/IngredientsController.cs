using Microsoft.AspNetCore.Mvc;
using RecipeManager.API.DTOs;
using RecipeManager.API.Models;
using RecipeManager.API.Repositories;

namespace RecipeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IIngredientRepository _repo;

    public IngredientsController(IIngredientRepository repo)
    {
        _repo = repo;
    }

    // GET api/ingredients
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ingredients = await _repo.GetAllAsync();
        var result = ingredients.Select(i => new IngredientDto
        {
            Id = i.Id,
            Name = i.Name
        });
        return Ok(result);
    }

    // GET api/ingredients/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ingredient = await _repo.GetByIdAsync(id);
        if (ingredient == null) return NotFound();

        return Ok(new IngredientDto { Id = ingredient.Id, Name = ingredient.Name });
    }

    // POST api/ingredients
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateIngredientDto dto)
    {
        var ingredient = new Ingredient { Name = dto.Name };
        await _repo.AddAsync(ingredient);
        return CreatedAtAction(nameof(GetById), new { id = ingredient.Id }, ingredient);
    }

    // DELETE api/ingredients/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}