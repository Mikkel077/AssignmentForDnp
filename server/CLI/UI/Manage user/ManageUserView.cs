using RepositoryContracts;

namespace CLI.UI.Manage_user;

public class ManageUserView()
{
    private readonly CreateUserView _createUserView;


    public ManageUserView(IUserRepository userRepository) : this()
    {
        _createUserView = new CreateUserView(this, userRepository);
    }

    public async Task ManageUserViewStartAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Post Menu");
            Console.WriteLine("----------------");
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Create User (Type 'Create')");
            Console.WriteLine("---------------------------------------------");
            String? response = Console.ReadLine()?.ToLower();

            if (response != null && response.Equals("create", StringComparison.OrdinalIgnoreCase))
            {
                await _createUserView.StartCreateUserViewAsync();
            }
        }
    }
}