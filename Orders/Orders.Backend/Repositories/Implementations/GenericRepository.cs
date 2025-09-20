namespace Orders.Backend.Repositories.Implementations;

public class GenericRepository<T>(DataContext context) : IGenericRepository<T> where T : class
{
    private readonly DataContext    _context    = context;
    private readonly DbSet<T>       _entity     = context.Set<T>();

    public virtual async Task<ActionResponse<T>> AddAsync(T entity)
    {
        _context.Add(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasIsSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return GenericRepository<T>.DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return GenericRepository<T>.ExceptionActionResponse(exception);
        }
    }

    public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponse<T>
            {
                Message = "Registro no encontrado."
            };
        }
        _entity.Remove(row);

        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasIsSuccess = true
            };
        }
        catch
        {
            return new ActionResponse<T>
            {
                Message = "Registro no se ha podido eliminar porque tiene registros relacionados."
            };
        }

    }

    public virtual async Task<ActionResponse<T>> GetAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponse<T>
            {
                Message = "Registro no encontrado."
            };
        }
        return new ActionResponse<T>
        {
            WasIsSuccess = true,
            Result = row
        };
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()   
        => new ActionResponse<IEnumerable<T>>
        {
            WasIsSuccess = true,
            Result = await _entity.ToListAsync()
        };

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _entity.AsQueryable();

        return new ActionResponse<IEnumerable<T>>
        {
            WasIsSuccess = true,
            Result = await queryable
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _entity.AsQueryable();
        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasIsSuccess = true,
            Result = (int)count
        };
    }

    public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
    {
        _context.Update(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasIsSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return GenericRepository<T>.DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return GenericRepository<T>.ExceptionActionResponse(exception);
        }
    }

    private static ActionResponse<T> DbUpdateExceptionActionResponse()
        => new() { Message = "Ya existe el registro." };      

    private static ActionResponse<T> ExceptionActionResponse(Exception exception)
        => new() { Message = exception.Message };
}
