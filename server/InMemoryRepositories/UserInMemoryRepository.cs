using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> users;

    public UserInMemoryRepository()
    {
        this.users = new List<User>();

        createData();
    }


    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any()
            ? users.Max(u => u.Id) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{user.Id}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }


    public Task<User> GetSingleAsync(int id)
    {
        User? userToReturn = users.Find(u => u.Id == id);
        if (userToReturn is null)
        {
            throw new InvalidOperationException(
                $"User with {id} was not found"
            );
        }

        return Task.FromResult(userToReturn);
    }


    public IQueryable<User> GetManyAsync()
    {
        return users.AsQueryable();
    }

    public void createData()
    {
    }
}