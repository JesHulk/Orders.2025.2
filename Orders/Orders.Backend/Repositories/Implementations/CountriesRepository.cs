namespace Orders.Backend.Repositories.Implementations;

public class CountriesRepository(DataContext context) : GenericRepository<Country>(context), ICountriesRepository
{
    private readonly DataContext _context = context;

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Countries  
            .Include(x => x.States)
            .AsQueryable();

        if (!string.IsNullOrEmpty(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<Country>> 
        {
            WasSuccess = true,
            Result     = await queryable.OrderBy    (x => x.Name)
                                        .Paginate   (pagination)
                                        .ToListAsync()
        };
    }

    public async override Task<ActionResponse<IEnumerable<Country>>> GetAsync()
    {
        var countries = await _context.Countries
            .Include(x => x.States)
            .ToListAsync();


        return new ActionResponse<IEnumerable<Country>>
        {
            WasSuccess = true,
            Result     = countries
        };
    }

    public async override Task<ActionResponse<Country>> GetAsync(int id)
    {
        var country = await _context.Countries
            .Include(x => x.States!)
                .ThenInclude(s => s.Cities)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (country == null)
        {
            return new ActionResponse<Country>
            {
                Message = "Registro no encontrado."
            };
        }

        return new ActionResponse<Country>
        {
            WasSuccess = true,
            Result = country
        };
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Countries.AsQueryable();

        if (!string.IsNullOrEmpty(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }
}
