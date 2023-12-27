using RP.Business.API.Extension;
using RP.Business.Config;
using System.Net.Http.Headers;
using System.Text;

namespace RP.Tests.Services.Jira
{
    public class JiraService
    {
        private HttpClient _httpClient;
        private const string IssueUrlTemplate = "{0}rest/api/latest/issue/{1}";
        private const string IssueFieldsUpdateTemplate = IssueUrlTemplate + "/transitions?expand=transitions.fields";
        private const string IssueCommentsUpdateTemplate = IssueUrlTemplate + "/comment";

        public JiraService(HttpClient httpClient, JiraConfig config)
        {
            httpClient.BaseAddress = new Uri(config.URL);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(config.User + ":" + config.Token)));
            _httpClient = httpClient;
        }

        public async void UpdateTestCaseStatus(string caseId, JiraStatus status)
        {
            await _httpClient.PostJson(string.Format(IssueFieldsUpdateTemplate, _httpClient.BaseAddress, caseId), new
            {
                transition = new
                {
                    id = GetTestCaseStatusIdAsync(caseId, status)
                }
            });
        }

        public async void AddCommentToTestCase(string caseId, string comment)
        {
            await _httpClient.PostJson(string.Format(IssueCommentsUpdateTemplate, _httpClient.BaseAddress, caseId), new { body = comment });
        }

        private int? GetTestCaseStatusIdAsync(string caseId, JiraStatus status)
        {
            return _httpClient.Get<TestCaseDto>(string.Format(IssueFieldsUpdateTemplate, _httpClient.BaseAddress, caseId))
                .Transitions.FirstOrDefault(t => t.Name.Replace(" ", "").Equals(status.ToString()))?.Id;
        }
    }
}
