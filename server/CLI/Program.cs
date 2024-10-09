using System.Threading.Channels;
using CLI.UI;
using FileRepositories;
using InMemoryRepositories;
using RepositoryContracts;


Console.WriteLine("Started CLI......");
IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();
IPostRepository postRepository = new PostFileRepository();



CliApp cliApp = new CliApp(commentRepository, postRepository, userRepository);
await cliApp.StartAppAsync();