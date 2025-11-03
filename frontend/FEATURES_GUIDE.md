# Factory Management System - Features Guide
# دليل نظام إدارة المصنع

## ✅ Implemented Features / الميزات المنفذة

### 1. Authentication System / نظام المصادقة
- **Login Page** with Arabic/English language switcher
- **Role-Based Access**: Admin vs Employee
- **Session Management** for maintaining user state
- **Demo Credentials**:
  - Admin: `admin` / `admin123`
  - Employee: `employee` / `emp123`

**صفحة تسجيل الدخول** مع مبدل اللغة العربية/الإنجليزية
**التحكم بالأدوار**: مدير مقابل موظف
**إدارة الجلسات** للحفاظ على حالة المستخدم

### 2. Arabic Language Support / دعم اللغة العربية
- **Bilingual Interface**: English & Arabic
- **RTL Layout** for Arabic
- **Language Switcher** in:
  - Login page / صفحة تسجيل الدخول
  - Main navigation / القائمة الرئيسية
- **All pages translated** / جميع الصفحات مترجمة

### 3. Admin Features / ميزات المدير

#### Admin Dashboard / لوحة تحكم المدير
- Overview statistics
- Quick actions
- Expiring materials alert

#### Access to All Features / الوصول لجميع الميزات:
- ✅ View All Employees / عرض جميع الموظفين
- ✅ Manage Attendance (All Employees) / إدارة الحضور (جميع الموظفين)
- ✅ Manage Payroll (All Employees) / إدارة الرواتب (جميع الموظفين)
- ✅ Add/Edit/Delete Production Rooms / إضافة/تعديل/حذف غرف الإنتاج
- ✅ View Raw Materials / عرض المواد الخام
- ✅ View Products / عرض المنتجات
- ✅ View Inventory / عرض المخزون
- ✅ Manage Vendors / إدارة الموردين
- ✅ Manage Customers / إدارة العملاء
- ✅ Production Tracking / تتبع الإنتاج

### 4. Employee Features / ميزات الموظف

#### Employee Dashboard / لوحة تحكم الموظف
- Personal information
- Quick links to personal data
- Today's summary

#### Limited Access / الوصول المحدود:
- ✅ View My Attendance Only / عرض حضوري فقط
- ✅ View My Payroll Only / عرض راتبي فقط
- ✅ Add Raw Materials (Receive) / إضافة مواد خام (استلام)
- ✅ Log Products (Record Production) / تسجيل منتجات (تسجيل الإنتاج)
- ✅ Product Search / البحث عن منتج
- ❌ **CANNOT**: Add Production Rooms / **لا يمكن**: إضافة غرف إنتاج
- ❌ **CANNOT**: View Other Employees' Data / **لا يمكن**: عرض بيانات موظفين آخرين

### 5. Product Traceability / تتبع المنتج
Complete traceability system for products:
نظام تتبع كامل للمنتجات:

- ✅ Search by Product Code / البحث برمز المنتج
- ✅ View Product Information / عرض معلومات المنتج
- ✅ View Raw Materials Used / عرض المواد الخام المستخدمة
- ✅ View Production Room Details / عرض تفاصيل غرفة الإنتاج
- ✅ View Employees Involved / عرض الموظفين المشاركين
- ✅ View Distribution Information / عرض معلومات التوزيع

*Note: Full data will display once database is connected*
*ملاحظة: ستظهر البيانات الكاملة بمجرد الاتصال بقاعدة البيانات*

### 6. Navigation System / نظام التنقل
Organized menu structure with role-based visibility:
هيكل قائمة منظم مع رؤية حسب الدور:

#### Admin Menu / قائمة المدير:
- 🏠 Home / الرئيسية
- 📦 Inventory / المخزون
  - Raw Materials / المواد الخام
  - Products / المنتجات
  - Warehouse / المستودع
- 👥 Employees / الموظفون
  - Employee List / قائمة الموظفين
  - Attendance / الحضور
  - Payroll / الرواتب
- ⚙️ Production / الإنتاج
  - Production Rooms / غرف الإنتاج
  - Production Tracking / تتبع الإنتاج
- 💼 Business / الأعمال
  - Vendors / الموردون
  - Customers / العملاء

#### Employee Menu / قائمة الموظف:
- 🏠 Home / الرئيسية
- 📅 My Attendance / حضوري
- 💰 My Payroll / راتبي
- ➕ Add / إضافة
  - Receive Raw Material / استلام مواد خام
  - Log Product / تسجيل منتج
- 🔍 Product Search / البحث عن منتج

## 🔐 Security Features / ميزات الأمان

1. **Session-Based Authentication** / **مصادقة قائمة على الجلسة**
2. **Role-Based Authorization** / **تفويض قائم على الأدوار**
3. **Page-Level Protection** / **حماية على مستوى الصفحة**
   - Admin pages redirect employees
   - Employee pages redirect admins
4. **Password Hashing (SHA256)** / **تشفير كلمة المرور**

## 📁 Pages Structure / هيكل الصفحات

