using Parent.Application.ViewModels;
using Parent.Domain;

namespace Parent.Application;

public class CreateChild : ICreateChild
{
    private readonly IChildRepository _childRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateChild(IChildRepository childRepository, IUnitOfWork unitOfWork)
    {
        _childRepository = childRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task HandleAsync(NewChildViewModel viewModel)
    {
        var guardianIdentifier = new GuardianIdentifier(viewModel.GuardianId);
        var name = new Name(viewModel.FirstName, viewModel.LastName);
        var child = Child.CreateNew(guardianIdentifier, name);

        await _childRepository.Store(child);
        await _unitOfWork.Commit();
    }
}