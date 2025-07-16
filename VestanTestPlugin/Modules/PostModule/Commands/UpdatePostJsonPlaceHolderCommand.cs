using VestanTestPlugin.Modules.CoreModule;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace VestanTestPlugin.Modules.PostModule.Commands {
    internal class UpdatePostJsonPlaceHolderCommand : IPostCommand<Post, Post> {
        private readonly HttpClient _httpClient;
        private const string REQUEST_URI = "posts/{0}";

        public UpdatePostJsonPlaceHolderCommand(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<Post> ExecuteAsync(Post updatedPost) {
            string json = JsonConvert.SerializeObject(updatedPost);
            StringContent content = new StringContent(json, Encoding.UTF8, GlobalConstant.APPLICATION_URL);
            HttpResponseMessage response = await _httpClient.PutAsync(string.Format(REQUEST_URI, updatedPost.Id), content);
            response.EnsureSuccessStatusCode();
            string responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Post>(responseJson);
        }
    }
}