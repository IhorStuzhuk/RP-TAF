using FluentAssertions;
using RP.Business.API.Models;
using RP.Core.Helpers;
using System.Net;

namespace RP.Tests.xUnit
{
    public class BaseXUnitTest : IDisposable
    {
        protected const string PROJECT_NAME = "default_personal";

        public BaseXUnitTest() { }

        public void Dispose()
        {
            DeleteAllCreatedDashboards();
        }

        private async void DeleteAllCreatedDashboards() => await Configuration.DashboardApiClient.DeleteAllCreatedDashboards();
    }
}

