
namespace RecipeManager.API.DTOs;

public class CreateRecipeDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<int> IngredientIds { get; set; } = new();
}