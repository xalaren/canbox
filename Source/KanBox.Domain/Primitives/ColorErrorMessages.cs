namespace KanBox.Domain.Primitives;

public sealed class ColorErrorMessages
{
    public const string NullOrWhiteSpace = "Hex string cannot be null or whitespace";
    public const string NotStartsWithHexCharacter = "Hex string must start with a hex character";
    public const string DoesNotMatchHexFormat = "Provided string does not match hex format";
}