namespace Orders.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController(IGenericUnitOfWork<Country> unitOfWork,
                                 ICountriesUnitOfWork countriesUnitOfWork) : GenericController<Country>(unitOfWork)
{
    private readonly ICountriesUnitOfWork _countriesUnitOfWork = countriesUnitOfWork;

    #region GET

    [HttpGet("totalRecords")]
    public override async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _countriesUnitOfWork.GetTotalRecordsAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }


    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
    {
        var response = await _countriesUnitOfWork.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest(response.Message);
    }

    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var action = await _countriesUnitOfWork.GetAsync();
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var action = await _countriesUnitOfWork.GetAsync(id);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return NotFound();
    }

    #endregion

}
