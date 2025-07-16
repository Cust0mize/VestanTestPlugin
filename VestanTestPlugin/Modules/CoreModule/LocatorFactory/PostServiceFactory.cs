using VestanTestPlugin.Modules.PostModule.Commands;
using VestanTestPlugin.Modules.PostModule;
using System.Collections.Generic;
using System.Net.Http;

namespace VestanTestPlugin.Modules.CoreModule.LocatorFactory {
    public class PostServiceFactory : BaseLocatorFactory {
        private readonly PostCommandContainer _commandContainer;

        public PostServiceFactory(List<IPostCommand> commands) {
            _commandContainer = new PostCommandContainer(commands);
        }

        public override void RegisterServices() {
            RegisterService(CreatePostService);
        }

        private PostService CreatePostService() {
            HttpClient httpClient = GetService<HttpClient>();
            List<IPostCommand> commands = _commandContainer.GetCommands(httpClient);
            return new PostService(commands);
        }
    }

    public class PostCommandContainer {
        private readonly List<IPostCommand> _userCommands;

        public PostCommandContainer(List<IPostCommand> commands) {
            _userCommands = commands;
        }

        public List<IPostCommand> GetCommands(HttpClient httpClient) {
            List<IPostCommand> defaultCommands = new List<IPostCommand>
            {
                new GetPostsJsonPlaceHolderCommand(httpClient),
                new CreatePostJsonPlaceHolderCommand(httpClient),
                new UpdatePostJsonPlaceHolderCommand(httpClient),
                new DeletePostJsonPlaceHolderCommand(httpClient)
            };

            if (_userCommands != null && _userCommands.Count != 0) {
                defaultCommands.AddRange(_userCommands);
            }

            return defaultCommands;
        }
    }
}