using System.Collections;
using Result.Errors;

namespace Result;

public readonly record struct Failure : IEnumerable<Error>
{
    private readonly List<Error> _errors = [];

    public Failure()
    {
    }

    public Failure(IEnumerable<Error> errors)
    {
        _errors = [..errors];
    }

    public Failure(params Error[] errors)
        : this(errors.AsEnumerable())
    {
    }

    public bool HasAnyErrors => _errors.Count != 0;

    public static implicit operator bool(Failure failure)
    {
        return failure.HasAnyErrors;
    }

    public void Add(Error error)
    {
        _errors.Add(error);
    }

    public void AddRange(IEnumerable<Error> errors)
    {
        _errors.AddRange(errors);
    }

    public IEnumerator<Error> GetEnumerator()
    {
        return _errors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
