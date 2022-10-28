using Parent.Application.MessageModels;
using Parent.Domain;

namespace Parent.Application;

public class ToyPurchased : IToyPurchased
{
    private readonly IChildRepository _childRepository;

    public ToyPurchased(IChildRepository childRepository)
    {
        _childRepository = childRepository;
    }
    
    public async Task HandleAsync(PurchasedToyViewModel viewModel)
    {
        var childIdentifier = new ChildIdentifier(viewModel.ChildId);
        var toyPurchased = new Toy(viewModel.UPC, viewModel.ToyName);
        var child = await _childRepository.GetBy(childIdentifier);
        child.AddToy(toyPurchased);
        await _childRepository.Store(child);
        var child2 = await _childRepository.GetBy(childIdentifier);
    }
}