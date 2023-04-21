using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Parent.Domain;
using Xunit;

namespace Parent.Persistence.Tests;

public class ChildRepositoryTests
{
    [Fact]
    public async Task Store_stores_toys_with_new_child()
    {
        // Arrange

        var guardian = Guardian.CreateNew(new Name("John", "Doe"), new EmailAddress("joe@doe.com"),
            new Address("100 main st", string.Empty, "Ely", "IA", "52227"));
        var child = Child.CreateNew(guardian.Identifier, new Name("John", "Doe"));
        child.AddToy(new Toy(Guid.NewGuid().ToString(), "Toy 1"));
        var builder = new DbContextOptionsBuilder<ParentDbContext>();
        builder.UseSqlite(@"Data Source = childtest.db");
        var context = new ParentDbContext(builder.Options);
        await context.Database.MigrateAsync();

        var sut = new ChildRepository(context);
        await context.Guardians.AddAsync(guardian);
        await context.SaveChangesAsync();

        // Act

        await sut.Store(child);
        await context.SaveChangesAsync();

        // Assert

        var builder2 = new DbContextOptionsBuilder<ParentDbContext>();
        builder2.UseSqlite(@"Data Source = childtest.db");
        var context2 = new ParentDbContext(builder2.Options);
        var childRepository = new ChildRepository(context2);
        var reconstitutedChild = await childRepository.GetBy(child.Identifier);
        reconstitutedChild.ToyBox.Any().Should().BeTrue();
    }
}