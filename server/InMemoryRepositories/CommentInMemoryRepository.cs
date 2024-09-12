using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private List<Comment> comments;

    public CommentInMemoryRepository()
    {
        this.comments = new List<Comment>();
        
        createData();
    }


    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(c => c.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.Id}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(c => c.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }

        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? commentToReturn = comments.Find(c => c.Id == id);
        if (commentToReturn is null)
        {
            throw new InvalidOperationException(
                $"Comment with {id} was not found"
            );
        }

        return Task.FromResult(commentToReturn);
    }


    public IQueryable<Comment> GetManyAsync()
    {
        return comments.AsQueryable();
    }

    public void createData()
    {
        Comment comment1 = new Comment(1, "Hi!!!", 1, 1);
        Comment comment2 = new Comment(2, "Hi!!!", 2, 2);
        Comment comment3 = new Comment(3, "Hi!!!", 3, 3);
        
        comments.Add(comment1);
        comments.Add(comment2);
        comments.Add(comment3);
        
    }
    
}