using BlazorAPICountries.Interfaces;
using BlazorAPICountries.Models;
using BlazorAPICountries.Pages;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Text.Json;

namespace BlazorAPICountries.Services
{
    public class CountryService: ICountryService
	{
        private List<Country> _AllCountriesCach;
        //this is httpClient using it as a dependency Injection cuze we write it as a server in the program
        //we dont need to initialize the instant of HttpClient cuze 
        //we use it as DI and add it into the service of the program
        //so we initialize it just when we need it.
        private readonly HttpClient _httpClient;
        public CountryService(HttpClient httpClient)//DI
        {
            _httpClient = httpClient;
        }


        //GetAllCountryAsync()
        public async Task<List<Country>> GetAllCountriesAsync()
        {
            if (_AllCountriesCach != null)
            {
                return _AllCountriesCach;
            }

            var response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                _AllCountriesCach = JsonSerializer.Deserialize<List<Country>>(content, option);
            }
            else
            {
                Console.WriteLine("There is an error with the API");
            }

            return _AllCountriesCach ?? new List<Country>();
        }


        public async Task<List<Country>> GetCountryByPopulationAsync(int minPopulation, int maxpopulation)
        {
            _AllCountriesCach = await GetAllCountriesAsync();
            return _AllCountriesCach.Where(c => c.Population >= minPopulation && c.Population <= maxpopulation).ToList();

        }




        public async Task<List<Country>> SearchCountryByNameAsync(string searchQuery)
        {
            _AllCountriesCach = await GetAllCountriesAsync();
             return _AllCountriesCach.Where(c => c.Name.Common.ToLower().Contains(searchQuery.ToLower())).ToList();

        }
    }
}
