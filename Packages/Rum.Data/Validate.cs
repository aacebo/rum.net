using System.Reflection;

using Rum.Core;
using Rum.Data.Annotations;

namespace Rum.Data;

public static partial class Schemas
{
    public static IResult<object> Validate(object value)
    {
        return Validate(value, value.GetType());
    }

    public static IResult<T> Validate<T>(T? value)
    {
        return new Result<T>(Validate(value, typeof(T)));
    }

    public static IResult<object> Validate(object? value, Type type)
    {
        var schema = Get(type);
        return schema.Validate(value);
    }

    public static AnySchema Get(object value)
    {
        return Get(value.GetType());
    }

    public static AnySchema Get<T>()
    {
        return Get(typeof(T));
    }

    public static AnySchema Get(Type type)
    {
        var schema = GetBaseByType(type);
        var attributes = type.GetCustomAttributes<SchemaAttribute>();

        foreach (var attr in attributes)
        {
            schema = attr.Apply(schema);
        }

        if (schema is ObjectSchema objectSchema)
        {
            var properties = type.GetProperties().Where(p => p.CanRead && p.CanWrite);

            foreach (var property in properties)
            {
                var rule = GetProperty(property);
                objectSchema = objectSchema.Property(property.Name, rule);
            }
        }

        return schema;
    }

    private static AnySchema GetProperty(PropertyInfo property)
    {
        var schema = GetBaseByType(property.PropertyType);
        var attributes = property.GetCustomAttributes<SchemaAttribute>();

        foreach (var attr in attributes)
        {
            schema = attr.Apply(schema);
        }

        return schema;
    }

    private static AnySchema GetBaseByType(Type type)
    {
        if (type == typeof(string)) return String();
        if (type == typeof(bool)) return Bool();
        if (type == typeof(double)) return Double();
        if (type == typeof(int)) return Int();
        if (type.IsArray) return Array();
        return Object();
    }
}