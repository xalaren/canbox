using KanBox.Domain.Primitives;

namespace KanBox.Domain.Tests.Primitives;

[TestFixture]
public class ColorTest
{
    [TestCaseSource(typeof(ColorTestCaseSources), nameof(ColorTestCaseSources.EmptyHexStrings))]
    public void Constructor_ProvidedNullOrEmptyHex_ThrowsArgumentNullException(string hex)
    {
        // Arrange - empty
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            Color color = new Color(hex);
        });
    }
    
    [TestCase("#FFAA00BBCC")]
    [TestCase("GGG")]
    [TestCase("#12345")]
    [TestCase("FFA500")]
    public void Constructor_ProvidedInvalidHex_ThrowsFormatException(string hex)
    {
        // Arrange - empty
        // Act & Assert
        Assert.Throws<FormatException>(() =>
        {
            Color color = new Color(hex);
        });
    }
    
    [TestCase("#FFA500", "#FFA500")]
    [TestCase("#ffa500", "#FFA500")]
    [TestCase("#FAB", "#FAB")]
    [TestCase("#faB", "#FAB")]
    [TestCase("#FAB0", "#FAB0")]
    [TestCase("#fab0", "#FAB0")]
    [TestCase("#FFAA00BB", "#FFAA00BB")]
    [TestCase("#ffaa00bb", "#FFAA00BB")]
    public void Constructor_ProvidedValidHex_SetHexPropertyCorrectly(string hex, string expected)
    {
        // Arrange - empty
        
        // Act & Assert
        Color color = new Color(hex);
        Assert.That(color.Hex, Is.EqualTo(expected));
    }
    
    [TestCase(255, 255, 255, 255, "#FFFFFFFF")]
    [TestCase(0, 0, 0, 0, "#00000000")]
    [TestCase(123, 80, 40, 30, "#7B50281E")]
    public void Constructor_ProvidedRgba_SetHexPropertyCorrectly(byte red, byte green, byte blue, byte alpha, string expected)
    {
        // Arrange - empty
        
        // Act
        Color color = new Color(red, green, blue, alpha);
        
        // Assert
        Assert.That(color.Hex, Is.EqualTo(expected));
    }
    
    [TestCase("#FFFFFF", "#FFFFFF")]
    [TestCase("#000", "#000")]
    public void Equals_ProvidedTwoHexStrings_ReturnsTrue(string hex1, string hex2)
    {
        // Arrange
        Color color1 = new Color(hex1);
        Color color2 = new Color(hex2);
        
        // Act
        bool result = color1.Equals(color2);
        
        // Assert
        Assert.That(result, Is.True);
    }

    private sealed class ColorTestCaseSources
    {
        public static string?[] EmptyHexStrings => [null, string.Empty, " "];
    }
}