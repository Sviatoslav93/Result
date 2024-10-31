namespace Result.Extensions;

public static partial class ResultExtensions
{
    public static TNext Match<TValue, TNext>(
            this Result<TValue> result,
            Func<TValue, TNext> onSuccess,
            Func<IEnumerable<Error>, TNext> onFailure)
    {
        return result.IsSuccess
            ? onSuccess(result.Value)
            : onFailure(result.Errors);
    }
}
