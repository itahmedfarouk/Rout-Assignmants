namespace GymCRM.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public string Iso2 { get; set; } = null!; // SA, AE
        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
