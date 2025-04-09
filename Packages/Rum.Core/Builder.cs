namespace Rum.Core;

/// <summary>
/// Any Builder
/// </summary>
/// <typeparam name="T">the type to build</typeparam>
public interface IBuilder<T>
{
    /// <summary>
    /// Build
    /// </summary>
    public T Build();
}
