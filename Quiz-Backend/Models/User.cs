
namespace Quiz.Models;

public class User
{
    public Guid guid { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public Admin? admin { get; set; }
    public Attender? attender { get; set; }
}