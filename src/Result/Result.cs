using Result.Abstractions;
using Result.Errors;

namespace Result;

public readonly struct Result<T> : IResult<T>
{
    private readonly T? _value;

    public Result(T value)
    {
        _value = value;
    }

    public Result(IEnumerable<Error> errors)
    {
        Failure = [..errors];
    }

    public Result(params Error[] errors)
    {
        Failure = [..errors];
    }

    public Failure Failure { get; } = [];
    public bool IsSuccess => !Failure;

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Result is not successful and value can not be accessed.");

    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return new Result<T>(error);
    }

    public static implicit operator Result<T>(Failure failure)
    {
        return new Result<T>(failure);
    }

    public void Deconstruct(out T? value, out Failure failure)
    {
        value = _value;
        failure = Failure;
    }

    public TProjection Match<TProjection>(Func<T, TProjection> success, Func<Failure, TProjection> failure)
    {
        return IsSuccess
            ? success(Value)
            : failure(Failure);
    }

    public async Task<TProjection> MatchAsync<TProjection>(Func<T, Task<TProjection>> success, Func<Failure, TProjection> failure)
    {
        return IsSuccess
            ? await success(Value)
            : failure(Failure);
    }
}
