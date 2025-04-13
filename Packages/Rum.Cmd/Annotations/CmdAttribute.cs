namespace Rum.Cmd.Annotations;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct,
    Inherited = true
)]
public abstract class CmdAttribute : Attribute
{

}