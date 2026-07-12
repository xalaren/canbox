namespace KanBox.Domain.Abstractions;

/// <summary>
/// Require instance validation interface 
/// </summary>
public interface IValidatable
{
    /// <summary>
    /// Validate the instance
    /// </summary>
    /// <returns><br/><b>True</b> - if instance is valid</returns>
    /// <returns><br/><b>False</b> - if instance is not valid</returns>
    bool IsValid();
}