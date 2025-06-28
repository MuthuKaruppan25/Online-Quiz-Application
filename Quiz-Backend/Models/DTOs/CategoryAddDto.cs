
using System.ComponentModel.DataAnnotations;

public class CategoryAddDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}