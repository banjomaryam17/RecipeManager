using RecipeManager.API.Models;

namespace RecipeManager.API.Repositories;

public interface IIngredientRepository
{
    Task<IEnumerable<Ingredient>> GetAllAsync();
    Task<Ingredient?> GetByIdAsync(int id);
    Task AddAsync(Ingredient ingredient);
    Task DeleteAsync(int id);
}