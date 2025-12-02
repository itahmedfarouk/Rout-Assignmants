using System.Collections.Generic;
using System.Linq;
using GymCRM.Model;

namespace GymCRM.Data
{
    public static class Seed
    {
        public static void Run(GymCRMContext db)
        {
            // 1) Countries + Cities + Branches
            if (!db.Countries.Any())
            {
                var sa = new Country { NameAr = "المملكة العربية السعودية", NameEn = "Saudi Arabia", Iso2 = "SA" };
                var ae = new Country { NameAr = "الإمارات العربية المتحدة", NameEn = "United Arab Emirates", Iso2 = "AE" };
                db.Countries.AddRange(sa, ae);
                db.SaveChanges();

                var cities = new Dictionary<string, City>
                {
                    // SA
                    ["SA-Riyadh"] = new City { NameAr = "الرياض", NameEn = "Riyadh", CountryId = sa.Id },
                    ["SA-Jeddah"] = new City { NameAr = "جدة", NameEn = "Jeddah", CountryId = sa.Id },
                    ["SA-Dammam"] = new City { NameAr = "الدمام", NameEn = "Dammam", CountryId = sa.Id },
                    ["SA-Khobar"] = new City { NameAr = "الخبر", NameEn = "Khobar", CountryId = sa.Id },
                    ["SA-Makkah"] = new City { NameAr = "مكة", NameEn = "Makkah", CountryId = sa.Id },
                    ["SA-Medina"] = new City { NameAr = "المدينة المنورة", NameEn = "Medina", CountryId = sa.Id },

                    // AE
                    ["AE-Dubai"] = new City { NameAr = "دبي", NameEn = "Dubai", CountryId = ae.Id },
                    ["AE-AbuDhabi"] = new City { NameAr = "أبوظبي", NameEn = "Abu Dhabi", CountryId = ae.Id },
                    ["AE-Sharjah"] = new City { NameAr = "الشارقة", NameEn = "Sharjah", CountryId = ae.Id },
                    ["AE-Ajman"] = new City { NameAr = "عجمان", NameEn = "Ajman", CountryId = ae.Id },
                    ["AE-AlAin"] = new City { NameAr = "العين", NameEn = "Al Ain", CountryId = ae.Id },
                };

                db.Cities.AddRange(cities.Values);
                db.SaveChanges();

                db.Branches.AddRange(
                    // KSA
                    new Branch { CityId = cities["SA-Riyadh"].Id, NameAr = "فرع النخيل", NameEn = "Al-Nakheel", Latitude = 24.774265, Longitude = 46.738586, AllowedGender = Gender.Male },
                    new Branch { CityId = cities["SA-Riyadh"].Id, NameAr = "فرع الندى", NameEn = "Al-Nada", Latitude = 24.834000, Longitude = 46.700000, AllowedGender = Gender.Female },
                    new Branch { CityId = cities["SA-Jeddah"].Id, NameAr = "فرع الروضة", NameEn = "Al-Rawdah", Latitude = 21.543333, Longitude = 39.172779, AllowedGender = Gender.Male },
                    new Branch { CityId = cities["SA-Dammam"].Id, NameAr = "فرع الدمام 1", NameEn = "Dammam 1", Latitude = 26.420700, Longitude = 50.088800, AllowedGender = Gender.Male },
                    new Branch { CityId = cities["SA-Khobar"].Id, NameAr = "فرع الخبر 1", NameEn = "Khobar 1", Latitude = 26.279440, Longitude = 50.208330, AllowedGender = Gender.Female },
                    new Branch { CityId = cities["SA-Makkah"].Id, NameAr = "فرع العزيزية", NameEn = "Aziziyah", Latitude = 21.389082, Longitude = 39.857910, AllowedGender = Gender.Male },
                    new Branch { CityId = cities["SA-Medina"].Id, NameAr = "فرع قباء", NameEn = "Quba", Latitude = 24.470901, Longitude = 39.612236, AllowedGender = Gender.Male },

                    // UAE
                    new Branch { CityId = cities["AE-Dubai"].Id, NameAr = "فرع دبي مارينا", NameEn = "Dubai Marina", Latitude = 25.0800, Longitude = 55.1400, AllowedGender = Gender.Male },
                    new Branch { CityId = cities["AE-Dubai"].Id, NameAr = "فرع جميرا", NameEn = "Jumeirah", Latitude = 25.1890, Longitude = 55.2430, AllowedGender = Gender.Female },
                    new Branch { CityId = cities["AE-AbuDhabi"].Id, NameAr = "فرع الكورنيش", NameEn = "Corniche", Latitude = 24.4667, Longitude = 54.3667, AllowedGender = Gender.Male },
                    new Branch { CityId = cities["AE-Sharjah"].Id, NameAr = "فرع المجاز", NameEn = "Al Majaz", Latitude = 25.3463, Longitude = 55.4209, AllowedGender = Gender.Male },
                    new Branch { CityId = cities["AE-Ajman"].Id, NameAr = "فرع عجمان سيتي", NameEn = "Ajman City", Latitude = 25.4052, Longitude = 55.5136, AllowedGender = Gender.Male },
                    new Branch { CityId = cities["AE-AlAin"].Id, NameAr = "فرع العين مول", NameEn = "Al Ain Mall", Latitude = 24.2075, Longitude = 55.7447, AllowedGender = Gender.Female }
                );
                db.SaveChanges();
            }

            // 2) Membership Plans
            if (!db.MembershipPlans.Any())
            {
                db.MembershipPlans.AddRange(
                    new MembershipPlan { TitleAr = "شهرية عادية", TitleEn = "Standard Monthly", PriceMonthly = 249, IsInstallmentSupported = false, DurationMonths = 1 },
                    new MembershipPlan { TitleAr = "بريميوم شهرية", TitleEn = "Premium Monthly", PriceMonthly = 349, IsInstallmentSupported = true, DurationMonths = 1 },
                    new MembershipPlan { TitleAr = "اشتراك سنوي", TitleEn = "Annual Upfront", PriceMonthly = 199, PriceUpfront = 1999, IsInstallmentSupported = false, DurationMonths = 12 }
                );
                db.SaveChanges();
            }

            // 3) Coupon
            if (!db.Coupons.Any(c => c.Code == "GYM10"))
            {
                db.Coupons.Add(new Coupon { Code = "GYM10", PercentOff = 10, IsActive = true });
                db.SaveChanges();
            }

            // 4) Session Types
            if (!db.SessionTypes.Any())
            {
                db.SessionTypes.AddRange(
                    new SessionType { TitleAr = "يوجا", TitleEn = "Yoga", DefaultDurationMin = 60 },
                    new SessionType { TitleAr = "ساونا", TitleEn = "Sauna", DefaultDurationMin = 30 }
                );
                db.SaveChanges();
            }
        }
    }
}
