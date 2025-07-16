using System.Threading.Tasks;
using System.Net.Http;

namespace VestanTestPlugin.Modules.PostModule.Commands {
    internal class DeletePostJsonPlaceHolderCommand : IPostCommand<int, bool> {
        private readonly HttpClient _httpClient;
        
        private const string REQUEST_URI = "posts/{0}";

        public DeletePostJsonPlaceHolderCommand(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<bool> ExecuteAsync(int id) {
            HttpResponseMessage response = await _httpClient.DeleteAsync(string.Format(REQUEST_URI, id));
            return response.IsSuccessStatusCode;
        }
    }
}