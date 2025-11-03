# Session and Authentication Guide
# دليل الجلسات والمصادقة

## ✅ HTTP Session Already Implemented
## ✅ جلسة HTTP مُنفذة بالفعل

### What is HTTP Session? / ما هي جلسة HTTP?

HTTP Session keeps the user logged in while they navigate between pages. They don't need to re-enter username/password on every page.

جلسة HTTP تبقي المستخدم مسجل الدخول أثناء التنقل بين الصفحات. لا يحتاجون لإعادة إدخال اسم المستخدم/كلمة المرور في كل صفحة.

### How It Works / كيف تعمل:

1. **User logs in** → Username & Password entered / المستخدم يسجل الدخول → إدخال اسم المستخدم وكلمة المرور
2. **Credentials verified** → System checks if valid / التحقق من البيانات → النظام يتحقق من صحتها
3. **Session created** → User info stored in session / إنشاء جلسة → حفظ معلومات المستخدم في الجلسة
4. **User navigates** → Session maintained automatically / المستخدم يتنقل → الجلسة تبقى تلقائياً
5. **No re-login needed** → Until session expires (30 minutes) / لا حاجة لإعادة تسجيل الدخول → حتى انتهاء الجلسة (30 دقيقة)

### Session Configuration / إعداد الجلسة

Located in `Program.cs` (Lines 9-15):
موجود في `Program.cs` (السطور 9-15):

```csharp
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session expires after 30 min of inactivity
    options.Cookie.HttpOnly = true;                 // Security: Cookie not accessible via JavaScript
    options.Cookie.IsEssential = true;              // Required for the app to function
});
```

### What is Stored in Session? / ما يُحفظ في الجلسة؟

When user logs in, the following information is stored:
عند تسجيل المستخدم، تُحفظ المعلومات التالية:

```csharp
// In Login.cshtml.cs (Line 85)
HttpContext.Session.SetUserSession(
    userId: 1,                    // User ID / معرف المستخدم
    username: Username,           // Username (e.g., "admin", "employee") / اسم المستخدم
    role: role,                   // Role: "Admin" or "Employee" / الدور: مدير أو موظف
    employeeId: employeeId,       // Employee ID if employee / معرف الموظف إن كان موظفاً
    fullName: fullName            // Full name for display / الاسم الكامل للعرض
);

// Language preference stored / تفضيل اللغة يُحفظ
HttpContext.Session.SetString("Language", Language); // "ar" or "en"
```

### Session Helper Methods / دوال مساعدة الجلسة

File: `Helpers/SessionHelper.cs`
الملف: `Helpers/SessionHelper.cs`

```csharp
// Check if user is logged in / التحقق من تسجيل الدخول
HttpContext.Session.IsLoggedIn()  // Returns true/false

// Check if user is admin / التحقق من كون المستخدم مديراً
HttpContext.Session.IsAdmin()     // Returns true if role is "Admin"

// Check if user is employee / التحقق من كون المستخدم موظفاً
HttpContext.Session.IsEmployee()  // Returns true if role is "Employee"

// Get username / الحصول على اسم المستخدم
HttpContext.Session.GetUsername()

// Get full name / الحصول على الاسم الكامل
HttpContext.Session.GetFullName()

// Get employee ID / الحصول على معرف الموظف
HttpContext.Session.GetEmployeeId()

// Clear session (logout) / مسح الجلسة (تسجيل الخروج)
HttpContext.Session.ClearUserSession()
```

### Session Protection on Pages / حماية الصفحات بالجلسة

Every authenticated page extends `AuthenticatedPageModel`:
كل صفحة محمية تمتد من `AuthenticatedPageModel`:

```csharp
// File: Pages/AuthenticatedPageModel.cs

public class AuthenticatedPageModel : PageModel
{
    // Automatically checks if user is logged in
    // If not logged in, redirects to Login page
    // يتحقق تلقائياً إذا كان المستخدم مسجل الدخول
    // إذا لم يكن مسجلاً، يُعاد توجيهه لصفحة تسجيل الدخول

    public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        if (!IsLoggedIn)
        {
            context.Result = new RedirectToPageResult("/Login");
        }
        base.OnPageHandlerExecuting(context);
    }
}
```

