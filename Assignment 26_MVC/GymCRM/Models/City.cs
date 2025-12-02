namespace GymCRM.Model
{
    public class City
    {
        public int Id { get; set; }
        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;

        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;
    }
}
