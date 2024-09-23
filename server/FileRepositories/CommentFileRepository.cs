using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string _filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }


    public async Task<Comment> AddAsync(Comment comment)
    {
        var comments = await OpenJSonFileAsync();

        var maxId = comments.Count > 0 ? comments.Max(c => c.Id) + 1 : 1;
        comment.Id = maxId;
        comments.Add(comment);

        await CloseJSonFileAsync(comments);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        var comments = await OpenJSonFileAsync();

        var existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment == null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.Id}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

        await CloseJSonFileAsync(comments);
    }

    public async Task DeleteAsync(int id)
    {
        var comments = await OpenJSonFileAsync();

        var comment = comments.SingleOrDefault(c => c.Id == id);
        if (comment == null)
        {
            throw new InvalidOperationException("Comment with ID '" + id + "' not found");
        }

        comments.Remove(comment);

        await CloseJSonFileAsync(comments);
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        var comments = await OpenJSonFileAsync();
        var comment = comments.SingleOrDefault(c => c.Id == id);
        if (comment == null)
        {
            throw new InvalidOperationException("Comment with ID '" + id + "' not found");
        }

        await CloseJSonFileAsync(comments);
        return comment;
    }

    public IQueryable<Comment> GetManyAsync()
    {
        string commentsAsJson = File.ReadAllTextAsync(_filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }

    public async Task<IQueryable<Comment>> FindCommentsForPostAsync(int id)
    {
        var comments = await OpenJSonFileAsync();
        var result = comments.Where(comment => comment.PostID == id).AsQueryable();
        await CloseJSonFileAsync(comments);
        return result;
    }

    private async Task<List<Comment>> OpenJSonFileAsync()
    {
        var commentsAsJson = await File.ReadAllTextAsync(_filePath);
        var comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments;
    }

    private async Task CloseJSonFileAsync(List<Comment> comments)
    {
        var commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(_filePath, commentsAsJson);
    }
}