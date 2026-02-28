namespace RecipeManager.API.DTOs;


public class RecipeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CreatedByUsername { get; set; } = string.Empty;
    public List<string> Ingredients { get; set; } = new();
}