namespace Entities;

public class User
{
    public int Id { get; set; }
    public String Username { get; set; }
    public String Password { get; set; }

    public User(int id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }
}