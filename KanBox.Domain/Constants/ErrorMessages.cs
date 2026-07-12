namespace KanBox.Domain.Constants;

public sealed partial class ErrorMessages
{
    public sealed class Templates
    {
        public const string NullOrWhiteSpace = "{0} cannot be null or whitespace";
        public const string NullOrEmpty = "{0} cannot be null or empty";
        public const string MustBeGreaterOrEqual = "{0} must be greater than or equal to {1}";
        public const string LengthCannotBeGreater = "{0} length cannot be greater than {1}";
    }
    public sealed class Color
    {
        public const string NullOrWhiteSpace = "Hex string cannot be null or whitespace";
        public const string NotStartsWithHexCharacter = "Provided string must starts with a hex character";
        public const string NotMatchHexFormat = "Provided string does not match hex format";
    }
}