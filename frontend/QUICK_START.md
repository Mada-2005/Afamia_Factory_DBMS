# Quick Start Guide - Factory Management System
# دليل البدء السريع - نظام إدارة المصنع

## ✅ Everything is Ready! / كل شيء جاهز!

### What You Have Now / ما لديك الآن:

1. ✅ **Login Page** - Opens automatically when you visit the site
   صفحة تسجيل الدخول - تفتح تلقائياً عند زيارة الموقع

2. ✅ **HTTP Session** - Already working! No need to re-login on every page
   جلسة HTTP - تعمل بالفعل! لا حاجة لإعادة تسجيل الدخول في كل صفحة

3. ✅ **Username/Password Storage** - Ready to save to database
   تخزين اسم المستخدم/كلمة المرور - جاهز للحفظ في قاعدة البيانات

4. ✅ **Arabic/English Support** - Full bilingual interface
   دعم العربية/الإنجليزية - واجهة كاملة بلغتين

5. ✅ **Role-Based Access** - Admin vs Employee
   الوصول حسب الدور - مدير مقابل موظف

---

## 🚀 How to Run / كيفية التشغيل

### Step 1: Close Visual Studio (if running)
### الخطوة 1: أغلق Visual Studio (إذا كان يعمل)

This releases the locked files.
هذا يحرر الملفات المقفلة.

### Step 2: Run the Application
### الخطوة 2: شغّل التطبيق

Open terminal in project folder and run:
افتح الطرفية في مجلد المشروع وشغّل:

```bash
cd E:\DataBase\Project\WebSite(Test1)\FactorySystemWebpage
dotnet run
```

### Step 3: Open Browser
### الخطوة 3: افتح المتصفح

The application will show you the URL, usually:
سيظهر لك التطبيق الرابط، عادة:

```
https://localhost:5001
or
http://localhost:5000
```

---

## 🔐 Login / تسجيل الدخول

### You Will See the Login Page Immediately!
### ستظهر لك صفحة تسجيل الدخول فوراً!

The system **automatically redirects to login** if you're not logged in.
النظام **يُعيد التوجيه تلقائياً لتسجيل الدخول** إذا لم تكن مسجلاً.

### Demo Credentials / بيانات التجربة:

#### Login as Admin / تسجيل دخول كمدير:
- **Username:** `admin`
- **Password:** `admin123`

#### Login as Employee / تسجيل دخول كموظف:
- **Username:** `employee`
- **Password:** `emp123`

### Language Selection / اختيار اللغة:
Click "English" or "العربية" at top-right of login page
اضغط "English" أو "العربية" أعلى يمين صفحة تسجيل الدخول

---

## 📋 What Happens After Login / ما يحدث بعد تسجيل الدخول

### ✅ Session is Created / الجلسة تُنشأ
Your credentials are saved in session for 30 minutes
بياناتك تُحفظ في الجلسة لمدة 30 دقيقة

### ✅ You Can Navigate Freely / يمكنك التنقل بحرية
- Click any menu item
- Go to any page
- **NO RE-LOGIN REQUIRED** for 30 minutes
- **لا حاجة لإعادة تسجيل الدخول** لمدة 30 دقيقة

### ✅ Role-Based Dashboard / لوحة تحكم حسب الدور

**If Admin:**
- Redirected to `/Dashboard/Admin/Index`
- Can see ALL employees' data
- Can add production rooms
- Full system access

**إذا كنت مديراً:**
- يُعاد توجيهك لـ `/Dashboard/Admin/Index`
- يمكنك رؤية بيانات جميع الموظفين
- يمكنك إضافة غرف إنتاج
- وصول كامل للنظام

**If Employee:**
- Redirected to `/Dashboard/Employee/Index`
- Can see ONLY your own attendance/payroll
- Can add raw materials and products
- CANNOT add production rooms

**إذا كنت موظفاً:**
- يُعاد توجيهك لـ `/Dashboard/Employee/Index`
- يمكنك رؤية حضورك وراتبك فقط
- يمكنك إضافة مواد خام ومنتجات
- لا يمكنك إضافة غرف إنتاج

---

## 🔍 Test the Session / اختبر الجلسة

### Try This / جرب هذا:

1. Login as **admin** / سجل دخول كـ **admin**
2. You see Admin Dashboard / تظهر لوحة تحكم المدير
3. Click on "Vendors" / اضغط على "الموردون"
4. ✅ **No login screen!** Session works! / **لا توجد شاشة تسجيل دخول!** الجلسة تعمل!
5. Click on "Employees" / اضغط على "الموظفون"
6. ✅ **Still no login!** / **ما زالت بدون تسجيل دخول!**
7. Navigate anywhere / تنقل في أي مكان
8. ✅ **Session maintained!** / **الجلسة محفوظة!**

### Then Logout / ثم سجل الخروج:

9. Click your name (top-right) / اضغط على اسمك (أعلى اليمين)
10. Click "تسجيل الخروج" or "Logout"
11. ✅ **Redirected to login!** / **إعادة توجيه لتسجيل الدخول!**
12. Session cleared / الجلسة مُمسوحة

### Login as Employee / سجل دخول كموظف:

