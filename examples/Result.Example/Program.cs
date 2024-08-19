using System.Globalization;
using Result.Example;
using Result.Extensions;

// Example_1
Console.WriteLine("\nExample_1:");

// get the successful result
var (value1, failure1) = TestService.Divide(10, 2);

// failure can be implicitly converted to bool
var result1Str = failure1
    ? $"Operation failed with errors: {string.Join(", ", failure1)}"
    : $"Operation succeeded with value: {value1}";
Console.WriteLine(result1Str);

// Example_2
Console.WriteLine("\nExample_2:");

// get the failed result
var result2 = TestService.Divide(10, 0);
var result2Str = result2 switch
{
    { IsSuccess: true } => $"Operation succeeded with value: {result2.Value}",
    { IsSuccess: false } => $"Operation failed with errors: {string.Join(", ", result2.Failure)}",
};
Console.WriteLine(result2Str);

// Example_3
Console.WriteLine("\nExample_3:");

// use Match method to handle both success and failure scenarios
var result3Str = TestService.Divide(1024, 7)
    .Match(
        value => $"Operation succeeded with value: {Math.Round(value, 2)}",
        failure => $"Operation failed with errors: {string.Join(", ", failure)}");
Console.WriteLine(result3Str);

// Example_4
Console.WriteLine("\nExample_4:");

var (value4, failure4) = TestService.CreateUser(new TestService.CreateUserCommand()
{
    Name = string.Empty,
    Email = "test$test.com",
    BirthDay = default,
});
var result4Str = failure4
    ? $"Operation failed with errors: {string.Join(", ", failure4)}"
    : $"Operation succeeded with value: {value4}";
Console.WriteLine(result4Str);

// Example_5
Console.WriteLine("\nExample_5:");

var (value5, failure5) = TestService.CreateUser(new TestService.CreateUserCommand()
{
    Name = "John Doe",
    Email = "example@gmail.com",
    BirthDay = new DateTimeOffset(1993, 9, 22, 0, 0, 0, TimeSpan.Zero),
});
var result5Str = failure5
    ? $"Operation failed with errors: {string.Join(", ", failure5)}"
    : $"Operation succeeded with value: {value5}";
Console.WriteLine(result5Str);

// Example_6
Console.WriteLine("\nExample_6:");

var (result6, failure6) = TestService.Divide(10, 2)
    .Then(x => TestService.Divide(x, 2))
    .Then(x => TestService.Divide(x, 2))
    .Then(x => Math.Round(x, 1))
    .Then(x => x.ToString(CultureInfo.InvariantCulture));

var result6Str = failure6
    ? $"Operation failed with errors: {string.Join(", ", failure6)}"
    : $"Operation succeeded with value: {result6}";

Console.WriteLine(result6Str);

// Example_7
Console.WriteLine("\nExample_7:");

var source = new CancellationTokenSource();
source.CancelAfter(1000 * 4);
var token = source.Token;

var res7 = await TestService.Divide(10, 2)
    .ThenAsync(x => TestService.DivideAsync(x, 2, token))
    .ThenAsync(x => TestService.DivideAsync(x, 2, token))
    .ThenAsync(x => Math.Round(x, 1))
    .ThenAsync(async x =>
    {
        await Task.Delay(10, token);
        return x.ToString(CultureInfo.InvariantCulture);
    });

var result7Str = res7 switch
{
    { IsSuccess: true } => $"Operation succeeded with value: {res7.Value}",
    { IsSuccess: false } => $"Operation failed with errors: {string.Join(", ", res7.Failure)}",
};
Console.WriteLine(result7Str);

// wait for user input
Console.ReadKey();
