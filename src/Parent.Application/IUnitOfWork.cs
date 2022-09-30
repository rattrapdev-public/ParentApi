using Parent.Domain;

namespace Parent.Application;

public interface IUnitOfWork
{
    Task Commit();
}