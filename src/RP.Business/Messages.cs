namespace RP.Business
{
    public class Messages
    {
        #region API
        public static string DashboardWithIdNotFoundOnProject(int id, string projectName)
            => $"Dashboard with ID '{id}' not found on project '{projectName}'. Did you use correct Dashboard ID?";

        public static string ResourceAlreadyExists(string resourceName)
            => $"Resource '{resourceName}' already exists. You couldn't create the duplicate.";

        public static string IncorrectRequestFieldNull(string fieldName)
            => $"Incorrect Request. [Field '{fieldName}' should not be null.] ";

        public static string IncorrectRequestFieldSize(string fieldName)
            => $"Incorrect Request. [Field '{fieldName}' should have size from '3' to '128'.] ";
        #endregion

        #region UI
        public static string YouHaveNoDashboards => "You have no dashboards";

        public static string SignedInSuccessfully => "Signed in successfully";

        public static string DashboardHasBeenAdded => "Dashboard has been added";

        public static string DashboardHasBeenDeleted => "Dashboard has been deleted";

        public static string WidgetHasBeenAdded => "Widget has been added";

        public static string WidgetHasBeenDeleted => "Widget has been deleted";

        public static string ThereAreNoWidgetsOnDashboard => "There are no widgets on this dashboard";

        #endregion
    }
}
