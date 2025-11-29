namespace Orders.Backend.Data;

public class SeedDb(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task SeedAsync()
    {
        // Create the database if it does not exist
        await _context.Database.EnsureCreatedAsync();

        await CheckCountriesFullAsync();
        
        await CheckCountriesAsync();

        await CheckCategoriesAsync();

    }

    private async Task CheckCountriesFullAsync()
    {
        if (!await _context.Countries.AnyAsync())
        {
            var countriesSQLScript = await File.ReadAllTextAsync("Data\\CountriesStatesCities.sql");
            await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
        }
    }

    private async Task CheckCategoriesAsync()
    {
        if (!await _context.Categories.AnyAsync())
        {
            _context.Categories.Add(new Category { Name = "Apple" });
            _context.Categories.Add(new Category { Name = "Autos" });
            _context.Categories.Add(new Category { Name = "Belleza" });
            _context.Categories.Add(new Category { Name = "Calzado" });
            _context.Categories.Add(new Category { Name = "Comida" });
            _context.Categories.Add(new Category { Name = "Cosmeticos" });
            _context.Categories.Add(new Category { Name = "Deportes" });
            _context.Categories.Add(new Category { Name = "Erótica" });
            _context.Categories.Add(new Category { Name = "Ferreteria" });
            _context.Categories.Add(new Category { Name = "Gamer" });
            _context.Categories.Add(new Category { Name = "Hogar" });
            _context.Categories.Add(new Category { Name = "Jardín" });
            _context.Categories.Add(new Category { Name = "Jugetes" });
            _context.Categories.Add(new Category { Name = "Lenceria" });
            _context.Categories.Add(new Category { Name = "Mascotas" });
            _context.Categories.Add(new Category { Name = "Nutrición" });
            _context.Categories.Add(new Category { Name = "Ropa" });
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
