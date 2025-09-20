using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    // Evitar ciclos en las referencias circulares. Entities relacionadas o dtos con referencias circulares
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddOpenApi();

// Agregar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyecci�n de la conexi�n a la base de datos
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));

// Inyecci�n del servicio para poblar la base de datos
builder.Services.AddTransient<SeedDb>();

// Inyecci�n de los repositorios
builder.Services.AddScoped(typeof(IGenericRepository<>) , typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericUnitOfWork<>) , typeof(GenericUnitOfWork<>));

builder.Services.AddScoped<ICountriesRepository         , CountriesRepository>  ();
builder.Services.AddScoped<IStatesRepository            , StatesRepository>     ();

builder.Services.AddScoped<ICountriesUnitOfWork         , CountriesUnitOfWork>  ();
builder.Services.AddScoped<IStatesUnitOfWork            , StatesUnitOfWork>     ();

var app = builder.Build();

// Poblar la base de datos
SeedData(app);

void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using var scope = scopedFactory!.CreateScope();

    var service = scope.ServiceProvider.GetService<SeedDb>();

    service!.SeedAsync().Wait();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    // Habilitar Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders API V1");
        c.RoutePrefix = string.Empty; // Esto hace que Swagger se muestre en la ra�z (/)
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
