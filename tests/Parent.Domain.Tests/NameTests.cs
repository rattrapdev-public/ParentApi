using Parent.Tests.Common;
using Shouldly;

namespace Parent.Domain.Tests;

public class NameTests
{
    [Theory, CustomAutoData]
    public void Contains_returns_true_for_matching_FirstName(Name name)
    {
        // Act

        var result = name.Contains(name.FirstName);

        // Assert

        result.ShouldBeTrue();
    }
    
    [Theory, CustomAutoData]
    public void Contains_returns_false_for_non_matching_FirstName(Name name)
    {
        // Act

        var result = name.Contains("NotTheFirstName");

        // Assert

        result.ShouldBeFalse();
    }
    
    [Theory, CustomAutoData]
    public void Contains_returns_true_for_matching_LastName(Name name)
    {
        // Act

        var result = name.Contains(name.LastName);

        // Assert

        result.ShouldBeTrue();
    }
    
    [Theory, CustomAutoData]
    public void Contains_returns_false_for_non_matching_LastName(Name name)
    {
        // Act

        var result = name.Contains("NotTheLastName");

        // Assert

        result.ShouldBeFalse();
    }
}