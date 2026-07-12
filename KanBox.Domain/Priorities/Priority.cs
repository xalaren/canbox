using KanBox.Domain.Abstractions;
using KanBox.Domain.Constants;
using KanBox.Domain.Primitives;

namespace KanBox.Domain.Priorities;

/// <summary>
/// Priority entity
/// </summary>
public sealed class Priority : Entity<PriorityId>
{
    /// <summary>
    /// Max length of label
    /// </summary>
    public const int LabelMaxLength = 50;
    
    private int _order;
    private string _label = null!;

    /// <summary>
    /// Identifier of Priority entity
    /// </summary>
    public override PriorityId Id { get; }

    /// <summary>
    /// Order value of priority
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">if value is less than 0</exception>
    public int Order
    {
        get => _order;
        set
        {
            if(value < 0) throw new ArgumentOutOfRangeException(nameof(value), string.Format(ErrorMessages.Templates.MustBeGreaterOrEqual, nameof(Order), 0));
            _order = value;
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
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value), string.Format(ErrorMessages.Templates.NullOrWhiteSpace, nameof(Label)));

            if (value.Length > LabelMaxLength) throw new ArgumentOutOfRangeException(nameof(value), string.Format(ErrorMessages.Templates.LengthCannotBeGreater, nameof(Label), LabelMaxLength));
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
    /// <param name="order">Order</param>
    /// <param name="label">Label</param>
    /// <param name="color">Color</param>
    public Priority(PriorityId id, int order, string label, Color color)
    {
        Id = id;
        Order = order;
        Label = label;
        Color = color;
    }
    
    /// <summary>
    /// Primary constructor overload. Sets a new generated GUID in Id
    /// </summary>
    /// <param name="importanceLevel">Importance level</param>
    /// <param name="label">Label</param>
    /// <param name="color">Color</param>
    public Priority(int order, string label, Color color) : this(PriorityId.New(), order, label, color) { }
}
