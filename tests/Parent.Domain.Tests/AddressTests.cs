using Parent.Tests.Common;
using Shouldly;

namespace Parent.Domain.Tests;

public class AddressTests
{
    [Theory, CustomAutoData]
    public void Constructor_sets_values(string address1, string address2, string city, string state, string zip)
    {
        // Act

        var result = new Address(address1, address2, city, state, zip);
        
        // Assert
        
        result.Address1.ShouldBe(address1);
        result.Address2.ShouldBe(address2);
        result.City.ShouldBe(city);
        result.State.ShouldBe(state);
        result.Zip.ShouldBe(zip);
    }

    [Theory, CustomAutoData]
    public void Constructor_throws_with_null_address1(string address2, string city, string state, string zip)
    {
        // Act
        
        var result = () => new Address(null, address2, city, state, zip);
        
        // Assert
        
        result.ShouldThrow<ArgumentNullException>();
    }

    [Theory, CustomAutoData]
    public void Constructor_throws_with_null_city(string address1, string address2, string state, string zip)
    {
        // Act
        
        var result = () => new Address(address1, address2, null, state, zip);
        
        // Assert
        
        result.ShouldThrow<ArgumentNullException>();
    }

    [Theory, CustomAutoData]
    public void Contains_returns_true_for_matching_city(Address address)
    {
        // Act
        
        var result = address.Contains(address.City);
        
        // Assert
        
        result.ShouldBeTrue();
    }
    
    [Theory, CustomAutoData]
    public void Contains_returns_false_for_non_matching_city(Address address)
    {
        // Act
        
        var result = address.Contains("not the city");
        
        // Assert
        
        result.ShouldBeFalse();
    }
    
    [Theory, CustomAutoData]
    public void Contains_returns_true_for_matching_city_with_different_case(Address address)
    {
        // Act
        
        var result = address.Contains(address.City.ToUpper());
        
        // Assert
        
        result.ShouldBeTrue();
    }
    
    [Theory, CustomAutoData]
    public void Contains_returns_true_for_matching_city_with_lower_case(Address address)
    {
        // Act
        
        var result = address.Contains(address.City.ToLower());
        
        // Assert
        
        result.ShouldBeTrue();
    }

    [Theory, CustomAutoData]
    public void Contains_returns_true_for_matching_state(Address address)
    {
        // Act
        
        var result = address.Contains(address.State);
        
        // Assert
        
        result.ShouldBeTrue();
    }
    
    [Theory, CustomAutoData]
    public void Contains_returns_false_for_non_matching_state(Address address)
    {
        // Act
        
        var result = address.Contains("not the state");
        
        // Assert
        
        result.ShouldBeFalse();
    }

    [Theory, CustomAutoData]
    public void Constructor_throws_exception_with_null_state(Address address)
    {
        // Act
        
        var result = () => new Address(address.Address1, address.Address2, address.City, null, address.Zip);
        
        // Assert
        
        result.ShouldThrow<ArgumentNullException>();
    }
    
    [Theory, CustomAutoData]
    public void Constructor_throws_exception_with_null_zip(Address address)
    {
        // Act
        
        var result = () => new Address(address.Address1, address.Address2, address.City, address.State, null);
        
        // Assert
        
        result.ShouldThrow<ArgumentNullException>();
    }
}