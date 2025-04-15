namespace Rum.Cmd;

public class PositionalAttribute(int Index = 0) : OptionAttribute($"${Index}")
{

}