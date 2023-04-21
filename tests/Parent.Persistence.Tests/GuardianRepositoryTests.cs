using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Parent.Common;
using Parent.Domain;
using Parent.Tests.Common;
using Xunit;

namespace Parent.Persistence.Tests;

public class GuardianRepositoryTests
{
    [Fact]
    public async Task Store_should_add_guardian_to_repository()
    {
        // Arrange
        var guardian = Guardian.CreateNew(new Name("John", "Doe"), new EmailAddress("joe@doe.com"),
            new Address("100 main st", string.Empty, "Ely", "IA", "52227"));
        var builder = new DbContextOptionsBuilder<ParentDbContext>();
        builder.UseInMemoryDatabase("TestDb");
        var context = new ParentDbContext(builder.Options);

        var sut = new GuardianRepository(context);
        
        // Act

        await sut.Store(guardian);
        await context.SaveChangesAsync();
        
        // Assert

        context.Guardians.Any(x => x.Identifier.Id == guardian.Id).Should().BeTrue();
        var reconstitutedGuardian = await context.Guardians.FirstAsync(x => x.Identifier.Id == guardian.Id);
        reconstitutedGuardian.Name.Should().Be(guardian.Name);
    }
    
    [Fact]
    public async Task Store_should_update_guardian()
    {
        // Arrange
        var guardian = Guardian.CreateNew(new Name("John", "Doe"), new EmailAddress("joe@doe.com"),
            new Address("100 main st", string.Empty, "Ely", "IA", "52227"));
        var builder = new DbContextOptionsBuilder<ParentDbContext>();
        builder.UseInMemoryDatabase("TestDb");
        var context = new ParentDbContext(builder.Options);

        var sut = new GuardianRepository(context);
        await sut.Store(guardian);
        await context.SaveChangesAsync();
        guardian.Move(new Address("100 main st", string.Empty, "Main", "IA", "52227"));
        
        // Act

        await sut.Store(guardian);
        await context.SaveChangesAsync();
        
        // Assert

        context.Guardians.Any(x => x.Identifier.Id == guardian.Id).Should().BeTrue();
        var entitiesToPublish = context.ChangeTracker.Entries<IEntity>().Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any()).ToList();
        ((Guardian) entitiesToPublish.First()).Identifier.Id.Should().Be(guardian.Identifier.Id);
        var reconstitutedGuardian = await context.Guardians.FirstAsync(x => x.Identifier.Id == guardian.Id);
        reconstitutedGuardian.Address.City.Should().Be("Main");
    }

    [Theory, CustomAutoData(typeof(GuardianSpecimenBuilder))]
    public async Task Search_returns_matching_guardian_searching_by_name(Guardian guardian1, Guardian guardian2)
    {
        // Arrange
        
        var builder = new DbContextOptionsBuilder<ParentDbContext>();
        builder.UseInMemoryDatabase("TestDb");
        var context = new ParentDbContext(builder.Options);
        await context.Guardians.AddAsync(guardian1);
        await context.Guardians.AddAsync(guardian2);
        await context.SaveChangesAsync();
        var sut = new GuardianRepository(context);
        
        // Act
        
        var result = await sut.Search(guardian1.Name.FirstName);
        
        // Assert
        
        result.Should().Contain(guardian1);
    }
    
    [Theory, CustomAutoData(typeof(GuardianSpecimenBuilder))]
    public async Task Search_returns_matching_guardian_searching_by_address(Guardian guardian1, Guardian guardian2)
    {
        // Arrange
        
        var builder = new DbContextOptionsBuilder<ParentDbContext>();
        builder.UseInMemoryDatabase("TestDb");
        var context = new ParentDbContext(builder.Options);
        await context.Guardians.AddAsync(guardian1);
        await context.Guardians.AddAsync(guardian2);
        await context.SaveChangesAsync();
        var sut = new GuardianRepository(context);
        
        // Act
        
        var result = await sut.Search(guardian1.Address.City);
        
        // Assert
        
        result.Should().Contain(guardian1);
    }
    
    [Theory, CustomAutoData(typeof(GuardianSpecimenBuilder))]
    public async Task Search_returns_empty_for_no_matching_results(Guardian guardian1, Guardian guardian2)
    {
        // Arrange
        
        var builder = new DbContextOptionsBuilder<ParentDbContext>();
        builder.UseInMemoryDatabase("TestDb");
        var context = new ParentDbContext(builder.Options);
        await context.Guardians.AddAsync(guardian1);
        await context.Guardians.AddAsync(guardian2);
        await context.SaveChangesAsync();
        var sut = new GuardianRepository(context);
        
        // Act
        
        var result = await sut.Search("Jane");
        
        // Assert
        
        result.Should().BeEmpty();
    }
}