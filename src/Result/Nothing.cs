namespace Result;

/// <summary>
/// Represents a type that has no value.
/// </summary>
public readonly struct Nothing : IEquatable<Nothing>, IComparable<Nothing>, IComparable
{
    private static readonly Nothing InitialValue = default;
    public static ref readonly Nothing Value => ref InitialValue;

    public static Task<Nothing> Task { get; } = System.Threading.Tasks.Task.FromResult(InitialValue);

    public static bool operator ==(Nothing first, Nothing second) => true;

    public static bool operator !=(Nothing first, Nothing second) => false;

    public int CompareTo(Nothing other) => 0;

    int IComparable.CompareTo(object? obj) => 0;

    public override int GetHashCode() => 0;

    public bool Equals(Nothing other) => true;

    public override bool Equals(object? obj) => obj is Nothing;

    public override string ToString() => "()";
}
