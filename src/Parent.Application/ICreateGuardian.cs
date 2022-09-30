namespace Parent.Application;

public interface ICreateGuardian
{
    Task HandleAsync(NewParentViewModel newParentViewModel);
}