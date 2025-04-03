using System.Reflection;

using Rum.Core;
using Rum.Data.Annotations;

namespace Rum.Data;

public static partial class Schemas
{
    public static IResult<object?> Validate(object value)
    {
        return Validate(value, value.GetType());
    }

    public static IResult<T?> Validate<T>(T? value)
    {
        return new Result<T?>(Validate(value, typeof(T)));
    }

    public static IResult<object?> Validate(object? value, Type type)
    {
        var schema = Get(type);
        return schema.Validate(value);
    }

    public static AnySchema<object?> Get(object value)
    {
        return Get(value.GetType());
    }

    public static AnySchema<T?> Get<T>()
    {
        return Any<T?>().Merge(Get(typeof(T)));
    }

    public static AnySchema<object?> Get(Type type)
    {
        if (!type.IsClass)
        {
            throw new InvalidOperationException("only classes can be validated using annotations");
        }

        var schema = Object();
        var properties = type.GetProperties().Where(p => p.CanRead && p.CanWrite);

        foreach (var property in properties)
        {
            var rule = GetProperty(property);
            schema = schema.Property(property.Name, rule);
        }

        return schema;
    }

    private static AnySchema<object?> GetProperty(PropertyInfo property)
    {
        var schema = GetBaseByType(property.PropertyType);
        var attributes = property.GetCustomAttributes<SchemaAttribute>();

        foreach (var attr in attributes)
        {
            schema = attr.Apply(schema);
        }

        return schema;
    }

    private static AnySchema<object?> GetBaseByType(Type type)
    {
        if (type == typeof(string)) return String().ToAny();
        if (type == typeof(bool)) return Bool().ToAny();
        if (type == typeof(double)) return Double().ToAny();
        if (type == typeof(int)) return Int().ToAny();
        if (type.IsArray) return Array().ToAny();
        return Object().ToAny();
    }
}