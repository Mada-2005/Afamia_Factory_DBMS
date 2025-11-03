# Database Connection Guide

## Step-by-Step Instructions for Connecting Your Database

### Option 1: Use Entity Framework to Create the Database (Recommended)

This approach will create the database automatically based on your models.

#### 1. Update the Connection String
Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=FactoryDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

Common server names:
- Local SQL Server: `localhost` or `(localdb)\\mssqllocaldb`
- SQL Server Express: `.\\SQLEXPRESS`
- Network server: `SERVER_IP,PORT` or `SERVER_NAME\\INSTANCE`

#### 2. Enable DbContext in Program.cs
Uncomment these lines in `Program.cs`:
```csharp
builder.Services.AddDbContext<FactoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

#### 3. Create Database Migration
Open terminal in project directory and run:
```bash
# Add Entity Framework tools if not installed
dotnet tool install --global dotnet-ef

# Create migration
dotnet ef migrations add InitialCreate

# Apply migration to create database
dotnet ef database update
```

This will create all tables based on your models!

### Option 2: Connect to Existing Database

If you're creating the database manually in SQL Server Management Studio:

#### 1. Create Your Database in SSMS
Run your SQL scripts to create tables based on the ER diagram.

#### 2. Update Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE_NAME;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

#### 3. Update DbContext
You may need to modify `FactoryDbContext.cs` to match your exact table/column names if they differ.

#### 4. Use Scaffold to Generate Models (Optional)
If you want to regenerate models from existing database:
```bash
dotnet ef dbcontext scaffold "YOUR_CONNECTION_STRING" Microsoft.EntityFrameworkCore.SqlServer -o Models -f
```

## Updating Page Models to Use Database

Once database is connected, update each page's `.cshtml.cs` file:

### Example: Vendors/Index.cshtml.cs

**Before (current):**
```csharp
public class IndexModel : PageModel
{
    public IList<Vendor> Vendors { get; set; } = new List<Vendor>();

    public void OnGet()
    {
        // TODO: Load vendors from database
    }
}
```

**After (with database):**
```csharp
using Microsoft.EntityFrameworkCore;
using FactorySystemWebpage.Data;

public class IndexModel : PageModel
{
    private readonly FactoryDbContext _context;

    public IndexModel(FactoryDbContext context)
    {
        _context = context;
    }

    public IList<Vendor> Vendors { get; set; } = new List<Vendor>();

    public async Task OnGetAsync()
    {
        Vendors = await _context.Vendors.ToListAsync();
    }
}
```

### Example: Vendors/Create.cshtml.cs

**Before (current):**
```csharp
public IActionResult OnPost()
{
    if (!ModelState.IsValid)
    {
        return Page();
    }

    // TODO: Save to database when ready
    TempData["SuccessMessage"] = "Vendor created successfully! (Database not connected yet)";
    return RedirectToPage("./Index");
}
```

**After (with database):**
```csharp
public async Task<IActionResult> OnPostAsync()
{
    if (!ModelState.IsValid)
    {
        return Page();
    }

    _context.Vendors.Add(Vendor);
    await _context.SaveChangesAsync();

    TempData["SuccessMessage"] = "Vendor created successfully!";
    return RedirectToPage("./Index");
}
```

## Testing the Connection

### 1. Simple Test
Add this to any page model:
```csharp
public async Task OnGetAsync()
{
    var canConnect = await _context.Database.CanConnectAsync();
    ViewData["DbStatus"] = canConnect ? "Connected!" : "Not connected";
}
```

### 2. View in Dashboard
Update `Pages/Dashboard/Index.cshtml.cs`:
```csharp
private readonly FactoryDbContext _context;

public IndexModel(FactoryDbContext context)
{
    _context = context;
}

public int RawMaterialCount { get; set; }
public int ProductCount { get; set; }
public int EmployeeCount { get; set; }
public int ProductionRoomCount { get; set; }

public async Task OnGetAsync()
{
    RawMaterialCount = await _context.RawMaterials.CountAsync();
    ProductCount = await _context.Products.CountAsync();
    EmployeeCount = await _context.Employees.Where(e => e.IsActive).CountAsync();
    ProductionRoomCount = await _context.ProductionRooms.Where(r => r.IsActive).CountAsync();
}
```

Then update the Dashboard page to display these counts instead of "--".

## Common Connection Issues

### Error: "A network-related or instance-specific error"
**Solution:**
- Check SQL Server is running
- Verify server name is correct
- Enable TCP/IP in SQL Server Configuration Manager
- Check firewall settings

### Error: "Login failed for user"
**Solution:**
- If using Windows Authentication: Use `Trusted_Connection=True`
- If using SQL Authentication: Use `User Id=username;Password=password;`
- Verify user has permissions to the database

### Error: "Cannot open database"
**Solution:**
- Database doesn't exist yet - run `dotnet ef database update`
- Or create database manually in SSMS first

## SQL Server Authentication Options

### Windows Authentication (Recommended for local development)
```json
"DefaultConnection": "Server=localhost;Database=FactoryDB;Trusted_Connection=True;MultipleActiveResultSets=true"
```

### SQL Server Authentication
```json
"DefaultConnection": "Server=localhost;Database=FactoryDB;User Id=sa;Password=YourPassword;MultipleActiveResultSets=true"
```

## Quick Checklist

- [ ] SQL Server is installed and running
- [ ] Connection string is updated in appsettings.json
- [ ] DbContext is uncommented in Program.cs
- [ ] Migration is created (if using EF approach)
- [ ] Database is created/updated
- [ ] Page models are updated to inject DbContext
- [ ] Test the connection by running the app

## Need Help?

1. Check the connection string format
2. Test connection in SQL Server Management Studio first
3. Review Entity Framework Core documentation: https://learn.microsoft.com/ef/core/
4. Check SQL Server error logs

---
Once your database is connected, all the TODO comments in the code can be replaced with actual database operations!
