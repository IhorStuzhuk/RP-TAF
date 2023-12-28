namespace RP.Business.API.Models
{
    public class ResponseException
    {
        public int ErrorCode { get; set; }

        public string Message { get; set; }
    }

    public class Messages
    {
        public static string DashboardWithIdNotFoundOnProject(int id, string projectName) 
            => $"Dashboard with ID '{id}' not found on project '{projectName}'. Did you use correct Dashboard ID?";

        public static string ResourceAlreadyExists(string resourceName)
            => $"Resource '{resourceName}' already exists. You couldn't create the duplicate.";

        public static string IncorrectRequestFieldNull(string fieldName)
            => $"Incorrect Request. [Field '{fieldName}' should not be null.] ";

        public static string IncorrectRequestFieldSize(string fieldName)
            => $"Incorrect Request. [Field '{fieldName}' should have size from '3' to '128'.] ";
    }
}