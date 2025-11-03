using Microsoft.AspNetCore.Http;

namespace FactorySystemWebpage.Helpers
{
    public static class SessionHelper
    {
        private const string UserIdKey = "UserId";
        private const string UsernameKey = "Username";
        private const string UserRoleKey = "UserRole";
        private const string EmployeeIdKey = "EmployeeId";
        private const string FullNameKey = "FullName";

        public static void SetUserSession(this ISession session, int userId, string username, string role, int? employeeId, string fullName)
        {
            session.SetInt32(UserIdKey, userId);
            session.SetString(UsernameKey, username);
            session.SetString(UserRoleKey, role);
            if (employeeId.HasValue)
                session.SetInt32(EmployeeIdKey, employeeId.Value);
            session.SetString(FullNameKey, fullName);
        }

        public static int? GetUserId(this ISession session)
        {
            return session.GetInt32(UserIdKey);
        }

        public static string? GetUsername(this ISession session)
        {
            return session.GetString(UsernameKey);
        }

        public static string? GetUserRole(this ISession session)
        {
            return session.GetString(UserRoleKey);
        }

        public static int? GetEmployeeId(this ISession session)
        {
            return session.GetInt32(EmployeeIdKey);
        }

        public static string? GetFullName(this ISession session)
        {
            return session.GetString(FullNameKey);
        }

        public static bool IsLoggedIn(this ISession session)
        {
            return session.GetUserId().HasValue;
        }

        public static bool IsAdmin(this ISession session)
        {
            return session.GetUserRole() == "Admin";
        }

        public static bool IsEmployee(this ISession session)
        {
            return session.GetUserRole() == "Employee";
        }

        public static void ClearUserSession(this ISession session)
        {
            session.Clear();
        }
    }
}
