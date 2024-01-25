namespace RP.Tests.Services.Jira
{
    public class TestCaseIdAttribute : Attribute
    {
        public string Id { get; set; }

        public TestCaseIdAttribute(string id)
        {
            Id = id;
        }
    }
}