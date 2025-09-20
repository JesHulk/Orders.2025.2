namespace Orders.Backend.UnitsOfWork.Implementations;

public class CitiesUnitOfWork(IGenericRepository<City> repository, 
                              ICitiesRepository citiesRepository) 
    : GenericUnitOfWork<City>(repository), ICitiesUnitOfWork
{
    private readonly ICitiesRepository _citiesRepository = citiesRepository;

    public async override Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination)
        => await _citiesRepository.GetAsync(pagination);

    public async override Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) 
        => await _citiesRepository.GetTotalRecordsAsync(pagination);    
}