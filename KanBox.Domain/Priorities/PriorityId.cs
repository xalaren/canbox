namespace KanBox.Domain.Priorities;

public readonly record struct PriorityId(Guid Value)
{
    public static PriorityId Empty() => new PriorityId(Guid.Empty);
    public static PriorityId New() => new PriorityId(Guid.NewGuid());
    public static PriorityId Parse(string value) => new PriorityId(Guid.Parse(value));
}