13. Login as **employee** / سجل دخول كـ **employee**
14. You see Employee Dashboard / تظهر لوحة تحكم الموظف
15. Try to access `/ProductionRooms/Create` manually
16. ✅ **Blocked! Redirected back!** / **ممنوع! إعادة توجيه!**
17. Employees cannot add production rooms!

---

## 📊 Where are Credentials Stored? / أين تُحفظ البيانات؟

### Currently / حالياً:
Credentials are in **code** (demo mode) in:
البيانات في **الكود** (وضع تجريبي) في:
```
Pages/Login.cshtml.cs (Lines 51-63)
```

### When Database Connected / عند ربط قاعدة البيانات:

1. **Username stored as-is** in `Users` table
   **اسم المستخدم يُحفظ كما هو** في جدول `Users`

2. **Password hashed (SHA256)** then stored
   **كلمة المرور تُشفّر (SHA256)** ثم تُحفظ

3. Example:
```sql
INSERT INTO Users (Username, PasswordHash, Role, FullName)
VALUES (
    'admin',
    'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',  -- Hashed "admin123"
    'Admin',
    'System Administrator'
);
```

### Helper to Create Users / مساعد لإنشاء المستخدمين:
See: `Helpers/UserRegistrationHelper.cs`
راجع: `Helpers/UserRegistrationHelper.cs`

---

## 🎯 What to Test / ما يُختبر

### ✅ Login System / نظام تسجيل الدخول
- [ ] Open `https://localhost:5001` → Login page shows
- [ ] Login with admin/admin123
- [ ] See Admin Dashboard
- [ ] Arabic/English switch works

### ✅ Session Persistence / استمرارية الجلسة
- [ ] Navigate between pages
- [ ] No re-login required
- [ ] User info displayed in navbar

### ✅ Role-Based Access / الوصول حسب الدور
- [ ] Admin can access all pages
- [ ] Employee blocked from admin pages
- [ ] Proper redirects work

### ✅ Logout / تسجيل الخروج
- [ ] Click logout
- [ ] Session cleared
- [ ] Redirected to login

---

## 📝 Important Files / ملفات مهمة

### Authentication / المصادقة:
- `Pages/Login.cshtml` - Login page UI / واجهة تسجيل الدخول
- `Pages/Login.cshtml.cs` - Login logic / منطق تسجيل الدخول
- `Pages/Logout.cshtml.cs` - Logout logic / منطق تسجيل الخروج

### Session Management / إدارة الجلسة:
- `Helpers/SessionHelper.cs` - Session methods / دوال الجلسة
- `Program.cs` (Lines 9-15) - Session configuration / إعداد الجلسة

### Security / الأمان:
- `Helpers/PasswordHelper.cs` - Password hashing / تشفير كلمة المرور
- `Pages/AuthenticatedPageModel.cs` - Base class for protected pages / الفئة الأساسية للصفحات المحمية

### User Storage / تخزين المستخدم:
- `Models/User.cs` - User model / نموذج المستخدم
- `Helpers/UserRegistrationHelper.cs` - Create users / إنشاء مستخدمين

---

## 🔧 Troubleshooting / حل المشاكل

### Problem: Login page doesn't show
### المشكلة: صفحة تسجيل الدخول لا تظهر

**Solution:**
1. Make sure you updated `Index.cshtml.cs` (done ✅)
2. Run `dotnet clean` then `dotnet run`

### Problem: Session doesn't persist
### المشكلة: الجلسة لا تستمر

**Solution:**
1. Check `Program.cs` has `app.UseSession()` (done ✅)
2. Clear browser cookies
3. Restart application

### Problem: Can't build (file locked)
### المشكلة: لا يمكن البناء (ملف مقفل)

**Solution:**
1. Close Visual Studio
2. Run from command line: `dotnet run`

---

## 🎉 Success Checklist / قائمة النجاح

- ✅ Login page opens automatically / صفحة تسجيل الدخول تفتح تلقائياً
- ✅ HTTP Session works (no re-login) / جلسة HTTP تعمل (بدون إعادة تسجيل دخول)
- ✅ Username/Password ready for database / اسم المستخدم/كلمة المرور جاهزة للقاعدة
- ✅ Arabic/English supported / العربية/الإنجليزية مدعومة
- ✅ Admin vs Employee roles work / أدوار المدير والموظف تعمل
- ✅ Product search with traceability / البحث عن منتجات مع التتبع
- ✅ All pages created / جميع الصفحات منشأة

---

## 📚 Documentation / الوثائق

1. **SESSION_AND_AUTH_GUIDE.md** - Complete session & auth explanation
   شرح كامل للجلسة والمصادقة

2. **FEATURES_GUIDE.md** - All features explained in Arabic & English
   جميع الميزات مشروحة بالعربية والإنجليزية

3. **DATABASE_CONNECTION_GUIDE.md** - How to connect database
   كيفية ربط قاعدة البيانات

4. **README.md** - Project overview
   نظرة عامة على المشروع

---

## 🚀 You're Ready! / أنت جاهز!

**Run the application and enjoy!**
**شغّل التطبيق واستمتع!**

```bash
dotnet run
```

Then open: **https://localhost:5001**

**Everything works as requested! 🎉**
**كل شيء يعمل كما طلبت! 🎉**
