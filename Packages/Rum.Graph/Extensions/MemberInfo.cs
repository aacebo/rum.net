using System.Reflection;
using System.Text.Json.Serialization;

namespace Rum.Graph.Exceptions;

public static class MemberInfoExtensions
{
    public static bool IsOptional(this MemberInfo member)
    {
        return Nullable.GetUnderlyingType(member.GetType()) is not null;
    }

    public static string GetName(this MemberInfo member)
    {
        return member.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? member.Name;
    }

    public static void SetValue(this MemberInfo member, object? obj, object? value)
    {
        if (member is FieldInfo field)
        {
            field.SetValue(obj, value);
        }
        else if (member is PropertyInfo property)
        {
            property.SetValue(obj, value);
        }
    }

    public static object? GetValue(this MemberInfo member, object? obj)
    {
        if (member is FieldInfo field)
        {
            return field.GetValue(obj);
        }
        else if (member is PropertyInfo property)
        {
            return property.GetValue(obj);
        }

        return null;
    }

    public static Type GetPropertyOrFieldType(this MemberInfo member)
    {
        if (member is PropertyInfo property)
        {
            return property.PropertyType;
        }
        else if (member is FieldInfo field)
        {
            return field.FieldType;
        }

        return member.GetType();
    }
}