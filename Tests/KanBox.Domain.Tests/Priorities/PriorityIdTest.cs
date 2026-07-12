using KanBox.Domain.Priorities;

namespace KanBox.Domain.Tests.Priorities;

[TestFixture]
public class PriorityIdTest
{
    [TestCaseSource(typeof(PriorityIdTestTestCaseSources), nameof(PriorityIdTestTestCaseSources.DifferentGuids))]
    public void Constructor_ProvidedId_SetsValueCorrectly(Guid id)
    {
        // Arrange - empty
        // Act 
        PriorityId priority = new PriorityId(id);

        // Assert
        Assert.That(priority.Value, Is.EqualTo(id));
    }

    [Test]
    public void EmptyProperty_ReturnsPriorityIdWithGuidEmptyValue()
    {
        // Arrange
        Guid emptyId = Guid.Empty;
        
        // Act
        PriorityId priority = PriorityId.Empty();
        
        // Assert
        Assert.That(priority.Value, Is.EqualTo(emptyId));
    }
    
    [Test]
    public void New_ReturnsPriorityIdWithNotEmptyValue()
    {
        // Arrange
        Guid emptyId = Guid.Empty;
        
        // Act
        PriorityId priority = PriorityId.New();
        
        // Assert
        Assert.That(priority.Value, Is.Not.EqualTo(emptyId));
    }
    
    [Test]
    public void Parse_ProvidedValidString_ReturnsPriorityIdWithValidValue()
    {
        // Arrange
        string validGuid = "e5f706f3-e475-4c8f-af35-01dd68f874f8";
        Guid validId = Guid.Parse(validGuid);
        
        // Act
        PriorityId priority = PriorityId.Parse(validGuid);
        
        // Assert
        Assert.That(priority.Value, Is.EqualTo(validId));
    }
    
    [TestCase("")]
    [TestCase("notValid")]
    [TestCase("123")]
    [TestCase("e5f706f3-e475-4c8f-af35-01dd")]
    public void Parse_ProvidedInvalidString_ReturnsPriorityIdWithValidValue(string invalidGuid)
    {
        // Arrange - empty
        // Act & Assert
        Assert.Throws<FormatException>(() => PriorityId.Parse(invalidGuid));
    }
    
    private sealed class PriorityIdTestTestCaseSources
    {
        public static Guid[] DifferentGuids => [Guid.Empty, Guid.NewGuid()];
    }
}

