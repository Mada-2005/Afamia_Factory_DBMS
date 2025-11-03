# Factory Database Management System (FactoryDBMS)
# نظام إدارة قاعدة بيانات المصنع

A comprehensive database management system for food manufacturing factories, designed to track raw materials, production processes, employee activities, and distribution from source to delivery.

نظام شامل لإدارة قواعد البيانات لمصانع تصنيع الأغذية، مصمم لتتبع المواد الخام، عمليات الإنتاج، أنشطة الموظفين، والتوزيع من المصدر إلى التسليم.

## 📁 Project Structure / هيكل المشروع

```
FactoryDBMS/
├── frontend/          # ASP.NET Core Web Application
│   ├── Models/        # Entity Models (13 models)
│   ├── Data/          # Database Context
│   ├── Pages/         # Razor Pages (UI)
│   ├── Helpers/       # Utility Classes
│   └── wwwroot/       # Static Files
└── backend/           # (Coming soon - Database Scripts)
```

## 🚀 Frontend - ASP.NET Core Web Application

### Technologies / التقنيات:
- **ASP.NET Core 8.0** with Razor Pages
- **Entity Framework Core 8.0** for database access
- **Bootstrap 5** with RTL support for Arabic
- **Bootstrap Icons**
- **Session-based Authentication**

### Features / الميزات:

#### ✅ Authentication & Authorization / المصادقة والتفويض
- Login system with role-based access
- Admin vs Employee roles
- HTTP Session (30-minute timeout)
- Password hashing (SHA256)

#### ✅ Bilingual Support / الدعم ثنائي اللغة
- Arabic (العربية) - RTL layout
- English - LTR layout
- Language switcher on all pages

#### ✅ Admin Features / ميزات المدير
- View all employees' attendance and payroll
- Manage production rooms (Add/Edit/Delete)
- Track overall production
- Manage vendors and customers
- Access to all inventory and products

#### ✅ Employee Features / ميزات الموظف
- View own attendance only
- View own payroll only
- Add raw materials (receive materials)
- Log products (record production)
- Product search with traceability

#### ✅ Product Traceability / تتبع المنتجات
- Search by product code
- View complete product information
- Track raw materials used
- See production room and employees involved
- Distribution information

### Entity Models / نماذج الكيانات

Based on the Factory ER Diagram, includes:
بناءً على مخطط ER للمصنع، يتضمن:

1. **Vendor** - Supplier information
2. **RawMaterial** - Raw materials with batch tracking
3. **Employee** - Employee records
4. **MonthlyPay** - Payroll management
5. **DailyWorkSchedule** - Attendance tracking
6. **Product** - Finished products
7. **Customer** - Distribution points
8. **Inventory** - Warehouse management
9. **ProductionRoom** - Production facilities
10. **ProductionPipelineLine** - Production lines
11. **ProductMaterial** - Junction table
12. **CustomerProduct** - Junction table
13. **EmployeeProductionLine** - Junction table
14. **User** - Authentication

## 🔐 Demo Credentials / بيانات التجربة

### Admin Access / وصول المدير:
- **Username:** `admin`
- **Password:** `admin123`

### Employee Access / وصول الموظف:
- **Username:** `employee`
- **Password:** `emp123`

## 📖 Installation & Setup / التثبيت والإعداد

### Prerequisites / المتطلبات:
- .NET 8.0 SDK
- SQL Server (for database)
- Git

### Steps / الخطوات:

1. **Clone the repository / استنساخ المستودع:**
```bash
git clone https://github.com/YOUR_USERNAME/FactoryDBMS.git
cd FactoryDBMS/frontend
```

2. **Restore packages / استعادة الحزم:**
```bash
dotnet restore
```

3. **Build the project / بناء المشروع:**
```bash
dotnet build
```

4. **Run the application / تشغيل التطبيق:**
```bash
dotnet run
```

5. **Open browser / افتح المتصفح:**
```
https://localhost:5001
```

## 🗄️ Database Connection / ربط قاعدة البيانات

### Current Status / الحالة الحالية:
- ✅ Models ready
- ✅ DbContext configured
- ✅ Connection string placeholder
- ⏳ Awaiting database creation

### To Connect Database / لربط قاعدة البيانات:

1. Update connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=FactoryDB;Trusted_Connection=True;"
  }
}
```

2. Uncomment DbContext registration in `Program.cs`:
```csharp
builder.Services.AddDbContext<FactoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

3. Create database migration:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

See `DATABASE_CONNECTION_GUIDE.md` for detailed instructions.

## 📚 Documentation / الوثائق

- **README.md** - This file / هذا الملف
- **QUICK_START.md** - Quick start guide / دليل البدء السريع
- **SESSION_AND_AUTH_GUIDE.md** - Authentication explained / شرح المصادقة
- **FEATURES_GUIDE.md** - Complete features documentation / وثائق الميزات الكاملة
- **DATABASE_CONNECTION_GUIDE.md** - Database setup / إعداد قاعدة البيانات

## 🎯 Use Cases / حالات الاستخدام

This system is designed for food manufacturing factories producing:
- Crackers / البسكويت المالح
- Biscuits / البسكويت الحلو
- Peanuts / الفول السوداني
- Snacks / الوجبات الخفيفة

### Key Capabilities / القدرات الرئيسية:
- Complete traceability from raw material to customer
- Quality control through batch tracking
- Employee attendance and payroll automation
- Inventory management with expiration tracking
- Distribution coordination

## 🔧 Technology Stack / مجموعة التقنيات

### Backend / الخلفية:
- ASP.NET Core 8.0
- Entity Framework Core 8.0
- C# 12

### Frontend / الواجهة الأمامية:
- Razor Pages
- Bootstrap 5 (with RTL)
- Bootstrap Icons
- JavaScript/jQuery

### Database / قاعدة البيانات:
- SQL Server
- Entity Framework Core Migrations

### Authentication / المصادقة:
- Session-based authentication
- SHA256 password hashing
- Role-based authorization

## 📊 Database Schema / مخطط قاعدة البيانات

See the ER Diagram document at:
راجع مخطط ER في:
```
E:\DataBase\Project\ERD\Factory_ER_Diagram_Report.pdf
```

## 🤝 Contributing / المساهمة

This is an academic project for database management systems course.
هذا مشروع أكاديمي لدورة أنظمة إدارة قواعد البيانات.

## 📄 License / الترخيص

Educational/Academic Use
استخدام تعليمي/أكاديمي

## 👥 Team / الفريق

Zewail City of Science and Technology
Communications & Information Engineering Program
CIE 206 - Database Management Systems

---

**Status:** ✅ Frontend Complete | ⏳ Database Integration Pending
**الحالة:** ✅ الواجهة الأمامية مكتملة | ⏳ في انتظار ربط قاعدة البيانات

---

For questions or issues, please refer to the documentation files in the `frontend` folder.
للأسئلة أو المشاكل، يرجى الرجوع إلى ملفات الوثائق في مجلد `frontend`.
