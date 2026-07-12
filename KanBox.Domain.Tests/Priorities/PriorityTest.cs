using KanBox.Domain.Primitives;
using KanBox.Domain.Priorities;

namespace KanBox.Domain.Tests.Priorities;

[TestFixture]
public class PriorityTest
{
    private const int ValidOrder = 0;
    private const string ValidLabel = "ValidLabel";
    private readonly Color _validColor = new Color(0, 0, 0, 0);

    [Test]
    public void Constructor_ProvidedId_SetsPropertiesCorrectly()
    {
        // Arrange
        PriorityId id = PriorityId.New();
        
        // Act;
        Priority priority = new Priority(id, ValidOrder, ValidLabel, _validColor);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(priority.Id, Is.EqualTo(id));
            Assert.That(priority.Label, Is.EqualTo(ValidLabel));
            Assert.That(priority.Order, Is.EqualTo(ValidOrder));
            Assert.That(priority.Color, Is.EqualTo(_validColor));
        });
    }

    [Test]
    public void Constructor_NotProvidedId_SetsPropertiesCorrectly()
    {
        // Arrange
        PriorityId empty = PriorityId.Empty();
        
        // Act
        Priority priority = new Priority(ValidOrder, ValidLabel, _validColor);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(priority.Id, Is.Not.EqualTo(empty));
            Assert.That(priority.Label, Is.EqualTo(ValidLabel));
            Assert.That(priority.Order, Is.EqualTo(ValidOrder));
        });
    }

    [TestCaseSource(typeof(PriorityTestCaseSources), nameof(PriorityTestCaseSources.EmptyLabelTestCases))]
    public void Constructor_ProvidedEmptyLabel_ThrowsArgumentNullException(string label)
    {
        // Arrange - empty
        // Act && Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            Priority priority = new Priority(ValidOrder, label, _validColor);
        });
    }
    
    [TestCaseSource(typeof(PriorityTestCaseSources), nameof(PriorityTestCaseSources.LongLabelTestCases))]
    public void Constructor_ProvidedLongLabel_ThrowsArgumentOutOfRangeException(string label)
    {
        // Arrange - empty
        // Act && Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Priority priority = new Priority(ValidOrder, label, _validColor);
        });
    }
    
    [TestCase(-1)]
    public void Constructor_ProvidedNegativeOrder_ThrowsArgumentOutOfRangeException(int order)
    {
        // Arrange - empty
        Priority priority = new Priority(ValidOrder, ValidLabel, _validColor);
        
        // Act && Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            priority.Order = order;
        });
    }
    
    [TestCaseSource(typeof(PriorityTestCaseSources), nameof(PriorityTestCaseSources.EmptyLabelTestCases))]
    public void LabelSetter_ProvidedEmptyLabel_ThrowsArgumentNullException(string label)
    {
        // Arrange
        Priority priority = new Priority(ValidOrder, ValidLabel, _validColor);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            priority.Label = label;
        });
    }

    [TestCaseSource(typeof(PriorityTestCaseSources), nameof(PriorityTestCaseSources.LongLabelTestCases))]
    public void LabelSetter_ProvidedLongLabel_ThrowsArgumentOutOfRangeException(string label)
    {
        // Arrange
        Priority priority = new Priority(ValidOrder, ValidLabel, _validColor);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            priority.Label = label;
        });
    }



    private sealed class PriorityTestCaseSources
    {
        public static string?[] EmptyLabelTestCases => [null, string.Empty, " "];
        public static string[] LongLabelTestCases => [GenerateString('a', Priority.LabelMaxLength + 1)];

        private static string GenerateString(char letter, int length)
        {
            Span<char> charSpan = new char[length];

            for (int i = 0; i < length; i++)
            {
                charSpan[i] = letter;
            }
            
            return new string(charSpan);
        }
    }
}
