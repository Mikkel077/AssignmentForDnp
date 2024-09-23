using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts;

    public PostInMemoryRepository()
    {
        this.posts = new List<Post>();
        createData();
    }


    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.Id}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? postToReturn = posts.Find(p => p.Id == id);
        if (postToReturn is null)
        {
            throw new InvalidOperationException(
                $"Post with {id} was not found"
            );
        }

        return Task.FromResult(postToReturn);
    }


    public IQueryable<Post> GetManyAsync()
    {
        return posts.AsQueryable();
    }

    public Task<bool> CheckIfPostExistsAsync(int id)
    {
        bool postExists = posts.Any(p => p.Id == id);
        return Task.FromResult(postExists);
    }


    public void createData()
    {
        Post post1 = new Post(1, "Comment", "Hi!!", 1);
        Post post2 = new Post(2, "Idk", "Hi!!!!!!", 2);
        Post post3 = new Post(3, "idc", "HiHi!!", 3);

        posts.Add(post1);
        posts.Add(post2);
        posts.Add(post3);
    }
}