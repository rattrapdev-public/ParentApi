using Parent.Application.ViewModels;
using Parent.Domain;

namespace Parent.Application;

public class AllChildren : IAllChildren
{
    private readonly IChildRepository _childRepository;

    public AllChildren(IChildRepository childRepository)
    {
        _childRepository = childRepository;
    }
    
    public async Task<IEnumerable<ChildViewModel>> All()
    {
        var children = await _childRepository.All();

        var childViewModels = new List<ChildViewModel>();

        foreach (var child in children)
        {
            var viewModel = new ChildViewModel
            {
                ChildId = child.Identifier.Id,
                GuardianId = child.GuardianIdentifier.Id,
                FirstName = child.Name.FirstName,
                LastName = child.Name.LastName,
                Toys = child.ToyBox.Select(x => new ToyViewModel {Name = x.Name, Cost = x.Cost, Upc = x.Upc})
            };
            
            childViewModels.Add(viewModel);
        }

        return childViewModels;
    }
}