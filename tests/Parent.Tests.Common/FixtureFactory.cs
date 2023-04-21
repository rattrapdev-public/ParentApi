using System.Reflection;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using BindingFlags = System.Reflection.BindingFlags;

namespace Parent.Tests.Common;

public class FixtureFactory
{
    public static IFixture Create(params Type[] builderTypes)
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoMoqCustomization());
        
        foreach (var builderType in builderTypes)
        {
            if (!builderType.IsAssignableFrom(typeof(ISpecimenBuilder)))
            {
                var constructor = builderType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null,
                    CallingConventions.HasThis, Type.EmptyTypes, null);
                if (constructor == null) continue;
                var specimenBuilder = (ISpecimenBuilder) constructor.Invoke(Array.Empty<object>());
                fixture.Customizations.Add(specimenBuilder);
            }
            else
            {
                throw new InvalidOperationException("Type does not implement ISpecimenBuilder");
            }
        }
        
        return fixture;
    }
}