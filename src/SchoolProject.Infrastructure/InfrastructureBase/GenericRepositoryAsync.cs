


namespace SchoolProject.Infrastructure.InfrastructureBase;

public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
{
    private readonly AppDbContext _context;
    public GenericRepositoryAsync()
    {

    }
    public GenericRepositoryAsync(AppDbContext context)
    {
        _context = context;
    }
    public virtual async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;

    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _context.Database.CommitTransaction();
    }

    public virtual async Task DeleteRangeAsync(ICollection<T> entity)
    {
        foreach (var item in entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public IQueryable<T> GetTableAsNoTracking()
    {
        return _context.Set<T>().AsQueryable();
    }

    public IQueryable<T> GetTableNoTracking()
    {
        return _context.Set<T>().AsNoTracking()
                 .AsQueryable();
    }

    public void RollBack()
    {
        _context.Database.RollbackTransactionAsync();
    }

    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
        await _context.SaveChangesAsync();

    }
}

