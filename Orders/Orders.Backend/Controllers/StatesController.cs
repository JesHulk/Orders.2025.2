namespace Orders.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatesController(IGenericUnitOfWork<State> unitOfWork,
                              IStatesUnitOfWork         statesUnitOfWork) : GenericController<State>(unitOfWork)
{
    private readonly IStatesUnitOfWork _statesUnitOfWork = statesUnitOfWork;

    #region GET

    [HttpGet("totalRecords")]
    public override async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
    {
        var response = await _statesUnitOfWork.GetTotalRecordsAsync(pagination);
        if (response.WasIsSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest(response.Message);
    }

    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
    {
        var response = await _statesUnitOfWork.GetAsync(pagination);
        if (response.WasIsSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest(response.Message);
    }

    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var repsonse = await _statesUnitOfWork.GetAsync();
        if (repsonse.WasIsSuccess)
        {
            return Ok(repsonse.Result);
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _statesUnitOfWork.GetAsync(id);
        if (response.WasIsSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    #endregion

}
