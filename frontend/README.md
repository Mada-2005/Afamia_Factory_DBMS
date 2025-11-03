# Factory Management System - ASP.NET Core Web Application

## Overview
This is a comprehensive Factory Management System built with ASP.NET Core 8.0 Razor Pages, designed to manage all aspects of a food manufacturing factory including raw materials, production, employees, and distribution.

## Current Status
✅ **Completed Components:**
- Project structure and folder organization
- All entity models (13 models total)
- Database context (FactoryDbContext) - ready for database connection
- Professional factory-themed navigation layout
- Dashboard with overview widgets
- CRUD pages for Vendors, Customers, and Production Rooms
- Entity Framework Core packages installed

⏳ **Pending - Database Connection Required:**
- Database creation and migration
- Full CRUD operations for all entities
- Data binding and display
- Advanced features (traceability, reporting, etc.)

## Project Structure

```
FactorySystemWebpage/
├── Models/                    # Entity models
│   ├── Vendor.cs
│   ├── RawMaterial.cs
│   ├── Employee.cs
│   ├── MonthlyPay.cs
│   ├── DailyWorkSchedule.cs
│   ├── Product.cs
│   ├── Customer.cs
│   ├── Inventory.cs
│   ├── ProductionRoom.cs
│   ├── ProductionPipelineLine.cs
│   ├── ProductMaterial.cs      # Junction table
│   ├── CustomerProduct.cs      # Junction table
│   └── EmployeeProductionLine.cs  # Junction table
├── Data/
│   └── FactoryDbContext.cs    # EF Core DbContext
├── Pages/
│   ├── Dashboard/             # Dashboard pages
│   ├── Vendors/               # Vendor CRUD pages
│   ├── Customers/             # Customer CRUD pages
│   ├── ProductionRooms/       # Production Room CRUD pages
│   ├── Employees/             # (To be created)
│   ├── RawMaterials/          # (To be created)
│   ├── Products/              # (To be created)
│   └── Inventory/             # (To be created)
└── wwwroot/                   # Static files
```

## Features Implemented

### 1. Entity Models
All 10 main entities from the ER diagram:
- **Vendor** - Supplier information
- **RawMaterial** - Raw materials with batch tracking
- **Employee** - Employee records and roles
- **MonthlyPay** - Payroll with salary calculations
- **DailyWorkSchedule** - Attendance and shift management
- **Product** - Finished products with traceability
- **Customer** - Distribution points (traders/supermarkets)
- **Inventory** - Warehouse storage management
- **ProductionRoom** - Physical production facilities
- **ProductionPipelineLine** - Production lines within rooms

### 2. Navigation System
Professional navigation with categorized dropdowns:
- **Inventory** - Raw Materials, Products, Warehouse
- **Production** - Production Rooms, Production Tracking
- **Employees** - Employee List, Attendance, Payroll
- **Business** - Vendors, Customers, Distribution
- **Dashboard** - Quick overview and statistics

### 3. Dashboard
Central dashboard with:
- Quick statistics widgets
- Product traceability search
- Expiring materials alerts
- Recent production tracking

### 4. CRUD Pages
Basic Create and Index pages for:
- Vendors (suppliers)
- Customers (distribution points)
- Production Rooms

## Technologies Used
- **ASP.NET Core 8.0** - Web framework
- **Razor Pages** - Page-based programming model
- **Entity Framework Core 8.0** - ORM for database access
- **Bootstrap 5** - UI framework
- **Bootstrap Icons** - Icon library
- **SQL Server** - Database (configured but not connected)

## Next Steps - Database Integration

### 1. Update Connection String
Edit `appsettings.json` to match your SQL Server instance:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=FactoryDB;Trusted_Connection=True;"
}
```

### 2. Uncomment DbContext Registration
In `Program.cs`, uncomment these lines:
```csharp
builder.Services.AddDbContext<FactoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

### 3. Create Database Migration
Run these commands in the terminal:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Update Page Models
Once database is connected, update the `.cshtml.cs` files to:
- Inject `FactoryDbContext`
- Load data from database
- Save changes to database

Example for Vendors/Index.cshtml.cs:
```csharp
private readonly FactoryDbContext _context;

public IndexModel(FactoryDbContext context)
{
    _context = context;
}

public async Task OnGetAsync()
{
    Vendors = await _context.Vendors.ToListAsync();
}
```

## Pages to Create

The following pages still need to be created (similar to Vendors/Customers):

1. **Employees** - Full CRUD
2. **RawMaterials** - Full CRUD with expiration tracking
3. **Products** - Full CRUD with traceability
4. **Inventory** - Warehouse management
5. **Production Tracking** - Daily production logging
6. **Attendance** - Employee attendance tracking
7. **Payroll** - Monthly payroll processing

## Running the Application

1. Make sure you have .NET 8.0 SDK installed
2. Navigate to the project directory
3. Run:
```bash
dotnet run
```
4. Open browser to `https://localhost:5001` or `http://localhost:5000`

## Key Features to Implement Later

### Product Traceability
- Scan product code
- View all raw materials used
- See production room and employees
- Track distribution to customers

### Inventory Management
- Track expiration dates
- Alert for expiring materials
- Monitor stock levels
- FIFO/FEFO management

### Employee Management
- Attendance tracking
- Automated payroll calculation
- Production line assignments
- Performance tracking

### Production Tracking
- Daily production logs
- Material consumption tracking
- Quality control records
- Efficiency monitoring

## Notes
- All models use Data Annotations for validation
- Navigation properties are set up for Entity Framework relationships
- The UI is responsive and mobile-friendly
- Bootstrap Icons provide professional icons throughout
- TempData is used for success/error messages

## Support
For issues or questions, refer to the ER Diagram document at:
`E:\DataBase\Project\ERD\Factory_ER_Diagram_Report.pdf`

---
**Created:** 2025
**Framework:** ASP.NET Core 8.0 with Razor Pages
**Database:** SQL Server (Entity Framework Core)
