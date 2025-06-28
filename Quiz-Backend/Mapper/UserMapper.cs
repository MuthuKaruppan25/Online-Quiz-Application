

using Quiz.Models;

public class UserMapper
{
    public User MaptoUser(UserRegisterDto userRegisterDto)
    {
        User user = new();
        user.guid = Guid.NewGuid();
        user.Username = userRegisterDto.Username;
        user.Password = userRegisterDto.Password;
        user.Role = userRegisterDto.Role;
        return user;
    }
}


public class AdminMapper
{
    public Admin MaptoAdmin(UserRegisterDto userRegisterDto)
    {
        Admin user = new();
        user.guid = Guid.NewGuid();
        user.FirstName = userRegisterDto.FirstName;
        user.LastName = userRegisterDto.LastName;

        return user;
    }
}


public class AttenderMapper
{
    public Attender MaptoAttender(UserRegisterDto userRegisterDto)
    {
        Attender user = new();
        user.guid = Guid.NewGuid();
        user.FirstName = userRegisterDto.FirstName;
        user.LastName = userRegisterDto.LastName;

        return user;
    }
}

