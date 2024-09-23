using Entities;
using RepositoryContracts;

namespace CLI.UI.Manage_post;

public class DeletePostView(
    IPostRepository postRepository,
    ManagePostView managePostView)
{
    public async Task DeletePostViewStartAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter id of post you want to delete");

            var response = Console.ReadLine();

            if (response != null)
            {
                var postId = int.Parse(response);
                if (await postRepository.CheckIfPostExistsAsync(postId))
                {
                    await postRepository.DeleteAsync(postId);
                    Console.WriteLine("Post deleted with id: " + postId + " Was deleted");
                    await Task.Delay(1500);
                    await managePostView.ManagePostViewStartAsync();
                }
                else
                {
                    Console.WriteLine("Post not found");
                    Console.WriteLine("type 'exit' to exit");
                    Console.WriteLine("Type 'y' to continue");
                    var responseToExit = Console.ReadLine();
                    if (responseToExit != null && responseToExit.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        await managePostView.ManagePostViewStartAsync();
                    }

                    if (responseToExit != null && responseToExit.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        await DeletePostViewStartAsync();
                    }
                    await Task.Delay(1500);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid post ID.");
                await Task.Delay(1500);
            }
        }
    }
}