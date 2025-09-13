
namespace Orders.Backend.Data;

public class SeedDb(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task SeedAsync()
    {
        // Create the database if it does not exist
        await _context.Database.EnsureCreatedAsync();

        await CheckCountriesAsync();

        await CheckCategoriesAsync();

    }

    private async Task CheckCategoriesAsync()
    {
        if (!await _context.Categories.AnyAsync())
        {
            _context.Categories.Add(new Category { Name = "Calzado" });
            _context.Categories.Add(new Category { Name = "Tecnología" });
            await _context.SaveChangesAsync();
        }

    }

    private async Task CheckCountriesAsync()
    {
        if (!await _context.Countries.AnyAsync())
        {
            _context.Countries.Add(new Country { Name = "Colombia" });
            _context.Countries.Add(new Country { Name = "Bolivia" });
            await _context.SaveChangesAsync();
        }
    }
}
