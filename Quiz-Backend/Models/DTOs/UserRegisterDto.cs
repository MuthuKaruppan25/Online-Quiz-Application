

using System.ComponentModel.DataAnnotations;

public class UserRegisterDto
{
    [EmailValidation]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [TextValidator]
    public string FirstName { get; set; } = string.Empty;
    [TextValidator]
    public string LastName { get; set; } = string.Empty;
    [TextValidator]
    public string Role { get; set; } = string.Empty;
}