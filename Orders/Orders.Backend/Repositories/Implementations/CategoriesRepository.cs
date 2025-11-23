namespace Orders.Backend.Repositories.Implementations;

public class CategoriesRepository(DataContext context) : GenericRepository<Category>(context), ICategoriesRepository
{
    private readonly DataContext _context = context;

    public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<Category>>
        {
            WasSuccess = true,
            Result       = await queryable.OrderBy    (x => x.Name)
                                          .Paginate   (pagination)
                                          .ToListAsync()
        };
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result       = (int)count
        };
    }
}
