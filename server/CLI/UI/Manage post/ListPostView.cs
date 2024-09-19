using Entities;
using RepositoryContracts;

namespace CLI.UI.Manage_post;

public class ListPostView(
    IPostRepository postRepository,
    ManagePostView managePostView)
{
    public async Task ListAllPostsAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Here is the list of all posts: ");
            Console.WriteLine();
            IQueryable<Post> listOfPosts = postRepository.GetManyAsync();
            foreach (var variable in listOfPosts)
            {
                Console.WriteLine(variable.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("To go back to the menu, type 'exit' to exit.");
            var response = Console.ReadLine()?.ToLower();
            if (response != null && response.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                await managePostView.WindowsManageStartAsync();
                break;
            }
        }
    }
}