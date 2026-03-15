# GIAI ĐOẠN 1: KHỞI TẠO VÀ CẤU HÌNH DATABASE TỰ ĐỘNG

*(Bạn đang ở Giai đoạn 1. [⬅️ Quay lại Mục Lục chính](./HuongDan_Index.md))*

---

## 1. Khởi tạo Solution và 3 Projects
1. Trền Visual Studio, tạo **Blank Solution** tên `PE_PRN222_FA25_TranThaiThinh.sln`.
2. Tạo 3 Project con bên trong:
   - **Database:** `RealEstateManagement_TranThaiThinh.Repositories` (Class Library)
   - **Logic:** `RealEstateManagement_TranThaiThinh.Services` (Class Library)
   - **Giao diện:** `RealEstateManagement_TranThaiThinh.Razor` (ASP.NET Core Web App / Razor Pages)

## 2. Add Project Reference (Liên kết các dự án)
- Ở Project `.Services`: Add reference vào `Repositories`.
- Ở Project `.Razor`: Add reference vào cả `Services` và `Repositories`.

## 3. Cài đặt NuGet Packages
Mở file `.csproj` của Project `.Repositories` (nhấp đúp vào tên Project) và dán đoạn **ItemGroup** chứa các package sau. 
*(Đây là danh sách package đầy đủ để EntityFramework hoạt động và đọc trích xuất được `appsettings.json`):*

```xml
 <ItemGroup>
   <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.5" />
   <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.5" />
   <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.5">
     <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
     <PrivateAssets>all</PrivateAssets>
   </PackageReference>
   <PackageReference Include="Microsoft.Extensions.Configuration" Version="8.0.0" />
   <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="8.0.0" />
 </ItemGroup>
```
Đừng quên ở Project `.Razor` cũng cần cài đặt các thư viện Entity Framework tương tự.

## 4. Scaffold DbContext (Dùng Terminal)
1. Mở Terminal (Developer Command Prompt / PowerShell của VS hoặc VS Code).
2. Di chuyển vào thư mục Repositories:
```powershell
cd RealEstateManagement_TranThaiThinh.Repositories
```
3. Chạy lệnh sinh code tự động từ Database (thay đổi thông tin `Server`, `Database`, `Uid`, `Pwd` cho khớp với SQL Server của bạn):
```powershell
dotnet ef dbcontext scaffold "Server=localhost;Database=FA25RealEstateDB;Uid=sa;Pwd=123456789;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context-dir DbContext --context FA25RealEstateDBContext --force
```
*(Nếu báo lỗi không tìm thấy `dotnet-ef`, bạn hãy chạy lệnh `dotnet tool install --global dotnet-ef` trước).*

## 5. Sửa lỗi "Hardcode" Chuỗi Kết Nối trong DbContext
Lệnh Scaffold thường gắn cứng chuỗi kết nối vào file DbContext. Để ứng dụng chạy đúng và linh hoạt chấm điểm, bạn **phải xóa đoạn Hardcode đó** và viết lại hàm `OnConfiguring`.

Mở file `DbContext/FA25RealEstateDBContext.cs`, ghi đè / bổ sung những nội dung sau:
```csharp
using Microsoft.Extensions.Configuration; // Bắt buộc using bộ này

// ... (Bên trong class DbContext) ...

public static string GetConnectionString(string connectionStringName)
{
    var config = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();

    string connectionString = config.GetConnectionString(connectionStringName);
    return connectionString;
}

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(GetConnectionString("DBContext")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
```

## 5. Lưu Chuỗi kết nối vào appsettings.json
Mở Project `.Razor`, vào file `appsettings.json` và thêm khối `ConnectionStrings`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DBContext": "Server=localhost;Database=FA25RealEstateDB;Uid=sa;Pwd=123456789;TrustServerCertificate=True"
  }
}
```

---
**Xong bước Setup cơ sở thành công!**
👉 **[Tiếp theo: Đi sang Giai đoạn 2 - Xây dựng GenericRepository](./2_Repositories_Tier.md)**
