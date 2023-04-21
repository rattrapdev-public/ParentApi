using AutoFixture.Xunit2;

namespace Parent.Tests.Common;

public class CustomAutoData : AutoDataAttribute
{
    public CustomAutoData(params Type[] builderTypes) : base(() => FixtureFactory.Create(builderTypes))
    {
    }
}