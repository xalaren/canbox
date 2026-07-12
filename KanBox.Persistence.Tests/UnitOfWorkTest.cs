using KanBox.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace KanBox.Persistence.Tests;

[TestFixture]
public class UnitOfWorkTest
{
    private DbContextOptions<TestDbContext> _options = null!;
    private readonly Guid _validGuid = Guid.NewGuid();
    private readonly int _validValue = 0;
    
    [SetUp]
    public void SetUp()
    {
        _options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }
    
    [Test]
    public async Task SaveChangesAsync_SetsCreatedOnUtc_ForAddedEntities()
    {
        // Arrange
        await using var context = new TestDbContext(_options);
        
        IUnitOfWork unitOfWork = new UnitOfWork(context);
        TestAuditableEntity entity = new()
        {
            Id = _validGuid,
            Value = _validValue,
        };
        
        await context.AddAsync(entity);
        
        // Act
        await unitOfWork.SaveChangesAsync();
        
        // Assert
        Assert.That(entity.CreatedOnUtc, Is.Not.EqualTo(default(DateTime)));
        Assert.That(entity.ModifiedOnUtc, Is.Not.EqualTo(default(DateTime)));
    }
    
    [Test]
    public async Task SaveChangesAsync_SetsModifiedOnUtc_ForModifiedEntities()
    {
        // Arrange
        await using var context = new TestDbContext(_options);
        
        IUnitOfWork unitOfWork = new UnitOfWork(context);
        TestAuditableEntity entity = new()
        {
            Id = _validGuid,
            Value = _validValue
        };
        
        await context.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        
        DateTime modifiedOn = entity.ModifiedOnUtc;
        
        // Act
        entity.Value = _validValue + 1;
        context.TestAuditableEntities.Update(entity);
        
        await unitOfWork.SaveChangesAsync();
        
        // Assert
        Assert.That(entity.ModifiedOnUtc, Is.Not.EqualTo(default(DateTime)));
        Assert.That(entity.ModifiedOnUtc, Is.Not.EqualTo(modifiedOn));
    }
}

public class TestDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TestAuditableEntity> TestAuditableEntities { get; init; }
}

public class TestAuditableEntity : IAuditable
{
    public Guid Id { get; init; }
    
    public int Value { get; set; }
    public DateTime CreatedOnUtc { get; init; }
    public DateTime ModifiedOnUtc { get; init; }
    
}