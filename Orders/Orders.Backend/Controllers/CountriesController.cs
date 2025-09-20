namespace Orders.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController(IGenericUnitOfWork<Country> unitOfWork,
                                 ICountriesUnitOfWork countriesUnitOfWork) : GenericController<Country>(unitOfWork)
{
    private readonly ICountriesUnitOfWork _countriesUnitOfWork = countriesUnitOfWork;

    #region GET

    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
    {
        var response = await _countriesUnitOfWork.GetAsync(pagination);
        if (response.WasIsSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest(response.Message);
    }

    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var action = await _countriesUnitOfWork.GetAsync();
        if (action.WasIsSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var action = await _countriesUnitOfWork.GetAsync(id);
        if (action.WasIsSuccess)
        {
            return Ok(action.Result);
        }
        return NotFound();
    }

    #endregion

}
