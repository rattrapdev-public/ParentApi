using Parent.Application.ViewModels;
using Parent.Domain;

namespace Parent.Application;

public class CreateChild : ICreateChild
{
    private readonly IChildRepository _childRepository;

    public CreateChild(IChildRepository childRepository)
    {
        _childRepository = childRepository;
    }
    
    public async Task HandleAsync(NewChildViewModel viewModel)
    {
        var guardianIdentifier = new GuardianIdentifier(viewModel.GuardianId);
        var name = new Name(viewModel.FirstName, viewModel.LastName);
        var child = Child.CreateNew(guardianIdentifier, name);

        await _childRepository.Store(child);
    }
}