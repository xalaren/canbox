using KanBox.Domain.Abstractions;
using KanBox.Domain.Primitives;

namespace KanBox.Domain.Priorities;

/// <summary>
/// Priority entity
/// </summary>
public sealed class Priority : Entity
{
    /// <summary>
    /// Max length of label
    /// </summary>
    public const int LabelMaxLength = 50;
    
    private int _importanceLevel;
    private string _label = null!;
    
    /// <summary>
    /// Identifier of Priority entity
    /// </summary>
    public PriorityId Id { get; }
    
    /// <summary>
    /// Importance level of priority
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">if value is less than 0</exception>
    public int ImportanceLevel
    {
        get => _importanceLevel;
        set
        {
            if(value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Importance level must be greater than or equal to 0");
            _importanceLevel = value;
        }
    }
    
    /// <summary>
    /// Label of priority
    /// </summary>
    /// <exception cref="ArgumentNullException">if label is null or empty or whitespace</exception>
    /// <exception cref="ArgumentOutOfRangeException">if length of label is greater than defined max length</exception>
    public string Label
    {
        get => _label;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value), "Label cannot be null or empty");

            if (value.Length > LabelMaxLength) throw new ArgumentOutOfRangeException(nameof(value), $"Label length cannot be greater than {LabelMaxLength}");
            _label = value;
        }
    }

    /// <summary>
    /// Color of priority
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// Primary constructor
    /// </summary>
    /// <param name="id">Identifier of priority</param>
    /// <param name="importanceLevel">Importance level</param>
    /// <param name="label">Label</param>
    /// <param name="color">Color</param>
    public Priority(PriorityId id, int importanceLevel, string label, Color color)
    {
        Id = id;
        ImportanceLevel = importanceLevel;
        Label = label;
        Color = color;
    }
    
    /// <summary>
    /// Primary constructor overload. Sets a new generated GUID in Id
    /// </summary>
    /// <param name="importanceLevel">Importance level</param>
    /// <param name="label">Label</param>
    /// <param name="color">Color</param>
    public Priority(int importanceLevel, string label, Color color) : this(PriorityId.New(), importanceLevel, label, color) { }
}
