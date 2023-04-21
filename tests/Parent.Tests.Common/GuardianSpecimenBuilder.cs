using AutoFixture;
using AutoFixture.Kernel;
using Parent.Domain;

namespace Parent.Tests.Common;

public class GuardianSpecimenBuilder : SpecimenBuilderBase<Guardian>
{
    protected override Guardian CreateSpecimen(ISpecimenContext context)
    {
        var name = context.Create<Name>();
        var emailAddress = context.Create<EmailAddress>();
        var address = context.Create<Address>();
        var guardianIdentifier = GuardianIdentifier.CreateNew();
        var guardian = Guardian.Reconstitute(guardianIdentifier, name, emailAddress, address);
        return guardian;
    }
}