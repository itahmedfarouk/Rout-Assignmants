// Services/LocationService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using GymCRM.Data;
using GymCRM.Model;

namespace GymCRM.Services
{
    public class LocationService : ILocationService
    {
        private readonly GymCRMContext _db;
        public LocationService(GymCRMContext db) { _db = db; }

        public IEnumerable<NearBranchDto> GetNearestBranches(
            double lat, double lng,
            Gender? gender = null,
            int? cityId = null,
            int take = 5)
        {
            var q = _db.Branches.AsQueryable();
            if (cityId.HasValue) q = q.Where(b => b.CityId == cityId.Value);
            if (gender.HasValue) q = q.Where(b => b.AllowedGender == gender.Value);

            // نحسب المسافة في الذاكرة باستخدام Haversine
            var list = q.AsEnumerable()
                .Select(b => new NearBranchDto(
                    b.Id,
                    b.CityId,
                    b.NameAr,
                    b.NameEn,
                    HaversineKm(lat, lng, b.Latitude, b.Longitude)
                ))
                .OrderBy(x => x.DistanceKm)
                .Take(Math.Max(1, take))
                .ToList();

            return list;
        }

        private static double HaversineKm(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371.0; // كم
            double dLat = ToRad(lat2 - lat1);
            double dLon = ToRad(lon2 - lon1);
            lat1 = ToRad(lat1);
            lat2 = ToRad(lat2);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }
        private static double ToRad(double deg) => deg * Math.PI / 180.0;
    }
}
