﻿using CLI.UI.Manage_post;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp(
    ICommentRepository commentRepository,
    IPostRepository postRepository,
    IUserRepository userRepository)
{
    private readonly ManagePostView _managePostView = new ManagePostView(postRepository, commentRepository);


    public async Task StartAppAsync()
    {
        Console.WriteLine("Starting app...");
        await OpenInitialWindows();
    }

    private async Task OpenInitialWindows()
    {
        Console.WriteLine("What entity would you like to work on");
        Console.WriteLine("Type the entity you would like to work on");
        Console.WriteLine("1. User");
        Console.WriteLine("2. Comment");
        Console.WriteLine("3. Post");
        var respons = Console.ReadLine()?.ToLower();
        if (respons is "user")
        {
            //Do something
        }
        else if (respons is "comment")
        {
            //Do something
        }
        else if (respons is "post")
        {
            await _managePostView.WindowsManageStartAsync();
        }
    }
}