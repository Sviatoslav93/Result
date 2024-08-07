using System.Collections;
using Result.Errors;

public readonly record struct Failure : IEnumerable<Error>
{
    private readonly List<Error> _errors = [];

    public Failure(IEnumerable<Error> errors)
    {
        _errors = new List<Error>(errors);
    }

    public Failure(params Error[] errors)
    {
        _errors = new List<Error>(errors);
    }

    public bool HasAnyErrors => _errors.Count != 0;

    public static implicit operator bool(Failure failure) => failure.HasAnyErrors;

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
