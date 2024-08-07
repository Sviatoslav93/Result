using Result.Errors;

namespace Result.Example;

public static class TestService
{
    public static Result<decimal> Divide(decimal a, decimal b)
    {
        if (b == 0)
        {
            return new Error("ErrorCode", "Division by zero");
        }

        return a / b;
    }
}
