using VestanTestPlugin.Modules.PostModule.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace VestanTestPlugin.Modules.PostModule {
    public class PostService {
        private readonly Dictionary<Type, IPostCommand> _commands;

        public PostService(List<IPostCommand> commands) {
            _commands = new Dictionary<Type, IPostCommand>();

            for (int i = 0; i < commands.Count; i++) {
                _commands[commands[i].GetType()] = commands[i];
            }
        }

        public async Task<TResult> ExecuteCommandAsync<TResult>() where TResult : class {
            IPostCommand<TResult> concreteCommand = FindCommand<IPostCommand<TResult>>();

            if (concreteCommand == null) {
                throw new ArgumentException($"Command returning {typeof(TResult).Name} not found");
            }

            return await concreteCommand.ExecuteAsync();
        }

        public async Task<TResult> ExecuteCommandAsync<TParam, TResult>(TParam parameter) {
            IPostCommand<TParam, TResult> concreteCommand = FindCommand<IPostCommand<TParam, TResult>>();

            if (concreteCommand == null) {
                throw new ArgumentException($"Command {typeof(TParam).Name} returning {typeof(TResult).Name} not found");
            }

            return await concreteCommand.ExecuteAsync(parameter);
        }

        private T FindCommand<T>() where T : class, IPostCommand {
            using (Dictionary<Type, IPostCommand>.Enumerator enumerator = _commands.GetEnumerator()) {
                while (enumerator.MoveNext()) {
                    KeyValuePair<Type, IPostCommand> kvp = enumerator.Current;

                    if (kvp.Value is T) {
                        return (T)kvp.Value;
                    }
                }
            }

            return null;
        }

        public async Task<List<Post>> GetPostsAsync() {
            return await ExecuteCommandAsync<List<Post>>();
        }

        public async Task<Post> CreatePostAsync(Post newPost) {
            return await ExecuteCommandAsync<Post, Post>(newPost);
        }

        public async Task<Post> UpdatePostAsync(Post updatedPost) {
            return await ExecuteCommandAsync<Post, Post>(updatedPost);
        }

        public async Task<bool> TryDeletePostAsync(int id) {
            return await ExecuteCommandAsync<int, bool>(id);
        }
    }
}