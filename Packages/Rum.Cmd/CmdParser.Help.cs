using System.Reflection;
using System.Text;

using Rum.Text;

namespace Rum.Cmd;

public static partial class CmdParser
{
    public static string GetHelp<T>() where T : ICommand, new()
    {
        var type = typeof(T);
        var attr = type.GetCustomAttribute<CommandAttribute>() ?? throw new Exception($"type '{type.Name}' is not a command");
        var name = attr.Name ?? type.Name;
        var version = attr.Version;
        var properties = type.GetProperties().Where(p => p.CanRead && p.CanWrite);
        var builder = new StringBuilder();

        builder.AppendLine($"{name} <command>").EndLine();
        var commands = properties.Where(p => p.GetCustomAttribute<CommandAttribute>() != null);

        if (commands.Any())
        {
            builder.AppendLine("Commands:");

            foreach (var property in properties)
            {
                var cmd = property.GetCustomAttribute<CommandAttribute>();

                if (cmd == null) continue;

                var cmdName = cmd.Name ?? property.PropertyType.Name;
                var line = new StringBuilder();

                line.Append($"\t{name} {cmdName}\t{cmd.Description}");

                if (cmd.Aliases.Length > 0)
                {
                    line.Append($"\t[aliases: {string.Join(", ", cmd.Aliases)}]");
                }

                builder.Append(line.EndLine());
            }
        }

        var options = properties.Where(p => p.GetCustomAttribute<OptionAttribute>() != null);

        if (options.Any())
        {
            builder.AppendLine("Options:");

            foreach (var property in properties)
            {
                var option = property.GetCustomAttribute<OptionAttribute>();

                if (option == null) continue;

                var optionName = option.Name ?? property.Name;
                var line = new StringBuilder().Append('\t');

                if (option is not PositionalAttribute)
                {
                    line.Append("--");
                }

                line.Append($"{optionName}\t{option.Description}");

                if (option.Aliases.Length > 0)
                {
                    line.Append($"\t[aliases: {string.Join(", ", option.Aliases.Select(a => $"-{a}"))}]");
                }

                builder.Append(line.EndLine());
            }
        }

        return builder.ToString();
    }
}