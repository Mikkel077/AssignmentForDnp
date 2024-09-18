using RepositoryContracts;

namespace CLI.UI.Manage_post;

public class ManagePostView()
{
    private readonly CreatePostView _createPostView;
    private readonly ListPostView _listPostView;

    public ManagePostView(IPostRepository postRepository) : this()
    {
        _createPostView = new CreatePostView(postRepository, this);
        _listPostView = new ListPostView(postRepository, this);
    }


    public async Task WindowsManageStartAsync()
    {
        var run = true;
        while (run)
        {
            Console.Clear();
            Console.WriteLine("Post Menu");
            Console.WriteLine("----------------");
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Create post (Type 'Create')");
            Console.WriteLine("2. List all posts (Type 'List')");
            Console.WriteLine("3. View one post (Type 'one')");
            Console.WriteLine("---------------------------------------------");
            String? response = Console.ReadLine()?.ToLower();


            if (response != null)
            {
                if (response.Equals("create"))
                {
                    await _createPostView.CreatPostAsync();
                }
                else if (response.Equals("list"))
                {
                    _listPostView.ListAllPostsAsync();
                }
                else
                {
                    run = false;
                }
            }
        }
    }
}