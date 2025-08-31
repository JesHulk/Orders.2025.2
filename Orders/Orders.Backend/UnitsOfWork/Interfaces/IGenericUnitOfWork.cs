using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Interfaces;

/// <summary>
/// Unidad de trabajo genérica para manejar operaciones CRUD
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGenericUnitOfWork<T> where T : class
{
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
    /// Actualiza una entidad
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>ActionResponse</returns>
    Task<ActionResponse<T>> UpdateAsync(T entity);
}
