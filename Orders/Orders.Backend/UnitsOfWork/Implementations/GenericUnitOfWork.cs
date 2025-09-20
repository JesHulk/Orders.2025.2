namespace Orders.Backend.UnitsOfWork.Implementations;

public class GenericUnitOfWork<T>(IGenericRepository<T> genericRepository) : IGenericUnitOfWork<T> where T : class
{
    private readonly IGenericRepository<T> _genericRepository = genericRepository;

    public virtual async Task<ActionResponse<T>>              AddAsync(T entity)                             => await _genericRepository .AddAsync(entity);
                                                                                                             
    public virtual async Task<ActionResponse<T>>              DeleteAsync(int id)                            => await _genericRepository .DeleteAsync(id);
                                                                                                             
    public virtual async Task<ActionResponse<T>>              GetAsync(int id)                               => await _genericRepository .GetAsync(id);
                                                                                                             
    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()                                     => await _genericRepository .GetAsync();
                                                                                                             
    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination)             => await _genericRepository.GetAsync(pagination);

    public virtual async Task<ActionResponse<int>>            GetTotalRecordsAsync(PaginationDTO pagination) => await _genericRepository.GetTotalRecordsAsync(pagination);

    public virtual async Task<ActionResponse<T>>              UpdateAsync(T entity)                          => await _genericRepository .UpdateAsync(entity);
}
