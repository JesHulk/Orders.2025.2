namespace Orders.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController(IGenericUnitOfWork<City> unitOfWork, ICitiesUnitOfWork citiesUnitOfWork) : GenericController<City>(unitOfWork)
{
    private readonly ICitiesUnitOfWork _citiesUnitOfWork = citiesUnitOfWork;

    #region GET
    [HttpGet("totalRecords")]
    public override async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
    {
        var response = await _citiesUnitOfWork.GetTotalRecordsAsync(pagination);
        if (response.WasIsSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest(response.Message);
    }

    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
    {
        var response = await _citiesUnitOfWork.GetAsync(pagination);
        if (response.WasIsSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest(response.Message);
    }
    #endregion
}
