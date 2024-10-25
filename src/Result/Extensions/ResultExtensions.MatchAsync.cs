namespace Result.Extensions;

public static partial class ResultExtensions
{
    public static async Task<TNext> MatchAsync<TValue, TNext>(this Result<TValue> result, Func<TValue, Task<TNext>> onSuccess, Func<IEnumerable<Error>, Task<TNext>> onFailure)
    {
        return result.IsSuccess
            ? await onSuccess(result.Value)
            : await onFailure(result.Errors);
    }

    public static async Task<TNext> MatchAsync<TValue, TNext>(this Result<TValue> result, Func<TValue, Task<TNext>> onSuccess, Func<IEnumerable<Error>, TNext> onFailure)
    {
        return result.IsSuccess
            ? await onSuccess(result.Value)
            : onFailure(result.Errors);
    }

    public static async Task<TNext> MatchAsync<TValue, TNext>(this Result<TValue> result, Func<TValue, TNext> onSuccess, Func<IEnumerable<Error>, Task<TNext>> onFailure)
    {
        return result.IsSuccess
            ? onSuccess(result.Value)
            : await onFailure(result.Errors);
    }

    public static async Task<TNext> MatchAsync<TValue, TNext>(this Task<Result<TValue>> task, Func<TValue, TNext> onSuccess, Func<IEnumerable<Error>, TNext> onFailure)
    {
        var result = await task;
        return result.IsSuccess
            ? onSuccess(result.Value)
            : onFailure(result.Errors);
    }

    public static async Task<TNext> MatchAsync<TValue, TNext>(this Task<Result<TValue>> task, Func<TValue, Task<TNext>> onSuccess, Func<IEnumerable<Error>, TNext> onFailure)
    {
        var result = await task;
        return result.IsSuccess
            ? await onSuccess(result.Value)
            : onFailure(result.Errors);
    }

    public static async Task<TNext> MatchAsync<TValue, TNext>(this Task<Result<TValue>> task, Func<TValue, TNext> onSuccess, Func<IEnumerable<Error>, Task<TNext>> onFailure)
    {
        var result = await task;
        return result.IsSuccess
            ? onSuccess(result.Value)
            : await onFailure(result.Errors);
    }

    public static async Task<TNext> MatchAsync<TValue, TNext>(this Task<Result<TValue>> task, Func<TValue, Task<TNext>> onSuccess, Func<IEnumerable<Error>, Task<TNext>> onFailure)
    {
        var result = await task;
        return result.IsSuccess
            ? await onSuccess(result.Value)
            : await onFailure(result.Errors);
    }
}
