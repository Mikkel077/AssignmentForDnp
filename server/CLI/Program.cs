using System.Threading.Channels;
using CLI.UI;
using InMemoryRepositories;
using RepositoryContracts;


Console.WriteLine("Started CLI......");
IUserRepository userRepository = new UserInMemoryRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository();



CliApp cliApp = new CliApp(commentRepository, postRepository, userRepository);
await cliApp.StartAppAsync();