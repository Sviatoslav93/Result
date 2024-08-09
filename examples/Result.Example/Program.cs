using Result.Example;

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
    { IsSuccessful: true } => $"Operation succeeded with value: {result2.Value}",
    { IsSuccessful: false } => $"Operation failed with errors: {string.Join(", ", result2.Failure)}",
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

Console.ReadKey();
