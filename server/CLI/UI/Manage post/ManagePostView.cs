using RepositoryContracts;

namespace CLI.UI.Manage_post;

public class ManagePostView()
{
    private readonly CreatePostView _createPostView;
    private readonly ListPostView _listPostView;
    private readonly UpdatePostView _updatePostView;
    private readonly DeletePostView _deletePostView;
    private readonly SpecificPostView _specificPostView;
    private readonly AddCommentToPostView _addCommentToPostView;

    public ManagePostView(IPostRepository postRepository, ICommentRepository commentRepository) : this()
    {
        _createPostView = new CreatePostView(postRepository, this);
        _listPostView = new ListPostView(postRepository, this);
        _updatePostView = new UpdatePostView(postRepository, this);
        _deletePostView = new DeletePostView(postRepository, this);
        _specificPostView = new SpecificPostView(postRepository, this, commentRepository);
        _addCommentToPostView = new AddCommentToPostView(postRepository, this, commentRepository);
    }


    public async Task ManagePostViewStartAsync()
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
            Console.WriteLine("3. Update post (Type 'Update')");
            Console.WriteLine("4. Delete post (Type 'Delete')");
            Console.WriteLine("5. Specific post (Type 'Specific')");
            Console.WriteLine("6. Add Comment (Type 'AddComment')");
            Console.WriteLine("---------------------------------------------");
            String? response = Console.ReadLine()?.ToLower();


            if (response != null)
            {
                if (response.Equals("create"))
                {
                    await _createPostView.CreatPostAsync();
                }

                if (response.Equals("list", StringComparison.OrdinalIgnoreCase))
                {
                    await _listPostView.ListAllPostsAsync();
                }

                if (response.Equals("update", StringComparison.OrdinalIgnoreCase))
                {
                    await _updatePostView.StartUpdateWindowAsync();
                }

                if (response.Equals("delete", StringComparison.OrdinalIgnoreCase))
                {
                    await _deletePostView.DeletePostViewStartAsync();
                }

                if (response.Equals("specific", StringComparison.OrdinalIgnoreCase))
                {
                    await _specificPostView.StartSpecificPostViewAsync();
                }

                if (response.Equals("addcomment", StringComparison.OrdinalIgnoreCase))
                {
                    await _addCommentToPostView.StartAddCommentASync();
                }
                
            }
        }
    }
}