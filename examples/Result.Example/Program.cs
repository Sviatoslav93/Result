using Result;
using Result.Example;

Console.WriteLine("Result Example");

var result1 = TestService.Divide(10, 2);
PrintResult(result1);

var result2 = TestService.Divide(10, 0);
PrintResult(result2);

TestService.Divide(1024, 7)
    .Match(
        value =>
        {
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("The result is success:");
            var roundedValue = Math.Round(value);
            Console.WriteLine($"Rounded value: {roundedValue}");
            return roundedValue;
        },
        failure =>
        {
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("The result is failed:");
            foreach (var error in failure)
            {
                Console.WriteLine($"Error: {error}");
            }

            return 0;
        });

Console.ReadKey();
return;

void PrintResult<T>(Result<T> result)
{
    var (value, failure) = result;

    Console.WriteLine(new string('-', 20));

    if (failure)
    {
        Console.WriteLine("The result is failed:");
        foreach (var error in failure)
        {
            Console.WriteLine($"Error: {error}");
        }
    }

    Console.WriteLine("The result is success:");
    Console.WriteLine($"Value: {value}");
}
