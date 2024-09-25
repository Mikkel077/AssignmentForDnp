using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string _filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
        CreateData();
    }

    public async Task<Post> AddAsync(Post post)
    {
        var posts = await OpenJSonFileAsync();

        var maxId = posts.Count > 0 ? posts.Max(p => p.Id) + 1 : 1;
        post.Id = maxId;
        posts.Add(post);

        await CloseJSonFileAsync(posts);
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        var posts = await OpenJSonFileAsync();

        var existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost == null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{post.Id}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(existingPost);

        await CloseJSonFileAsync(posts);
    }

    public async Task DeleteAsync(int id)
    {
        var posts = await OpenJSonFileAsync();

        var post = posts.SingleOrDefault(p => p.Id == id);
        if (post == null)
        {
            throw new InvalidOperationException("Comment with ID '" + id + "' not found");
        }

        posts.Remove(post);

        await CloseJSonFileAsync(posts);
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        var posts = await OpenJSonFileAsync();
        var post = posts.SingleOrDefault(p => p.Id == id);
        if (post == null)
        {
            throw new InvalidOperationException("Comment with ID '" + id + "' not found");
        }

        await CloseJSonFileAsync(posts);
        return post;
    }

    public IQueryable<Post> GetManyAsync()
    {
        var posts = OpenJSonFileAsync().Result;
        _ = CloseJSonFileAsync(posts);
        return posts.AsQueryable();
    }

    public async Task<bool> CheckIfPostExistsAsync(int id)
    {
        var posts = await OpenJSonFileAsync();
        bool postExists = posts.Any(p => p.Id == id);
        await CloseJSonFileAsync(posts);
        return postExists;
    }

    private async Task<List<Post>> OpenJSonFileAsync()
    {
        var postsAsJSon = await File.ReadAllTextAsync(_filePath);
        var posts = JsonSerializer.Deserialize<List<Post>>(postsAsJSon)!;
        return posts;
    }

    private async Task CloseJSonFileAsync(List<Post> posts)
    {
        var postsAsJSon = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(_filePath, postsAsJSon);
    }

    private void CreateData()
    {
        var posts = OpenJSonFileAsync();
        Post post1 = new Post(1, "Comment", "Hi!!", 1);
        Post post2 = new Post(2, "Idk", "Hi!!!!!!", 2);
        Post post3 = new Post(3, "idc", "HiHi!!", 3);
        List<Post> postToJSon =
        [
            post1,
            post2,
            post3
        ];
        _ = CloseJSonFileAsync(postToJSon);
    }
}