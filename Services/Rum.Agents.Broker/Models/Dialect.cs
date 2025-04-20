using Rum.Text;

namespace Rum.Agents.Broker.Models;

public class Dialect(string value) : StringEnum(value)
{
    public static readonly Dialect Mcp = new("mcp");
    public bool IsMcp => Value.Equals(Mcp);

    public static readonly Dialect A2A = new("a2a");
    public bool IsA2A => Value.Equals(A2A);
}