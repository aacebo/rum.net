using Rum.Graph.Exceptions;

namespace Rum.Graph.Extensions;

public static class TypeExtensions
{
    public static bool IsOptional(this Type type)
    {
        return Nullable.GetUnderlyingType(type) is not null;
    }

    public static string GetHumanFriendlyName(this Type type)
    {
        var _type = type;

        if (_type.IsOptional())
        {
            _type = Nullable.GetUnderlyingType(_type) ?? throw new InvalidTypeException(_type, "type is not nullable");
        }

        int index = _type.Name.IndexOf('`');
        return index == -1 ? _type.Name : _type.Name[..index];
    }

    public static bool IsEnumerable(this Type type)
    {
        return type.IsAssignableTo(typeof(IEnumerable<object>));
    }

    public static Type GetIndexType(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            return type.GetGenericArguments()[0];
        }

        var iface = (from i in type.GetInterfaces()
                     where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                     select i).FirstOrDefault();

        return iface is null
            ? throw new ArgumentException("Does not represent an enumerable type.", nameof(type))
            : GetIndexType(iface);
    }
}