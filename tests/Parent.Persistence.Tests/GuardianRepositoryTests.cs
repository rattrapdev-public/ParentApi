using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Parent.Common;
using Parent.Domain;
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
}