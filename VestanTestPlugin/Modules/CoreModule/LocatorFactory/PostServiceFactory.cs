using VestanTestPlugin.Modules.PostModule.Commands;
using VestanTestPlugin.Modules.PostModule;
using System.Collections.Generic;
using System.Net.Http;

namespace VestanTestPlugin.Modules.CoreModule.LocatorFactory {
    public class PostServiceFactory : BaseLocatorFactory {
        public override void RegisterServices() {
            RegisterService(CreatePostService);
        }

        private PostService CreatePostService() {
            HttpClient httpClient = GetService<HttpClient>();

            List<IPostCommand> commands = new List<IPostCommand>
            {
                new GetPostsJsonPlaceHolderCommand(httpClient),
                new CreatePostJsonPlaceHolderCommand(httpClient),
                new UpdatePostJsonPlaceHolderCommand(httpClient),
                new DeletePostJsonPlaceHolderCommand(httpClient)
            };

            return new PostService(commands);
        }
    }
}