using RP.Business.API.Models;
using RP.Core.Helpers;

namespace RP.Tests.TestDataProviders
{
    public class DashboardProvider
    {
        public static IEnumerable<object[]> GetDashboardSource()
        {
            yield return new object[]{ new DashboardDto
            {
                Name = StringHelper.RandomString(5),
                Description = StringHelper.RandomString(5),
            }};
            yield return new object[]{ new DashboardDto
            {
                Name = $"{StringHelper.RandomString(5)} Name",
                Description = $"{StringHelper.RandomString(5)} Description",
            }};
            yield return new object[]{ new DashboardDto
            {
                Name = $"{StringHelper.RandomString(5)} Name 1",
                Description = $"{StringHelper.RandomString(5)} Description 1",
            }};
        }

        public static DashboardDto GetDashboard() =>
           new DashboardDto
           {
               Name = StringHelper.RandomString(5),
               Description = StringHelper.RandomString(5)
           };
    }
}
