using Newtonsoft.Json;

namespace VestanTestPlugin.Modules.PostModule {
    public class Post {
        public int Id { get; }
        public string Title { get; }
        public string Body { get; }
        public int UserId { get; }

        [JsonConstructor]
        public Post(
        int id,
        string title,
        string body,
        int userId
        ) {
            Id = id;
            Title = title;
            Body = body;
            UserId = userId;
        }

        public Post(string title, string body) : this(0, title, body, userId: 1) {
        }

        public Post() : this(0, "", "", userId: 1) {
        }
    }
}