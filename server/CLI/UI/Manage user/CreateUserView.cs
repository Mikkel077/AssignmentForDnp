using Entities;
using RepositoryContracts;

namespace CLI.UI.Manage_user;

public class CreateUserView(
    ManageUserView manageUserView,
    IUserRepository userRepository)
{
    public async Task StartCreateUserViewAsync()
    {
            Console.Clear();
            Console.WriteLine("Give me your username:");
            var username = Console.ReadLine();
            Console.WriteLine("Give me your Password:");
            var password = Console.ReadLine();
            if (username != null && password != null)
            {
                User userToCreate = new User(1, username, password);
                await userRepository.AddAsync(userToCreate);
            }

            Console.WriteLine("User created");
            await Task.Delay(1000);
    }
}