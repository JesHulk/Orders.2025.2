namespace Orders.Backend.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    #region DbSet

    public DbSet<Country> Countries { get; set; } = null!;
    
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
    }

}
