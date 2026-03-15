# GIAI ĐOẠN 2: XÂY DỰNG TẦNG REPOSITORIES

*(Bạn đang ở Giai đoạn 2. [⬅️ Quay lại Mục Lục chính](./HuongDan_Index.md) | [⬅️ Về Giai đoạn 1](./1_Setup_And_Database.md))*

Tại đây, chúng ta ở trong Project **`RealEstateManagement_TranThaiThinh.Repositories`**.

## 1. Mẫu Code Base GenericRepository
Việc lặp lại Code CRUD cho từng bảng (Contract, Broker, User) rất tốn công. Ta sẽ dùng Generic `T` đại diện cho mọi Class Models.

Tạo thư mục `Basic` và tạo class `GenericRepository.cs` và copy y chang cục code dưới đây vào. (*Lưu ý: Thay `FA25RealEstateDBContext` bằng tên DbContext thực tế của bạn*).

```csharp
using Microsoft.EntityFrameworkCore;
// Using thư mục chứa DbContext của bạn vào đây

namespace RealEstateManagement__TranThaiThinh.Repositories.Basic
{
    public class GenericRepository<T> where T : class
    {
        protected FA25RealEstateDBContext _context;

        public GenericRepository()
        {
            _context ??= new FA25RealEstateDBContext();
        }

        public GenericRepository(FA25RealEstateDBContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public async Task<int> CreateAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public bool Remove(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T GetById(string code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(string code)
        {
            return await _context.Set<T>().FindAsync(code);
        }


        public T GetById(Guid code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(Guid code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        #region Separating asigned entity and save operators        

        public void PrepareCreate(T entity)
        {
            _context.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _context.Remove(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion Separating asign entity and save operators
    }
}
```
*(Lời khuyên ở Logic Service chỉ nên gọi các hàm kết thúc bằng chữ **Async**)*

## 2. Kế thừa GenericRepository cho các Bảng Cụ thể
Với những hàm cơ bản, ta không cần viết lại. Ta chỉ viết Repository riêng để nối bảng (Include) hoặc tìm kiếm (Search).

Tạo `ContractRepository.cs`:
```csharp
using Microsoft.EntityFrameworkCore;
using RealEstateManagement__TranThaiThinh.Repositories.Basic;
using RealEstateManagement__TranThaiThinh.Repositories.Models;

namespace RealEstateManagement__TranThaiThinh.Repositories
{
    // Kế thừa GenericRepository với kiểu là Contract
    public class ContractRepository : GenericRepository<Contract>
    {
        public ContractRepository() { }
        public ContractRepository(FA25RealEstateDBContext context) { _context = context; }

        // Viết đè (hoặc viết mới) các hàm có tính chất đặc thù: ví dụ Join bảng Broker
        public async Task<List<Contract>> GetAllWithBrokerAsync()
        {
            return await _context.Contracts.Include(p => p.Broker).ToListAsync();
        }

        public async Task<List<Contract>> SearchAsync(string? title, string? type)
        {
            return await _context.Contracts
                .Include(c => c.Broker)
                .Where(c => 
                    (string.IsNullOrEmpty(title) || c.ContractTitle.Contains(title)) &&
                    (string.IsNullOrEmpty(type) || c.PropertyType.Contains(type))
                )
                .ToListAsync();
        }
    }
}
```

Tương tự, tạo `SystemUserRepository.cs` để check Login:
```csharp
public class SystemUserRepository : GenericRepository<SystemUser>
{
    public async Task<SystemUser> GetAccount(string userName, string password)
    {
        return await _context.SystemUsers.FirstOrDefaultAsync(u => u.Username == userName && u.UserPassword == password);
    }
}
```

---
**Kho dữ liệu của bạn đã sẵn sàng được triệu hồi!**
👉 **[Tiếp theo: Đi qua Giai đoạn 3 - Xây Dụng Dịch Vụ và Cấu hình Giao diện Web](./3_Services_And_Razor_UI.md)**
