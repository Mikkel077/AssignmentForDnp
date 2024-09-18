using Entities;
using RepositoryContracts;

namespace CLI.UI.Manage_post;

public class ListPostView(IPostRepository postRepository,
    ManagePostView managePostView)
{
    public void ListAllPostsAsync()
    {
        Console.WriteLine("test");
    }
}