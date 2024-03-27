using BlazorAPICountries.Models;

namespace BlazorAPICountries.Interfaces
{
	public interface ICountryService
	{
		Task<List<Country>> GetAllCountriesAsync();

	    Task<List<Country>> GetCountryByPopulationAsync(int minPopulation, int maxpopulation);

		Task<List<Country>> SearchCountryByNameAsync(string searchQuery);

	}
}
