using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

using Rum.Text;

namespace Rum.Cmd;

public static partial class CmdParser
{
    public static IDictionary<string, string?> Parse(params string[] args)
    {
        Dictionary<string, string?> cmd = [];
        var position = 0;

        for (var i = 0; i < args.Length; i++)
        {
            if (args[i].StartsWith('-'))
            {
                var (key, value) = ParseKeyValue(args[i]);

                if (value == null && i < args.Length - 1 && !args[i + 1].StartsWith('-'))
                {
                    value = args[i + 1];
                    i++;
                }

                cmd[key] = value;
                continue;
            }

            cmd[$"${position}"] = args[i];
            position++;
        }

        return cmd;
    }

    public static void Parse<T>(params string[] args) where T : ICommand, new()
    {
        var name = args[0];
        args = args.Skip(1).ToArray();

        try
        {
            var cmd = new T();
            var type = typeof(T);
            var attr = type.GetCustomAttribute<CommandAttribute>() ?? throw new Exception($"type '{type.Name}' is not a command");

            if (!attr.Select(name))
            {
                throw new Exception($"received command '{name}' expected '{attr.Name ?? type.Name}'");
            }

            var values = Parse(args);
            var properties = type.GetProperties().Where(p => p.CanRead && p.CanWrite);

            foreach (var property in properties)
            {
                var command = property.GetCustomAttribute<CommandAttribute>();

                if (command == null || args.Length == 0 || !command.Select(args[0])) continue;

                var method = typeof(CmdParser)
                    .GetMethods()
                    .Where(m => m.IsGenericMethod && m.Name == "Parse")
                    .FirstOrDefault();

                method = method?.MakeGenericMethod(property.PropertyType);
                method?.Invoke(null, [args]);
                return;
            }

            foreach (var (key, value) in values)
            {
                foreach (var property in properties)
                {
                    var option = property.GetCustomAttribute<OptionAttribute>();

                    if (option == null || !option.Select(key)) continue;

                    var parsed = option.Parse(property.PropertyType, value);
                    property.SetValue(cmd, parsed);
                }
            }

            var context = new ValidationContext(cmd);
            Validator.ValidateObject(cmd, context);
            cmd.Execute();
        }
        catch (Exception ex)
        {
            Console.Error.Write(GetHelp<T>());
            Console.Error.WriteLine(new StringBuilder().Red(new StringBuilder().Bold(ex.Message).ToString()));
        }
    }

    private static (string, string?) ParseKeyValue(string arg)
    {
        arg = arg.StartsWith("--") ? arg.Remove(0, 2) : arg.Remove(0, 1);
        var parts = arg.Split('=', 2);

        if (parts.Length == 1)
        {
            return (parts[0], null);
        }

        return (parts[0], parts[1]);
    }
}