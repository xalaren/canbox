using KanBox.Domain.Abstractions;
using KanBox.Domain.Constants;

namespace KanBox.Domain.Primitives;

/// <summary>
/// Color value object stored in a HEX format
/// </summary>
public readonly struct Color : IEquatable<Color>
{
    
    /// <summary>
    /// Primary constructor (format string to upper HEX format like #FF00FF)
    /// </summary>
    /// <param name="hex">Color in a hex format</param>
    public Color(string hex)
    {
        Hex = Format(hex);
    }
    
    /// <summary>
    /// Overloaded constructor for initializing from RGBA format
    /// </summary>
    /// <param name="red">Red value</param>
    /// <param name="green">Green value</param>
    /// <param name="blue">Blue value</param>
    /// <param name="alpha">Alpha value</param>
    public Color(byte red, byte green, byte blue, byte alpha)
    {
        Hex = Format(red, green, blue, alpha);
    }
    
    /// <summary>
    /// Max length of HEX string
    /// </summary>
    public const int MaxHexLength = 10;
    
    /// <summary>
    /// Color string value in HEX format
    /// </summary>
    public string Hex { get; }

    public bool Equals(Color other)
    {
        return 
            GetType() == other.GetType() && 
            string.Equals
            (
                Hex, 
                other.Hex, 
                StringComparison.InvariantCultureIgnoreCase
            );
    }

    public override bool Equals(object? obj)
    {
        return 
            obj is not null && 
            GetType() == obj.GetType() && 
            string.Equals
            (
                Hex, 
                ((Color)obj).Hex, 
                StringComparison.InvariantCultureIgnoreCase
            );

    }
    public override int GetHashCode() => ColorHashCodeMultiplier * Hex.GetHashCode();

    public static bool operator ==(Color first, Color next) => first.Equals(next);
    
    public static bool operator !=(Color first, Color next) => !first.Equals(next);
    
    private const int ColorHashCodeMultiplier = 41;
    private const int HexCharacterIndex = 1;
    private const int RgbHexLength = 3;
    private const int RgbaHexLength = 4;
    private const int DoubledRgbHexLength = 6;
    private const int DoubledRgbaHexLength = 8;
    
    private string Format(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
        {
            throw new ArgumentNullException(nameof(hex), ErrorMessages.Color.NullOrWhiteSpace);
        }

        if (!hex.StartsWith("#"))
        {
            throw new FormatException(ErrorMessages.Color.NotStartsWithHexCharacter);
        }

        ReadOnlySpan<char> cleaned = hex
            .ToUpper()
            .AsSpan()
            .Trim();

        ReadOnlySpan<char> sliced = cleaned.Slice(HexCharacterIndex);
        int length = sliced.Length;

        if (length is not RgbHexLength and
            not RgbaHexLength and
            not DoubledRgbHexLength and
            not DoubledRgbaHexLength)
        {
            throw new FormatException(ErrorMessages.Color.NotMatchHexFormat);
        }

        foreach (char character in sliced)
        {
            if (!IsCharacterInHex(character))
            {
                throw new FormatException(ErrorMessages.Color.NotMatchHexFormat);
            }
        }

        return new string(cleaned);
    }

    private string Format(byte red, byte green, byte blue, byte alpha)
    {
        const string hexFormat = "X2";
        var redHex = red.ToString(hexFormat);
        var greenHex = green.ToString(hexFormat);
        var blueHex = blue.ToString(hexFormat);
        var alphaHex = alpha.ToString(hexFormat);

        return $"#{redHex}{greenHex}{blueHex}{alphaHex}";
    }

    private bool IsCharacterInHex(char character) => character is >= '0' and <= '9' or >= 'a' and <= 'f' or >= 'A' and <= 'F';
}