using VestanTestPlugin.Modules.CoreModule;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace VestanTestPlugin.Modules.PostModule.Commands {
    internal class CreatePostJsonPlaceHolderCommand : IPostCommand<Post, Post> {
        private readonly HttpClient _httpClient;
        
        private const string REQUEST_URI = "posts";

        public CreatePostJsonPlaceHolderCommand(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<Post> ExecuteAsync(Post newPost) {
            string json = JsonConvert.SerializeObject(newPost);
            StringContent content = new StringContent(json, Encoding.UTF8, GlobalConstant.APPLICATION_URL);
            HttpResponseMessage response = await _httpClient.PostAsync(REQUEST_URI, content);
            response.EnsureSuccessStatusCode();
            string responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Post>(responseJson);
        }
    }
}