using FluentAssertions;
using RP.Business.Models;
using RP.Core.API;
using RP.Core.API.Helpers;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RP.Tests.BDD.Steps
{
    [Binding]
    public class DashboardsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public DashboardsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext.Set(Configuration.DashboardApiClient, "DashboardApiClient");
        }

        [Given(@"I have created dashboard")]
        public async Task GivenIHaveCreatedDashboard(Table table)
        {
            var dashboard = table.CreateInstance<DashboardDto>();
            var response = await _scenarioContext.Get<DashboardApiClient>("DashboardApiClient").CreateDashboard(dashboard);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            _scenarioContext.Set(response.GetContentAs<DashboardDto>().Id, "CreatedDBId");
        }

        [Given(@"I have created dashboard with (.*) and (.*)")]
        public async Task GivenIHaveCreatedDashboard(string name, string description)
        {
            var dashboard = new DashboardDto { Name = name, Description = description };
            var response = await _scenarioContext.Get<DashboardApiClient>("DashboardApiClient").CreateDashboard(dashboard);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            _scenarioContext.Set(response.GetContentAs<DashboardDto>().Id, "CreatedDBId");
        }

        [Given(@"I have created dashboards")]
        public async Task GivenIHaveCreatedDashboards(Table table)
        {
            var dashboards = table.CreateSet<DashboardDto>();
            foreach(var db in dashboards)
            {
                var response = await _scenarioContext.Get<DashboardApiClient>("DashboardApiClient").CreateDashboard(db);
                response.StatusCode.Should().Be(HttpStatusCode.Created);
            }
        }

        [Given(@"I do not have any created dashboards")]
        public async Task GivenIDoNotHaveAnyCreatedDashboards()
        {
            await _scenarioContext.Get<DashboardApiClient>("DashboardApiClient").DeleteAllCreatedDashboards();
        }

        [When(@"I get created dashboard")]
        public async Task WhenIGetCreatedDashboard()
        {
            var response = await _scenarioContext.Get<DashboardApiClient>("DashboardApiClient").GetDashboardById(_scenarioContext.Get<int>("CreatedDBId"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            _scenarioContext.Set(response.GetContentAs<DashboardDto>(), "CreatedDB");
        }

        [When(@"I get created dashboards")]
        public async Task WhenIGetCreatedDashboards()
        {
            var response = await _scenarioContext.Get<DashboardApiClient>("DashboardApiClient").GetAllDashboards();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            _scenarioContext.Set(response.GetContentAs<DashboardResponceDto>().Dashboards, "CreatedDBs");
        }

        [When(@"I edit last created dashboard with new fields")]
        public async Task WhenIEditLastCreatedDashboard(Table table)
        {
            var dashboardToUpdate = table.CreateInstance<DashboardDto>();
            var response = await _scenarioContext.Get<DashboardApiClient>("DashboardApiClient").UpdateDashboardById(_scenarioContext.Get<int>("CreatedDBId"), dashboardToUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"I make sure that dashboard is edited and has the following fields:")]
        public async Task ThenIMakeSureThatDashboardIsEdited(Table table)
        {
            var expectedDashboard = table.CreateInstance<DashboardDto>();

            var response = await _scenarioContext.Get<DashboardApiClient>("DashboardApiClient").GetDashboardById(_scenarioContext.Get<int>("CreatedDBId"));
            var dashboard = response.GetContentAs<DashboardDto>();

            dashboard.Name.Should().Be(expectedDashboard.Name);
            dashboard.Description.Should().Be(expectedDashboard.Description);
        }

        [Then(@"I make sure that dashboard has (.*) and (.*)")]
        public void ThenIMakeSureThatDashboardHasNameAndDescription(string name, string description)
        {
            _scenarioContext.Get<DashboardDto>("CreatedDB").Name.Should().Be(name);
            _scenarioContext.Get<DashboardDto>("CreatedDB").Description.Should().Be(description);
        }

        [Then(@"I make sure that dashboards amount is (.*)")]
        public void ThenIMakeSureThatDashboardAmountIs(int expAmount)
        {
            _scenarioContext.Get<List<DashboardDto>>("CreatedDBs").Count.Should().Be(expAmount);
        }
    }
}
