# TỔNG QUAN TRIỂN KHAI DỰ ÁN ASP.NET CORE 3 LỚP (3 TIER)

Chào bạn, đây là bộ tài liệu hoàn chỉnh để bế 1 bộ Database trống rỗng thành 1 Website với kiến trúc 3 lớp tiêu chuẩn của .NET Core.

Dự án được chia làm 3 Project chính. Dưới đây là kiến trúc cây thư mục chi tiết (Project Tree) tham khảo sau khi bạn đã dựng xong theo chuẩn 3 lớp:

```text
PE_PRN222_FA25_TranThaiThinh.sln
│
├── RealEstateManagement_TranThaiThinh.Repositories/ (Class Library)
│   ├── Basic/
│   │   └── GenericRepository.cs         (Chứa CRUD cơ bản dùng chung - Chuẩn Async)
│   ├── DbContext/
│   │   └── FA25RealEstateDBContext.cs   (Tự động sinh từ DB, đã cấu hình đọc appsettings)
│   ├── Models/
│   │   ├── Broker.cs
│   │   ├── Contract.cs
│   │   └── SystemUser.cs
│   ├── ContractRepository.cs            (Code truy vấn DB cho bảng Contract)
│   └── SystemUserRepository.cs          (Code truy vấn Login, Phân quyền)
│
├── RealEstateManagement_TranThaiThinh.Services/ (Class Library)
│   ├── IContractService.cs              (Interface logic)
│   ├── ContractService.cs               (Xử lý logic, gọi tới ContractRepository)
│   └── ...
│
└── RealEstateManagement_TranThaiThinh.Razor/ (ASP.NET Core Web App)
    ├── Pages/                           (Chứa các file giao diện .cshtml)
    │   ├── Login.cshtml                 (Giao diện đăng nhập)
    │   └── Contracts/
    │       ├── Index.cshtml             (Danh sách hợp đồng)
    │       ├── Create.cshtml            (Form tạo mới)
    │       └── Edit.cshtml
    ├── appsettings.json                 (Chứa chuỗi kết nối Database)
    └── Program.cs                       (Nơi gắn Dependency Injection & Cookie Auth)
```

Để tiện theo dõi và copy-paste code theo từng giai đoạn, toàn bộ tài liệu đã được chia thành **3 Giai Đoạn Độc Lập** liên kết với nhau. 

**VUI LÒNG ĐỌC VÀ LÀM THEO THỨ TỰ CÁC BƯỚC SAU:**

### 🚀 [Giai đoạn 1: Khởi tạo Project & Cấu hình Database (Scaffold DbContext)](./1_Setup_And_Database.md)
Bước này hướng dẫn tạo Solution, cài Nuget, chạy lệnh Terminal để tự động sinh DbContext và Models từ Database có sẵn, đồng thời setup chuỗi kết nối vào `appsettings.json` của tầng Web.

### 🏗️ [Giai đoạn 2: Xây dựng tầng Repositories & Base GenericRepository](./2_Repositories_Tier.md)
Bước này hướng dẫn tạo một file `GenericRepository.cs` siêu bá đạo để dùng chung hàm CRUD cho mọi bảng, và cách viết các Repository con (ví dụ `ContractRepository`) kế thừa nó như thế nào.

### ⚙️ [Giai đoạn 3: Viết Services Logic & Cấu hình UI (DI, Cookie Auth)](./3_Services_And_Razor_UI.md)
Bước này hướng dẫn tạo interface `IService`, tiêm dữ liệu (Dependency Injection) trong `Program.cs`, cấu hình bộ lọc Đăng nhập (Auth) và ví dụ Code Behind (C#) của trang hiển thị danh sách / tạo mới.

---
*(Hãy click vào Link của Giai đoạn 1 để bắt đầu!)*
