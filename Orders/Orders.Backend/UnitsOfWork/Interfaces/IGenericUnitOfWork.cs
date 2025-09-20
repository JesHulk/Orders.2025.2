namespace Orders.Backend.UnitsOfWork.Interfaces;

/// <summary>
/// Unidad de trabajo genérica para manejar operaciones CRUD
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGenericUnitOfWork<T> where T : class
{

    /// <summary>
    /// Agrega una entidad
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>ActionResponse</returns>
    Task<ActionResponse<T>> AddAsync(T entity);

    /// <summary>
    /// Borra una entidad por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>ActionResponse</returns>
    Task<ActionResponse<T>> DeleteAsync(int id);

    /// <summary>
    /// Obtiene todas las entidades paginadas
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

    /// <summary>
    /// Obtiene una entidad por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>ActionResponse</returns>
    Task<ActionResponse<T>> GetAsync(int id);

    /// <summary>
    /// Obtiene todas las entidades
    /// </summary>
    /// <returns>ActionResponse</returns>
    Task<ActionResponse<IEnumerable<T>>> GetAsync();

    /// <summary>
    /// Asynchronously retrieves the total number of records available for the specified pagination parameters.
    /// </summary>
    /// <param name="pagination">An object containing pagination settings, such as page size and filters, that determine which records are
    /// counted. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ActionResponse with the total
    /// count of records matching the pagination criteria.</returns>
    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
    /// <summary>
    /// Actualiza una entidad
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>ActionResponse</returns>
    Task<ActionResponse<T>> UpdateAsync(T entity);
}
