using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace VestanTestPlugin.Modules.PostModule.Commands {
    internal class GetPostsJsonPlaceHolderCommand : IPostCommand<List<Post>> {
        private readonly HttpClient _httpClient;
        private const string REQUEST_URI = "posts";

        public GetPostsJsonPlaceHolderCommand(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> ExecuteAsync() {
            HttpResponseMessage response = await _httpClient.GetAsync(REQUEST_URI);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Post>>(json);
        }
    }
}