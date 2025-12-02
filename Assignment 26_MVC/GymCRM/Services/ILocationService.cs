// Services/ILocationService.cs
using System.Collections.Generic;
using GymCRM.Model;

namespace GymCRM.Services
{
    // DTO خفيف للاستخدام في الـAPI
    public record NearBranchDto(int Id, int CityId, string NameAr, string NameEn, double DistanceKm);

    public interface ILocationService
    {
        IEnumerable<NearBranchDto> GetNearestBranches(
            double lat, double lng,
            Gender? gender = null,
            int? cityId = null,
            int take = 5);
    }
}
