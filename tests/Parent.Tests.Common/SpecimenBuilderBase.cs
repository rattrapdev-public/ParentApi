using AutoFixture.Kernel;

namespace Parent.Tests.Common;

public abstract class SpecimenBuilderBase<T> : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        var requestType = request as Type;

        if (requestType != null && requestType == typeof(T))
        {
            return CreateSpecimen(context);
        }

        return new NoSpecimen();
    }
    
    protected abstract T CreateSpecimen(ISpecimenContext context);
}