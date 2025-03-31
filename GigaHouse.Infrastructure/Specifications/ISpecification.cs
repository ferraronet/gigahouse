namespace GigaHouse.Infrastructure.Specifications;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
}
