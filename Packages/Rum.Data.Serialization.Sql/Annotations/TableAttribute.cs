namespace Rum.Data.Serialization.Sql.Annotations;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct,
    Inherited = true,
    AllowMultiple = true
)]
public class TableAttribute(string? Name = null, string? Comment = null) : Attribute
{
    /// <summary>
    /// the table name
    /// </summary>
    public string? Name { get; private set; } = Name;

    /// <summary>
    /// the table comment
    /// </summary>
    public string? Comment { get; private set; } = Comment;
}