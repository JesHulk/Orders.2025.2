namespace Orders.Backend.Repositories.Implementations;

public class StatesRepository(DataContext context) : GenericRepository<State>(context), IStatesRepository
{
    private readonly DataContext _context = context;

    public override async Task<ActionResponse<IEnumerable<State>>> GetAsync()
    {
        var response = new ActionResponse<IEnumerable<State>>();
        try
        {
            var states = await _context.States
                .Include(s => s.Cities)
                .ToListAsync();

            response.Result = states;
            response.WasIsSuccess = true;
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
                response.WasIsSuccess = true;
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