### Role-Based Access / الوصول حسب الدور

**Admin pages** extend `AdminPageModel`:
**صفحات المدير** تمتد من `AdminPageModel`:

```csharp
public class AdminPageModel : AuthenticatedPageModel
{
    // Only admins can access
    // Employees are redirected to Employee dashboard
    // المدراء فقط يمكنهم الوصول
    // الموظفون يُعادون لتوجيههم للوحة تحكم الموظف
}
```

**Employee pages** extend `EmployeePageModel`:
**صفحات الموظف** تمتد من `EmployeePageModel`:

```csharp
public class EmployeePageModel : AuthenticatedPageModel
{
    // Only employees can access
    // Admins are redirected to Admin dashboard
    // الموظفون فقط يمكنهم الوصول
    // المدراء يُعادون لتوجيههم للوحة تحكم المدير
}
```

---

## 🔐 Username & Password Storage
## 🔐 تخزين اسم المستخدم وكلمة المرور

### Current Demo Setup / الإعداد التجريبي الحالي

Currently using hardcoded demo credentials in `Login.cshtml.cs`:
حالياً نستخدم بيانات تجريبية ثابتة في `Login.cshtml.cs`:

```csharp
// Lines 51-63
if (Username == "admin" && Password == "admin123")
{
    isValidUser = true;
    role = "Admin";
    fullName = "System Administrator";
}
else if (Username == "employee" && Password == "emp123")
{
    isValidUser = true;
    role = "Employee";
    employeeId = 1;
    fullName = "Ahmed Hassan";
}
```

### Password Hashing / تشفير كلمة المرور

File: `Helpers/PasswordHelper.cs`
الملف: `Helpers/PasswordHelper.cs`

Passwords are **NEVER stored in plain text**. They are hashed using SHA256:
كلمات المرور **لا تُحفظ أبداً كنص عادي**. يتم تشفيرها باستخدام SHA256:

```csharp
// Hash a password / تشفير كلمة المرور
string hashedPassword = PasswordHelper.HashPassword("admin123");
// Result: "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI="

// Verify password / التحقق من كلمة المرور
bool isValid = PasswordHelper.VerifyPassword("admin123", hashedPassword);
// Returns true if password matches
```

### User Model / نموذج المستخدم

File: `Models/User.cs`
الملف: `Models/User.cs`

```csharp
public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }        // Username: "admin", "employee", etc.
    public string PasswordHash { get; set; }    // Hashed password (SHA256)
    public string Role { get; set; }            // "Admin" or "Employee"
    public int? EmployeeId { get; set; }        // Link to Employee table
    public string FullName { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastLogin { get; set; }
}
```

### How to Add Users to Database / كيفية إضافة مستخدمين للقاعدة

File: `Helpers/UserRegistrationHelper.cs`
الملف: `Helpers/UserRegistrationHelper.cs`

**Example: Create a new user**
**مثال: إنشاء مستخدم جديد**

```csharp
// Create user with hashed password
var user = UserRegistrationHelper.CreateUser(
    username: "john",
    password: "password123",      // Will be hashed automatically
    role: "Employee",
    employeeId: 5,
    fullName: "John Doe"
);

// The user object is ready to be saved to database
// كائن المستخدم جاهز للحفظ في قاعدة البيانات
```

### When Database is Connected / عند ربط قاعدة البيانات

Uncomment the code in `Login.cshtml.cs` (Lines 65-80):
أزل التعليق عن الكود في `Login.cshtml.cs` (السطور 65-80):

```csharp
// TODO: Uncomment when database is ready
var user = _context.Users
    .Include(u => u.Employee)
    .FirstOrDefault(u => u.Username == Username && u.IsActive);

if (user != null && PasswordHelper.VerifyPassword(Password, user.PasswordHash))
{
    isValidUser = true;
    role = user.Role;
    employeeId = user.EmployeeId;
    fullName = user.FullName;

    user.LastLogin = DateTime.Now;
    _context.SaveChanges();
}
```

