namespace Orders.Backend.UnitsOfWork.Implementations;

public class StatesUnitOfWork(IGenericRepository<State> repository, 
                              IStatesRepository         statesRepository) 
    : GenericUnitOfWork<State>(repository), IStatesUnitOfWork
{

    private readonly IStatesRepository _statesRepository = statesRepository;

    public async override Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) 
        => await _statesRepository.GetTotalRecordsAsync(pagination);

    public async override Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination)
        => await _statesRepository.GetAsync(pagination);

    public override async Task<ActionResponse<IEnumerable<State>>> GetAsync() => await _statesRepository.GetAsync();

    public override async Task<ActionResponse<State>> GetAsync(int id) => await _statesRepository.GetAsync(id);
}
