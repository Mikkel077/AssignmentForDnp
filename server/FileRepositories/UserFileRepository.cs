using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    
    private readonly string _filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
        CreateData();
    }
    public async Task<User> AddAsync(User user)
    {
        var users = await OpenJSonFileAsync();

        var maxId = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
        user.Id = maxId;
        users.Add(user);

        await CloseJSonFileAsync(users);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        var users = await OpenJSonFileAsync();

        var existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser == null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{user.Id}' not found");
        }

        users.Remove(existingUser);
        users.Add(existingUser);

        await CloseJSonFileAsync(users);
    }

    public async Task DeleteAsync(int id)
    {
        var users = await OpenJSonFileAsync();

        var user = users.SingleOrDefault(u => u.Id == id);
        if (user == null)
        {
            throw new InvalidOperationException("Comment with ID '" + id + "' not found");
        }

        users.Remove(user);

        await CloseJSonFileAsync(users);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        var users = await OpenJSonFileAsync();
        var user = users.SingleOrDefault(u => u.Id == id);
        if (user == null)
        {
            throw new InvalidOperationException("Comment with ID '" + id + "' not found");
        }

        await CloseJSonFileAsync(users);
        return user;
    }

    public IQueryable<User> GetManyAsync()
    {
        var users = OpenJSonFileAsync().Result;
        _ = CloseJSonFileAsync(users);
        return users.AsQueryable();
    }
    
    private async Task<List<User>> OpenJSonFileAsync()
    {
        var usersAsJSon = await File.ReadAllTextAsync(_filePath);
        var users = JsonSerializer.Deserialize<List<User>>(usersAsJSon)!;
        return users;
    }

    private async Task CloseJSonFileAsync(List<User> users)
    {
        var usersAsJsOn = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(_filePath, usersAsJsOn);
    }

    private void CreateData()
    {
        var posts = OpenJSonFileAsync();
        User user1 = new User(1, "mikkel1", "hejhej");
        User user2 = new User(2, "mikkel2", "hejhej");
        User user3 = new User(3, "mikkel3", "hejhej");

        List<User> users = new List<User> { user1, user2, user3 };
        _ = CloseJSonFileAsync(users);
    }
}