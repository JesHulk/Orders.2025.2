namespace Orders.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController(IGenericUnitOfWork<Country> unitOfWork) : GenericController<Country>(unitOfWork) {}
