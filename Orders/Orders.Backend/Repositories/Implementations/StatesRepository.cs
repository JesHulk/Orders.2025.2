namespace Orders.Backend.Repositories.Implementations;

public class StatesRepository(DataContext context) : GenericRepository<State>(context), IStatesRepository
{
    private readonly DataContext _context = context;

    public override async Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.States
            .Include(s => s.Cities)
            .Where(x => x.Country!.Id == pagination.Id)
            .AsQueryable();

        if (!string.IsNullOrEmpty(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.Contains(pagination.Filter, StringComparison.CurrentCultureIgnoreCase));
        }

        return new ActionResponse<IEnumerable<State>>
        {
            WasSuccess = true,
            Result = await queryable
               .OrderBy(x => x.Name)
               .Paginate(pagination)
               .ToListAsync()
        };
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.States
            .Where(x => x.Country!.Id == pagination.Id)
            .AsQueryable();

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

    public override async Task<ActionResponse<IEnumerable<State>>> GetAsync()
    {
        var response = new ActionResponse<IEnumerable<State>>();
        try
        {
            var states = await _context.States
                .Include(s => s.Cities)
                .ToListAsync();

            response.Result = states;
            response.WasSuccess = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return response;
    }

    public override async Task<ActionResponse<State>> GetAsync(int id)
    {
        var response = new ActionResponse<State>();
        try
        {
            var state = await _context.States
                        .Include(s => s.Cities)
                        .FirstOrDefaultAsync(s => s.Id == id);

            if (state != null)
            {
                response.Result = state;
                response.WasSuccess = true;
            }
            else
            {
                response.Message = "Estado no encontrado.";
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return response;
    }
}
