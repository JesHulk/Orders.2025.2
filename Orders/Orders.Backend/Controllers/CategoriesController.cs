namespace Orders.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IGenericUnitOfWork<Category> unitOfWork) : GenericController<Category>(unitOfWork) {}