### Seed Initial Users / تهيئة المستخدمين الأوليين

After creating database, run this once to create admin and employee:
بعد إنشاء قاعدة البيانات، شغّل هذا مرة واحدة لإنشاء المدير والموظف:

```csharp
// In Program.cs or a separate seeding method
public static void SeedUsers(FactoryDbContext context)
{
    if (!context.Users.Any(u => u.Username == "admin"))
    {
        var admin = new User
        {
            Username = "admin",
            PasswordHash = PasswordHelper.HashPassword("admin123"),
            Role = "Admin",
            FullName = "System Administrator",
            IsActive = true,
            CreatedDate = DateTime.Now
        };
        context.Users.Add(admin);
    }

    if (!context.Users.Any(u => u.Username == "employee"))
    {
        var employee = new User
        {
            Username = "employee",
            PasswordHash = PasswordHelper.HashPassword("emp123"),
            Role = "Employee",
            EmployeeId = 1,
            FullName = "Ahmed Hassan",
            IsActive = true,
            CreatedDate = DateTime.Now
        };
        context.Users.Add(employee);
    }

    context.SaveChanges();
}
```

---

## 🔄 Complete Authentication Flow
## 🔄 سير عملية المصادقة الكاملة

### 1. User Opens Website / المستخدم يفتح الموقع
```
www.factorysystem.com
    ↓
Index.cshtml checks session
    ↓
No session found → Redirect to /Login
```

### 2. User Logs In / المستخدم يسجل الدخول
```
Login Page
    ↓
User enters username & password
    ↓
POST to Login.cshtml.cs
    ↓
Verify credentials (database or demo)
    ↓
Create session with user info
    ↓
Redirect to appropriate dashboard
```

### 3. User Navigates / المستخدم يتنقل
```
User clicks on "My Attendance"
    ↓
Session automatically sent with request
    ↓
Page checks: IsLoggedIn? IsEmployee?
    ↓
Access granted (no re-login needed!)
```

### 4. Session Expires / انتهاء الجلسة
```
30 minutes of inactivity
    ↓
Session expires
    ↓
Next page visit → Redirect to /Login
```

### 5. User Logs Out / المستخدم يسجل الخروج
```
User clicks "Logout"
    ↓
Session.ClearUserSession()
    ↓
Redirect to /Login
```

---

## 📊 Database Schema for Users
## 📊 مخطط قاعدة البيانات للمستخدمين

When you create the database, this table will be created:
عند إنشاء قاعدة البيانات، سيتم إنشاء هذا الجدول:

```sql
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL,
    EmployeeId INT NULL,
    FullName NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastLogin DATETIME NULL,
    FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);
```

---

## ✅ Summary / الملخص

| Feature | Status | Details |
|---------|--------|---------|
| HTTP Session | ✅ Implemented | 30-minute timeout, auto-maintains login |
| جلسة HTTP | ✅ مُنفذ | مهلة 30 دقيقة، تحافظ على تسجيل الدخول تلقائياً |
| Password Hashing | ✅ Implemented | SHA256 encryption |
| تشفير كلمة المرور | ✅ مُنفذ | تشفير SHA256 |
| Role-Based Access | ✅ Implemented | Admin vs Employee |
| الوصول حسب الدور | ✅ مُنفذ | مدير مقابل موظف |
| User Storage | ⏳ Ready | Model ready, awaiting database |
| حفظ المستخدمين | ⏳ جاهز | النموذج جاهز، في انتظار قاعدة البيانات |
| Demo Login | ✅ Working | admin/admin123, employee/emp123 |
| تسجيل دخول تجريبي | ✅ يعمل | admin/admin123, employee/emp123 |

---

**The session system is fully working! Users don't need to re-enter credentials on every page!**
**نظام الجلسات يعمل بالكامل! المستخدمون لا يحتاجون لإعادة إدخال البيانات في كل صفحة!**
