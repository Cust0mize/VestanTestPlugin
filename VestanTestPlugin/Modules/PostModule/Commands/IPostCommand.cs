using System.Threading.Tasks;

namespace VestanTestPlugin.Modules.PostModule.Commands {
    public interface IPostCommand<TParam1, TParam2, TResult> : IPostCommand {
        Task<TResult> ExecuteAsync(TParam1 parameter1, TParam2 parameter2);
    }

    public interface IPostCommand<TParam, TResult> : IPostCommand {
        Task<TResult> ExecuteAsync(TParam parameter);
    }

    public interface IPostCommand<TResult> : IPostCommand {
        Task<TResult> ExecuteAsync();
    }

    public interface IPostCommand {
    }
}