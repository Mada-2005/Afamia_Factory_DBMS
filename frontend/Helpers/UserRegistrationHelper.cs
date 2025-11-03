using FactorySystemWebpage.Models;
using FactorySystemWebpage.Data;

namespace FactorySystemWebpage.Helpers
{
    /// <summary>
    /// Helper class to register new users and save them to the database
    /// This demonstrates how username and password are stored
    /// </summary>
    public static class UserRegistrationHelper
    {
        /// <summary>
        /// Creates a new user with hashed password
        /// Username and hashed password will be saved to database
        /// </summary>
        public static User CreateUser(string username, string password, string role, int? employeeId = null, string fullName = "")
        {
            var user = new User
            {
                Username = username,
                PasswordHash = PasswordHelper.HashPassword(password), // Password is hashed using SHA256
                Role = role, // "Admin" or "Employee"
                EmployeeId = employeeId,
                FullName = fullName,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            return user;
        }

        /// <summary>
        /// Example: How to save users to database (Uncomment when database is ready)
        /// </summary>
        /*
        public static void SaveUserToDatabase(FactoryDbContext context, string username, string password, string role, int? employeeId = null, string fullName = "")
        {
            // Create user with hashed password
            var user = CreateUser(username, password, role, employeeId, fullName);

            // Add to database
            context.Users.Add(user);
            context.SaveChanges();
        }
        */

        /// <summary>
        /// Example: Seed initial admin and employee users
        /// Call this method once after creating the database
        /// </summary>
        /*
        public static void SeedInitialUsers(FactoryDbContext context)
        {
            // Check if admin exists
            if (!context.Users.Any(u => u.Username == "admin"))
            {
                var admin = CreateUser("admin", "admin123", "Admin", null, "System Administrator");
                context.Users.Add(admin);
            }

            // Check if demo employee exists
            if (!context.Users.Any(u => u.Username == "employee"))
            {
                var employee = CreateUser("employee", "emp123", "Employee", 1, "Ahmed Hassan");
                context.Users.Add(employee);
            }

            context.SaveChanges();
        }
        */
    }
}
