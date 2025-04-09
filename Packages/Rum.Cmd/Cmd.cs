using Rum.Cmd.Options;

namespace Rum.Cmd;

public static partial class Cmd
{
    public static Command.Builder Command(string name) => new(name);
    public static Named.Builder Option() => new();
    public static Positional.Builder Positional() => new();
}