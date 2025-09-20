namespace Orders.Backend.Repositories.Implementations;

public class CountriesRepository(DataContext context) : GenericRepository<Country>(context), ICountriesRepository
{
    private readonly DataContext _context = context;

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Countries
            .Include(x => x.States)
            .AsQueryable();

        return new ActionResponse<IEnumerable<Country>>
        {
            WasIsSuccess    = true,
            Result          = await queryable.OrderBy    (x => x.Name)
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
            WasIsSuccess = true,
            Result = countries
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
            WasIsSuccess = true,
            Result = country
        };
    }
}
