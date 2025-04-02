using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    public static IResult<T?> Validate<T>(T? value)
    {
        return new Result<T?>(Validate((object?)value));
    }

    public static IResult<object?> Validate(object? value)
    {
        return Result<object?>.Ok();
    }
}