namespace Orders.Backend.UnitsOfWork.Interfaces;

public interface IStatesUnitOfWork
{

    /// <summary>
    /// Total de registros
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    /// <summary>
    /// Asynchronously retrieves a paginated collection of states.
    /// </summary>
    /// <param name="pagination">An object specifying pagination parameters, such as page number and page size, to control which subset of states
    /// is returned. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ActionResponse with an enumerable
    /// collection of State objects corresponding to the requested page.</returns>
    Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination);

    /// <summary>
    /// Obtiene un estado por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Un estado</returns>
    Task<ActionResponse<State>> GetAsync(int id);

    /// <summary>
    /// Obtiene todos los estados
    /// </summary>
    /// <returns>Un enumerable de estados</returns>
    Task<ActionResponse<IEnumerable<State>>> GetAsync();
}
