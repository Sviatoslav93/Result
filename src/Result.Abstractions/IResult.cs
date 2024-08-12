namespace Result.Abstractions;

public interface IResult
{
    bool IsSuccess { get; }
}

public interface IResult<out T> : IResult
{
    T? Value { get; }
}
