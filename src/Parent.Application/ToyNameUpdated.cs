using Parent.Application.MessageModels;
using Parent.Domain;

namespace Parent.Application;

public class ToyNameUpdated : IToyNameUpdated
{
    private readonly IChildRepository _childRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ToyNameUpdated(IChildRepository childRepository, IUnitOfWork unitOfWork)
    {
        _childRepository = childRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task HandleAsync(ToyNameChangedViewModel evt)
    {
        var childrenToUpdate = (await _childRepository.GetByOwnedToy(evt.UPC)).ToList();
        var updatedToy = new Toy(evt.UPC, evt.UpdatedName);
        foreach (var child in childrenToUpdate)
        {
            child.UpdateToy(updatedToy);
            await _childRepository.Store(child);
        }

        await _unitOfWork.Commit();
    }
}