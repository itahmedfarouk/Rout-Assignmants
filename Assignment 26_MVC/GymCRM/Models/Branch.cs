namespace GymCRM.Model
{
    public class Branch
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Gender AllowedGender { get; set; }

        public City City { get; set; } = null!;
    }
}
