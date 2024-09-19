using Entities;
using RepositoryContracts;

namespace CLI.UI.Manage_post;

public class SpecificPostView(
    IPostRepository postRepository,
    ManagePostView managePostView,
    ICommentRepository commentRepository)
{
    public async Task StartSpecificPostViewAsync()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Please enter the post id you want to view:");
            var postIdToView = Console.ReadLine();
            if (postIdToView != null)
            {
                var postId = int.Parse(postIdToView);
                
                if (await postRepository.CheckIfPostExistsAsync(postId))
                {
                    Post postToLookFor = await postRepository.GetSingleAsync(postId);
                    var commentsForPost = await commentRepository.FindCommentsForPostAsync(postId);

                    Console.WriteLine("Your post:");
                    Console.WriteLine(postToLookFor.ToString());
                    Console.WriteLine();
                    Console.WriteLine("Comments:");
                    foreach (var element in commentsForPost)
                    {
                        Console.WriteLine(element.Body);
                    }
                }
                else
                {
                    Console.WriteLine("Post not found");
                }

                await Task.Delay(1500);
            }

            Console.WriteLine();
            Console.WriteLine("to go back to the menu type 'exit'");
            var response = Console.ReadLine();
            if (response != null && response.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                await managePostView.ManagePostViewStartAsync();
            }
        }
    }
}