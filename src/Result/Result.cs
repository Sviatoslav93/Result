using Result.Abstractions;
using Result.Errors;

namespace Result;

public readonly struct Result<TValue> : IResult<TValue>
{
    private readonly TValue? _value;

    private Result(TValue value)
    {
        _value = value;
    }

    private Result(IEnumerable<Error> errors)
    {
        Failure = [..errors];
    }

    private Result(params Error[] errors)
    {
        Failure = [..errors];
    }

    public Failure Failure { get; } = [];
    public bool IsSuccess => !Failure;

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Result is not successful and value can not be accessed.");

    public static implicit operator Result<TValue>(TValue value)
    {
        return new Result<TValue>(value);
    }

    public static implicit operator Result<TValue>(Error error)
    {
        return new Result<TValue>(error);
    }

    public static implicit operator Result<TValue>(Failure failure)
    {
        return new Result<TValue>(failure);
    }

    public static Result<TValue> Success(TValue value) => new(value);

    public static Result<TValue> Failed(params Error[] errors) => new(errors);

    public static Result<TValue> Failed(IEnumerable<Error> errors) => new(errors);

    public void Deconstruct(out TValue? value, out Failure failure)
    {
        value = _value;
        failure = Failure;
    }

    public TNext Match<TNext>(Func<TValue, TNext> onSuccess, Func<Failure, TNext> onFailure)
    {
        return IsSuccess
            ? onSuccess(Value)
            : onFailure(Failure);
    }

    public async Task<TNext> MatchAsync<TNext>(Func<TValue, Task<TNext>> onSuccess, Func<Failure, TNext> onFailure)
    {
        return IsSuccess
            ? await onSuccess(Value)
            : onFailure(Failure);
    }
}
