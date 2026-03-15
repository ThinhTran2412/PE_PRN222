# GIAI ĐOẠN 3: XÂY DỰNG SERVICES VÀ CẤU HÌNH GIAO DIỆN (RAZOR PAGES)

*(Bạn đang ở Giai đoạn cuối. [⬅️ Quay lại Mục Lục chính](./Readme.md) | [⬅️ Về Giai đoạn 2](./2_Repositories_Tier.md))*

Hai Project được dùng trong bước này là `RealEstateManagement_TranThaiThinh.Services` và `RealEstateManagement_TranThaiThinh.Razor`.

---

## 1. Viết Logic Nghiệp Vụ ở Tầng Services
Tầng này gọi các hàm từ tầng `Repositories`, bọc trong `try-catch` để bắt ngoại lệ trước khi đẩy lên UI.

Tạo Interface `IContractService`:
```csharp
using RealEstateManagement__TranThaiThinh.Repositories.Models;

namespace RealEstateManagement__TranThaiThinh.Services
{
    public interface IContractService
    {
        Task<List<Contract>> GetAllAsync();
        Task<int> CreateAsync(Contract contract);
        // ... Các thao tác tương tự.
    }
}
```

Tạo Class `ContractService` thực thi Interface trên:
```csharp
using RealEstateManagement__TranThaiThinh.Repositories;
using RealEstateManagement__TranThaiThinh.Repositories.Models;

namespace RealEstateManagement__TranThaiThinh.Services
{
    public class ContractService : IContractService
    {
        private readonly ContractRepository _repository;
        
        // Khởi tạo cứng Repo ở Logic
        public ContractService() => _repository = new ContractRepository();

        public async Task<List<Contract>> GetAllAsync()
        {
            try {
                // Gọi hàm GetAllWithBrokerAsync từ ContractRepository thay vì Generic 
                // để lấy cả Tên của Broker
                return await _repository.GetAllWithBrokerAsync();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> CreateAsync(Contract contract)
        {
            try {
                return await _repository.CreateAsync(contract);
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
```

---

## 2. Cấu hình Program.cs để kết nối Mọi Thứ (Tầng Razor)
Mở file `Program.cs` ở Project Razor và tiến hành **3 việc then chốt**: Đăng ký UI, Đăng ký DI (Dependency Injection), và Đăng ký Phân quyền Cookie.

```csharp
using Microsoft.AspNetCore.Authentication.Cookies;
using RealEstateManagement__TranThaiThinh.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Kích hoạt Razor Pages
builder.Services.AddRazorPages();

// 2. ĐĂNG KÝ DI CHÍNH THỨC - Khi UI gọi IContractService, hệ thống tự cấp class ContractService
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<SystemUserService>(); // Nếu class ko có Interface thì đăng ký thẳng

// 3. Phân quyền Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; 
        options.AccessDeniedPath = "/Forbidden";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    });

var app = builder.Build();

/* ... (Bỏ qua phần dev Env và UseStaticFiles ...) */

app.UseRouting();

// 4. BẬT AUTH (ĐÚNG THỨ TỰ: Authenticate kiểm tra ai -> Authorize kiểm tra quyền)
app.UseAuthentication();
app.UseAuthorization();

// 5. Build Map Endpoint
app.MapRazorPages().RequireAuthorization(); // Mặc định khoá hết các trang, bắt log in

app.Run();
```

---

## 3. Cách gọi Code tại Giao Diện (Razor Pages Code-Behind)
Ví dụ tại file `Pages/Contracts/Index.cshtml.cs` (Trang xem danh sách Hợp đồng):

```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstateManagement__TranThaiThinh.Services;
using RealEstateManagement__TranThaiThinh.Repositories.Models;

namespace RealEstateManagement__TranThaiThinh.Razor.Pages.Contracts
{
    public class IndexModel : PageModel
    {
        private readonly IContractService _contractService;

        // DI tự động Tiêm Service vào Constructor của bạn (Nhờ lệnh AddScoped ở Program.cs)
        public IndexModel(IContractService contractService)
        {
            _contractService = contractService;
        }

        // Biến để HTML lặp qua hiển thị
        public IList<Contract> Contract { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Không bao giờ giao tiếp Repository ở đây, chỉ gọi Service.
            Contract = await _contractService.GetAllAsync();
        }
    }
}
```

---
**CHÚC MỪNG BẠN ĐÃ LÀM CHỦ KIẾN TRÚC 3 LỚP ASP.NET CORE RAZOR PAGES!** 🎉
*(Bạn có thể [⬅️ Quay lại Mục Lục chính](./HuongDan_Index.md))*
