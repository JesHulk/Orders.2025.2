namespace Orders.Backend.Repositories.Implementations;

public class CitiesRepository(DataContext context) : GenericRepository<City>(context), ICitiesRepository
{
    private readonly DataContext _context = context;

    public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Cities
            .Where(x => x.State!.Id == pagination.Id)
            .AsQueryable();

        return new ActionResponse<IEnumerable<City>>
        {
            WasIsSuccess = true,
            Result = await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Cities
            .Where(x => x.State!.Id == pagination.Id)
            .AsQueryable();

        double count = await queryable.CountAsync();

        return new ActionResponse<int>
        {
            WasIsSuccess = true,
            Result = (int)count
        };
    }

}
