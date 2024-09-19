using Entities;
using RepositoryContracts;

namespace CLI.UI.Manage_post;

public class CreatePostView(
    IPostRepository postRepository,
    ManagePostView managePostView
)
{
    public async Task CreatPostAsync()
    {
        Console.Clear();
        Console.WriteLine("You selected Create Post");

        Console.WriteLine("Enter the title of the post:");
        var postName = Console.ReadLine();
        
        Console.WriteLine("Enter the body of the post:");
        var postBody = Console.ReadLine();
        
        Console.WriteLine("What is your userID?");
        var userId = Console.ReadLine();
        

        if (postName != null && postBody != null && userId != null)
        {
            var userIdAsInt = int.Parse(userId);
            Post post = new Post(postRepository.GetManyAsync().Count() + 1, postName, postBody, userIdAsInt);
            await postRepository.AddAsync(post);
            Console.WriteLine("Post successfully created!");
            Console.WriteLine("--------------------");
            Console.WriteLine("Your post details:" + post.ToString());
            Console.WriteLine();
            Thread.Sleep(1500);
            await managePostView.ManagePostViewStartAsync();
        }
        else
        {
            Console.WriteLine("Error try again");
        }
    }
}