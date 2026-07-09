using KanBox.Domain.Primitives;
using KanBox.Domain.Priorities;

namespace KanBox.Domain.Tests.Priorities;

[TestFixture]
public class PriorityTest
{
    private const int ValidImportanceLevel = 0;
    private const string ValidLabel = "ValidLabel";
    private readonly Color _validColor = new Color(0, 0, 0, 0);

    [Test]
    public void Constructor_ProvidedId_SetsPropertiesCorrectly()
    {
        // Arrange
        PriorityId id = PriorityId.New();
        
        // Act;
        Priority priority = new Priority(id, ValidImportanceLevel, ValidLabel, _validColor);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(priority.Id, Is.EqualTo(id));
            Assert.That(priority.Label, Is.EqualTo(ValidLabel));
            Assert.That(priority.ImportanceLevel, Is.EqualTo(ValidImportanceLevel));
            Assert.That(priority.Color, Is.EqualTo(_validColor));
        });
    }

    [Test]
    public void Constructor_NotProvidedId_SetsPropertiesCorrectly()
    {
        // Arrange
        PriorityId empty = PriorityId.Empty();
        
        // Act
        Priority priority = new Priority(ValidImportanceLevel, ValidLabel, _validColor);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(priority.Id, Is.Not.EqualTo(empty));
            Assert.That(priority.Label, Is.EqualTo(ValidLabel));
            Assert.That(priority.ImportanceLevel, Is.EqualTo(ValidImportanceLevel));
        });
    }

    [TestCaseSource(typeof(PriorityTestCaseSources), nameof(PriorityTestCaseSources.EmptyLabelTestCases))]
    public void Constructor_ProvidedEmptyTitle_ThrowsArgumentNullException(string label)
    {
        // Arrange - empty
        // Act && Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            Priority priority = new Priority(ValidImportanceLevel, label, _validColor);
        });
    }
    
    [TestCaseSource(typeof(PriorityTestCaseSources), nameof(PriorityTestCaseSources.OutOfRangeLabelTestCases))]
    public void Constructor_ProvidedOutOfRangeTitle_ThrowsArgumentOutOfRangeException(string label)
    {
        // Arrange - empty
        // Act && Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Priority priority = new Priority(ValidImportanceLevel, label, _validColor);
        });
    }
    
    
    public void Constructor_ProvidedOutOfRangeImportanceLevel_ThrowsArgumentOutOfRangeException(int importanceLevel)
    {
        // Arrange - empty
        // Act && Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Priority priority = new Priority(importanceLevel, ValidLabel, _validColor);
        });
    }

    private sealed class PriorityTestCaseSources
    {
        public static string?[] EmptyLabelTestCases => [null, string.Empty, " "];
        public static string[] OutOfRangeLabelTestCases => [GenerateString('a', Priority.LabelMaxLength + 1)];

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
