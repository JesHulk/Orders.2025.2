namespace Orders.Backend.UnitsOfWork.Interfaces;

public interface IStatesUnitOfWork
{
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
