using Microsoft.EntityFrameworkCore;
using Shoghlana.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF.Repositories;
public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<T> Get(Func<T, bool> where)
    {
        return _context.Set<T>().Where(where).ToList();
    }

    public List<T> GetAll(string include = null)
    {
        if (include == null)
        {
            return _context.Set<T>().ToList();
        }
        return _context.Set<T>().Include(include).ToList();
    }

    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Insert(T entity)
    {
        _context.Add(entity);
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

}
