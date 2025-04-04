namespace Rum.Data.Serialization.Sql.Annotations;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field,
    Inherited = true,
    AllowMultiple = true
)]
public class ColumnAttribute(string? Name = null, string? Type = null, string? Comment = null) : Attribute
{
    /// <summary>
    /// the column name
    /// </summary>
    public string? Name { get; private set; } = Name;

    /// <summary>
    /// the column type
    /// </summary>
    public string? Type { get; private set; } = Type;

    /// <summary>
    /// the column comment
    /// </summary>
    public string? Comment { get; private set; } = Comment;
}