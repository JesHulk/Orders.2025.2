namespace Orders.Backend.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    #region DbSet

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;

    public DbSet<Country> Countries { get; set; } = null!;

    public DbSet<State> States { get; set; } = null!;

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Índices

        modelBuilder.Entity<Category>   ()  .HasIndex(x => x.Name)                      .IsUnique();

        modelBuilder.Entity<City>       ()  .HasIndex(x => new {x.StateId   , x.Name }) .IsUnique();

        modelBuilder.Entity<Country>    ()  .HasIndex(x => x.Name)                      .IsUnique();

        modelBuilder.Entity<State>      ()  .HasIndex(x => new {x.CountryId , x.Name }) .IsUnique();

        #endregion

        // Desactivar el borrado en cascada
        DisableCascadeDelete(modelBuilder);

    }

    private static void DisableCascadeDelete(ModelBuilder modelBuilder)
    {
        var relationShips = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());

        foreach (var relationship in relationShips)
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
