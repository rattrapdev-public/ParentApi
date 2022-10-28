using Parent.Application.MessageModels;

namespace Parent.Application;

public interface IToyPurchased
{
    Task HandleAsync(PurchasedToyViewModel viewModel);
}