```
Pages/
├── Login.cshtml                    # صفحة تسجيل الدخول
├── Logout.cshtml                   # تسجيل الخروج
├── ProductSearch.cshtml            # البحث عن منتج
├── Dashboard/
│   ├── Admin/
│   │   ├── Index.cshtml           # لوحة تحكم المدير
│   │   ├── Attendance.cshtml      # إدارة الحضور (الكل)
│   │   └── Payroll.cshtml         # إدارة الرواتب (الكل)
│   └── Employee/
│       ├── Index.cshtml           # لوحة تحكم الموظف
│       ├── MyAttendance.cshtml    # حضوري فقط
│       └── MyPayroll.cshtml       # راتبي فقط
├── Vendors/
│   ├── Index.cshtml               # قائمة الموردين
│   └── Create.cshtml              # إضافة مورد
├── Customers/
│   ├── Index.cshtml               # قائمة العملاء
│   └── Create.cshtml              # إضافة عميل
├── ProductionRooms/               # (Admin Only / مدير فقط)
│   ├── Index.cshtml               # قائمة غرف الإنتاج
│   └── Create.cshtml              # إضافة غرفة إنتاج
└── Shared/
    └── _LayoutArabic.cshtml       # القالب العربي/الإنجليزي
```

## 🎨 UI Features / ميزات الواجهة

1. **Bootstrap 5** with RTL support for Arabic
2. **Bootstrap Icons** for visual elements
3. **Responsive Design** / **تصميم متجاوب**
4. **Color-Coded Cards** / **بطاقات ملونة**
5. **Professional Gradients** / **تدرجات احترافية**

## 🔄 How to Switch Languages / كيفية تبديل اللغات

### Method 1: Login Page / طريقة 1: صفحة تسجيل الدخول
Click "English" or "العربية" buttons at top-right

### Method 2: Main Navigation / طريقة 2: القائمة الرئيسية
1. Click on your name (top-right) / اضغط على اسمك (أعلى اليمين)
2. Select "English" or "العربية"
3. Page will reload in selected language / ستُعاد تحميل الصفحة باللغة المختارة

## 📊 Database Connection / اتصال قاعدة البيانات

### Current Status / الحالة الحالية:
- ✅ Models created / النماذج منشأة
- ✅ DbContext ready / DbContext جاهز
- ✅ UI complete / الواجهة كاملة
- ⏳ Waiting for database connection / في انتظار الاتصال بقاعدة البيانات

### What Happens After Database Connection / ما سيحدث بعد الاتصال بقاعدة البيانات:
1. All "--" placeholders will show real data
2. Product search will work with actual products
3. Attendance and payroll will display real records
4. CRUD operations will persist to database

جميع البيانات "--" ستظهر بيانات حقيقية
البحث عن المنتجات سيعمل مع المنتجات الفعلية
الحضور والرواتب ستعرض سجلات حقيقية
عمليات الإضافة/التعديل/الحذف ستحفظ في قاعدة البيانات

## 🚀 Running the Application / تشغيل التطبيق

```bash
dotnet run
```

Then open / ثم افتح:
- https://localhost:5001
- http://localhost:5000

### First Time? / المرة الأولى؟
1. Go to login page / اذهب لصفحة تسجيل الدخول
2. Use demo credentials / استخدم بيانات التجربة:
   - **Admin**: admin / admin123
   - **Employee**: employee / emp123
3. Choose language / اختر اللغة
4. Explore! / استكشف!

## 📝 Notes / ملاحظات

### Admin vs Employee Summary / ملخص المدير مقابل الموظف

| Feature | Admin (مدير) | Employee (موظف) |
|---------|-------------|------------------|
| Add Production Rooms | ✅ Yes | ❌ No |
| View All Attendance | ✅ Yes | ❌ No |
| View All Payroll | ✅ Yes | ❌ No |
| View Own Attendance | ✅ Yes | ✅ Yes |
| View Own Payroll | ✅ Yes | ✅ Yes |
| Add Raw Materials | ✅ Yes | ✅ Yes |
| Log Products | ✅ Yes | ✅ Yes |
| Product Search | ✅ Yes | ✅ Yes |
| Manage Vendors | ✅ Yes | ❌ No |
| Manage Customers | ✅ Yes | ❌ No |

## 🎯 Next Steps / الخطوات التالية

1. **Connect Database** / **ربط قاعدة البيانات**
   - See `DATABASE_CONNECTION_GUIDE.md`
   - راجع `DATABASE_CONNECTION_GUIDE.md`

2. **Test with Real Data** / **الاختبار بالبيانات الحقيقية**
3. **Create More Users** / **إنشاء المزيد من المستخدمين**
4. **Customize as Needed** / **تخصيص حسب الحاجة**

---
**Created with:** ASP.NET Core 8.0 + Razor Pages + Bootstrap 5 + Arabic/English Support
**تم الإنشاء باستخدام:** ASP.NET Core 8.0 + Razor Pages + Bootstrap 5 + دعم العربية/الإنجليزية
