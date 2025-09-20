namespace Orders.Backend.UnitsOfWork.Implementations;

public class CountriesUnitOfWork(IGenericRepository<Country> repository, 
                                 ICountriesRepository countriesRepository) 
    : GenericUnitOfWork<Country>(repository), ICountriesUnitOfWork
{    

    private readonly ICountriesRepository _countriesRepository = countriesRepository;

    public async override Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination) 
        => await _countriesRepository.GetAsync(pagination);

    public async override Task<ActionResponse<Country>> GetAsync(int id) => await _countriesRepository.GetAsync(id);

    public async override Task<ActionResponse<IEnumerable<Country>>> GetAsync() => await _countriesRepository.GetAsync();
}
