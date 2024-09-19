using Entities;
using RepositoryContracts;

namespace CLI.UI.Manage_post;

public class UpdatePostView(
    IPostRepository postRepository,
    ManagePostView managePostView)
{
    public async Task StartUpdateWindowAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter id of post u want to update");
            var response = Console.ReadLine();
            
            if (response != null)
            {
                int idFromResponse = int.Parse(response);
                if (await postRepository.CheckIfPostExistsAsync(idFromResponse))
                {
                    Post postFromMemory = await postRepository.GetSingleAsync(idFromResponse);
                    Console.WriteLine(postFromMemory.ToString());
                    Console.WriteLine("input 'title' to change title");
                    Console.WriteLine("input 'body' to change body");
                    var responseForSelection = Console.ReadLine();
                    if (responseForSelection != null && responseForSelection.Equals("title", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Enter new title");
                        var newTitle = Console.ReadLine();
                        if (newTitle != null)
                        {
                            postFromMemory.Title = newTitle;
                        }

                        Console.WriteLine("U changed title to: " + newTitle );
                        Console.WriteLine("for post with id: " + postFromMemory.Id);
                        Thread.Sleep(2000);
                        await managePostView.ManagePostViewStartAsync();
                    }

                    if (responseForSelection != null && responseForSelection.Equals("body", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Enter new body");
                        var newBody = Console.ReadLine();
                        if (newBody != null)
                        {
                            postFromMemory.Body = newBody;
                        }
                        Console.WriteLine("U changed body to: " + newBody);
                        Console.WriteLine("for post with id: " + postFromMemory.Id);
                        Thread.Sleep(2000);
                        await managePostView.ManagePostViewStartAsync();
                    }
                }
                else
                {
                    Console.WriteLine("Post not found");
                    await Task.Delay(1000);
                }
            }
        }
    }
}