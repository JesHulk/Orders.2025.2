namespace Orders.Backend.UnitsOfWork.Implementations;

public class CategoriesUnitOfWork(IGenericRepository<Category> repository, ICategoriesRepository categoriesRepository) 
    : GenericUnitOfWork<Category>(repository), ICategoriesUnitOfWork
{
    private readonly ICategoriesRepository _categoriesRepository = categoriesRepository;

    public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination) 
        => await _categoriesRepository.GetAsync(pagination);

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) 
        => await _categoriesRepository.GetTotalRecordsAsync(pagination);
}

