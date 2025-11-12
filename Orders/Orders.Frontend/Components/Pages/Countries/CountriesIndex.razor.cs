
using System.Runtime.CompilerServices;

namespace Orders.Frontend.Components.Pages.Countries;

public partial class CountriesIndex
{
    [Inject] private IRepository Repository { get; set; } = null!;
    private List<Country>? Countries { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {        
        var httpResult = await Repository.GetAsync<List<Country>>("api/countries");
        Thread.Sleep(3000);
        Countries = httpResult.Response;        
    }
}
