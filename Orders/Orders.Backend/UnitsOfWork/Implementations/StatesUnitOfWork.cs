namespace Orders.Backend.UnitsOfWork.Implementations;

public class StatesUnitOfWork(IGenericRepository<State> repository, 
                              IStatesRepository         statesRepository) 
    : GenericUnitOfWork<State>(repository), IStatesUnitOfWork
{

    private readonly IStatesRepository _statesRepository = statesRepository;

    public override async Task<ActionResponse<IEnumerable<State>>> GetAsync() => await _statesRepository.GetAsync();

    public override async Task<ActionResponse<State>> GetAsync(int id) => await _statesRepository.GetAsync(id);
}
