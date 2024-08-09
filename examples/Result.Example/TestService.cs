using System.Text.RegularExpressions;
using Result.Errors;
using Result.Extensions;

namespace Result.Example;

public static partial class TestService
{
    public static Result<decimal> Divide(decimal a, decimal b)
    {
        if (b == 0)
        {
            return new Error("ErrorCode", "Division by zero");
        }

        return a / b;
    }

    public static Result<UserEntity> CreateUser(CreateUserCommand command)
    {
        if (Validate().ToFailure(out var failure))
        {
            return failure;
        }

        // some internal logic to store user in the database etc.
        var user = new UserEntity(command.Name, command.Email, command.BirthDay);

        return user;

        IEnumerable<Error> Validate()
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                yield return Error.Validation("NameIsRequired", "user name is required");
            }

            if (string.IsNullOrEmpty(command.Email))
            {
                yield return Error.Validation("EmailIsRequired", "user email is required");
            }
            else
            {
                if (!EmailRegex().IsMatch(command.Email))
                {
                    yield return Error.Validation("EmailIsInvalid", "user email is invalid");
                }
            }

            if (command.BirthDay == default)
            {
                yield return Error.Validation("BirthDayIsRequired", "user birth day is required");
            }
        }
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();

    public class UserEntity(
        string name,
        string email,
        DateTimeOffset birthDay)
    {
        private string Name { get; } = name;
        private string Email { get; } = email;
        private DateTimeOffset BirthDay { get; } = birthDay;

        public override string ToString()
        {
            return $"Name: {Name}, Email: {Email}, BirthDay: {BirthDay:yyyy-MM-dd}";
        }
    }

    public class CreateUserCommand
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTimeOffset BirthDay { get; set; }
    }
}
