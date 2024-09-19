using Entities;
using RepositoryContracts;

namespace CLI.UI.Manage_post;

public class AddCommentToPostView(
    IPostRepository postRepository,
    ManagePostView managePostView,
    ICommentRepository commentRepository)
{
    public async Task StartAddCommentASync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Type id of the post you want to add a comment to");
            var postId = Console.ReadLine();
            Console.WriteLine("Type the comment you want to add");
            var commentContent = Console.ReadLine();
            Console.WriteLine("What is your userID");
            var userId = Console.ReadLine();

            if (postId != null && commentContent != null && userId != null)
            {
                var postIdAsInt = int.Parse(postId);
                var userIdAsInt = int.Parse(userId);
                if (await postRepository.CheckIfPostExistsAsync(postIdAsInt))
                {
                    Comment commentToAdd = new Comment(1, commentContent, postIdAsInt, userIdAsInt);
                    await commentRepository.AddAsync(commentToAdd);

                    Console.WriteLine("Comment added");
                    await Task.Delay(1000);
                    await managePostView.ManagePostViewStartAsync();
                }
                else
                {
                    Console.WriteLine("Post not found");
                    await Task.Delay(1500);
                }
            }
        }
    }
}