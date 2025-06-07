using Shoghlana.API.Services.Interfaces;

namespace Shoghlana.API.Services.Implementaions;

public class GenaricService<T> : IGenaricService<T> where T : class
{
    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public T GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}
