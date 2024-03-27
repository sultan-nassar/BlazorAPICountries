namespace BlazorAPICountries.Models
{
    public  class Country
    {
        public Name Name {  get; set; }
        public Dictionary<string, Currency> Currencies { get; set; }

        public string[] Capital { get; set; }

        public Maps Maps { get; set; }

        public int Population { get; set; }

        public Flags Flags { get; set; }

    }
}
