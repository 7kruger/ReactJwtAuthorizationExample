namespace WebApi.DAL.Interfaces;

public interface IRepository<T> where T : class
{
	IQueryable<T> GetAll();
	Task Create(T entity);
	Task Update(T entity);
	Task Delete(T entity);
}
