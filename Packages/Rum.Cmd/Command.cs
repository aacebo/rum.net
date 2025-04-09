using System.Reflection;
using System.Text.Json.Serialization;

using Rum.Cmd.Annotations;
using Rum.Cmd.Options;
using Rum.Core;
using Rum.Core.Json;

namespace Rum.Cmd;

/// <summary>
/// Command
/// </summary>
[JsonConverter(typeof(TrueTypeJsonConverter<ICommand>))]
public interface ICommand
{
    /// <summary>
    /// the command name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// method used to determine if the command
    /// matches the name or alias provided
    /// </summary>
    public bool Select(string nameOrAlias);

    /// <summary>
    /// execute the command
    /// </summary>
    public IResult<T> Run<T>(params string[] args) where T : new();
}

/// <summary>
/// Command
/// </summary>
public class Command : ICommand
{
    [JsonPropertyName("name")]
    [JsonPropertyOrder(0)]
    public string Name { get; }

    [JsonPropertyName("alias")]
    [JsonPropertyOrder(1)]
    public IList<string> Aliases { get; set; }

    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    public string? Description { get; set; }

    [JsonPropertyName("usage")]
    [JsonPropertyOrder(3)]
    public string? Usage { get; set; }

    [JsonPropertyName("options")]
    [JsonPropertyOrder(4)]
    public IDictionary<string, IOption> Options { get; set; }

    [JsonPropertyName("commands")]
    [JsonPropertyOrder(5)]
    public IDictionary<string, ICommand> Commands { get; set; }

    [JsonIgnore]
    public Func<object, Task>? Handler;

    [JsonIgnore]
    public IList<INamedOption> NamedOptions =>
        Options.Values
            .Where(o => o is INamedOption)
            .Select(o => (INamedOption)o)
            .ToList();

    [JsonIgnore]
    public IList<IPositionalOption> PositionalOptions =>
        Options.Values
            .Where(o => o is IPositionalOption)
            .Select(o => (IPositionalOption)o)
            .OrderBy(o => o.Index)
            .ToList();

    public Command(string name)
    {
        Name = name;
        Aliases = [];
        Options = new Dictionary<string, IOption>();
        Commands = new Dictionary<string, ICommand>();
    }

    public static Command Create<T>(Func<T, Task> run)
    {
        var type = typeof(T);
        var properties = type.GetProperties().Where(p => p.CanRead && p.CanWrite);
        var attribute = type.GetCustomAttribute<CommandAttribute>() ?? throw new Exception($"type '{type.Name}' must use the 'CommandAttribute'");
        var aliasesAttribute = type.GetCustomAttribute<Annotations.Command.AliasesAttribute>();
        var builder = Cmd.Command(attribute.Name ?? type.Name).Run(run);

        foreach (var property in properties)
        {
            
        }

        return builder.Build();
    }

    public bool Select(string nameOrAlias)
    {
        return Name == nameOrAlias || Aliases.Any(alias => alias == nameOrAlias);
    }

    public IResult<T> Run<T>(params string[] args) where T : new()
    {
        var cmd = new T();
        var properties = typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite);
        var named = NamedOptions;
        var i = 0;

        foreach (var positional in PositionalOptions)
        {
            if (!positional.Select(i.ToString()))
            {
                return Result<T>.Err($"positional option at index {positional.Index} not satisfied");
            }

            var property = properties.Where(p =>
            {
                var attr = p.GetCustomAttribute<PositionalAttribute>();
                return attr != null && attr.Select(positional.Index);
            }).FirstOrDefault();

            if (property == null)
            {
                return Result<T>.Err($"positional property at index {positional.Index} not found on type '{typeof(T).Name}'");
            }

            var res = positional.Parse(args[i]);

            if (res.Error != null)
            {
                return Result<T>.Err(res.Error);
            }

            property.SetValue(cmd, res.Value);
            i++;
        }

        foreach (var part in args)
        {
            var arg = part.Trim();
            
            if (arg.StartsWith("--"))
            {
                var parts = arg.TrimStart('-', '-').Split('=', 1);
                var name = parts.First();
                var value = parts.Length > 1 ? parts[1] : null;
                var option = Options.Values.Where(o => o.Select(name)).FirstOrDefault();

                if (option == null)
                {
                    throw new KeyNotFoundException($"option '{name}' not found");
                }

                var property = properties.Where(p =>
                {
                    var attr = p.GetCustomAttribute<OptionAttribute>();
                    return attr != null && attr.Select(name);
                }).FirstOrDefault();

                if (property == null)
                {
                    throw new Exception($"property '{name}' not found on type '{typeof(T).Name}'");
                }

                var res = option.Parse(value);

                if (res.Error != null)
                {
                    return Result<T>.Err(res.Error);
                }

                property.SetValue(cmd, res.Value);
            }
            else if (arg.StartsWith('-'))
            {
                var parts = arg.TrimStart('-').Split('=', 1);
                var alias = parts.First();
                var value = parts.Length > 1 ? parts[1] : null;
                var option = Options.Values.Where(o => o.Select(alias)).FirstOrDefault();

                if (option == null)
                {
                    throw new KeyNotFoundException($"option with alias '{alias}' not found");
                }

                var property = properties.Where(p =>
                {
                    var attr = p.GetCustomAttribute<Annotations.Option.AliasesAttribute>();
                    return attr != null && attr.Select(alias);
                }).FirstOrDefault();

                if (property == null)
                {
                    throw new Exception($"property '{alias}' not found on type '{typeof(T).Name}'");
                }

                var res = option.Parse(value);

                if (res.Error != null)
                {
                    return Result<T>.Err(res.Error);
                }

                property.SetValue(cmd, res.Value);
            }
        }

        return Result<T>.Ok(cmd);
    }

    public class Builder(string name) : IBuilder<Command>
    {
        private readonly Command _value = new(name);

        public Builder Aliases(params string[] aliases)
        {
            foreach (var alias in aliases)
            {
                _value.Aliases.Add(alias);
            }

            return this;
        }

        public Builder Usage(string? usage = null)
        {
            _value.Usage = usage;
            return this;
        }

        public Builder Description(string description)
        {
            _value.Description = description;
            return this;
        }

        public Builder Option(INamedOption option)
        {
            _value.Options[option.Name] = option;
            return this;
        }

        public Builder Option(string name, INamedOption option)
        {
            option.Name = name;
            _value.Options[name] = option;
            return this;
        }

        public Builder Positional(IPositionalOption option)
        {
            _value.Options[option.Index.ToString()] = option;
            return this;
        }

        public Builder Positional(int index, IPositionalOption option)
        {
            option.Index = index;
            _value.Options[index.ToString()] = option;
            return this;
        }

        public Builder Command(ICommand command)
        {
            _value.Commands[command.Name] = command;
            return this;
        }

        public Builder Run<TArgs>(Func<TArgs, Task> handler)
        {
            _value.Handler = Task (args) => handler((TArgs)args);
            return this;
        }

        public Command Build() => _value;
    }
}
