# Báo Cáo Task: Nghiên cứu & Triển khai Clean Architecture với .NET 8

**Mục tiêu:** Báo cáo kết quả nghiên cứu các từ khóa công nghệ thực hành áp dụng Clean Architecture, CQRS, Mediator cho dự án .NET 8 Web API.

---

## Phần 1: Tổng hợp kết quả nghiên cứu

Trong quá trình thực hiện task, em đã tìm hiểu và áp dụng các keyword vào dự án:

1. **.NET Core 8:** Phiên bản LTS của framework .NET.
2. **LINQ (Language Integrated Query):** Sử dụng để truy vấn (Select, Include, ToListAsync), lọc (Where, FirstOrDefaultAsync), Sắp xếp (OrderBy), Thêm (AddAsync), Xóa(ExecuteDeleteAsync, Soft Delete) .
3. **EF Core (DB First):** Thiết kế database trên SQL Server trước, sau đó sử dụng lệnh `Scaffold-DbContext` để generate các Entity classes và DbContext ngược vào code. Entity nằm ở PersistenceModel
4. **SQL Server:** Gắn index cho các field lấy dữ liệu
5. **Mediator in C# (thông qua thư viện MediatR):** Request từ Controller sẽ được gửi đến Mediator, sau đó Mediator sẽ điều hướng đến Handler tương ứng.
6. **Clean Architecture:** Gồm các layer chính: `Domain`, `Application`, `Infrastructure`, và `Web API`.
7. **CQRS (Command Query Responsibility Segregation):** Phân tách rõ ràng các thao tác làm thay đổi dữ liệu (Command: Create, Update, Delete) và các thao tác chỉ đọc dữ liệu (Query: Read).
8. **RESTful API:** sử dụng HTTP methods: GET, POST, PUT, DELETE.

---

## Phần 2: Triển khai dự án

### 1. Mô hình dữ liệu (Database Schema)
Dự án triển khai mô hình quan hệ **1 - N (Cha - Con)** với 2 bảng:
* **`Menu` (Bảng cha):** Quản lý danh mục tin tức.
* **`News` (Bảng con):** Quản lý bài viết tin tức, mỗi bài viết thuộc về một `Menu` cụ thể.


### 2. Cấu trúc Solution (Clean Architecture)

```text
├── DemoCleanArchitecture.API
│   ├── Common                         # ApiResponse
│   ├── Controllers                    # Controller API
│   ├── Middleware                     # GlobalExceptionHandleMiddleware xử lý các Exception và trả về Apiresponse
│
├── DemoCleanArchitecture.Application
│   ├── Common
│   │   ├── DTOs                       # Dữ liệu hiển thị ở API
│   │   ├── Mappings                   # Sử dụng Automapper để map dữ liệu domain entity sang dto
│   │   └── Validators                 # Sử dụng cấu hình pipeline validation (Không phải bắt ở mỗi Handler)
│   └── Features
│       ├── Menus                      
│       │   ├── Commands               # Write side của menu
│       │   │   ├── CreateMenu        
│       │   │   ├── DeleteMenu
│       │   │   └── UpdateMenu
│       │   └── Queries                # Read side của menu
│       │       ├── GetMenuById
│       │       ├── GetMenuByIdWithNews
│       │       ├── GetMenus
│       │       └── GetMenusWithNews
│       └── News
│           ├── Commands              # Write side của news
│           │   ├── ArchiveNews
│           │   ├── CreateNews
│           │   ├── DeleteNews
│           │   ├── MoveNews
│           │   ├── PublishNews
│           │   ├── UnarchiveNews
│           │   ├── UnpublishNews
│           │   └── UpdateNews
│           └── Queries               # Read side của news
│               ├── GetNews
│               └── GetNewsById
│
├── DemoCleanArchitecture.Domain
│   ├── Entities                      # Domain Entity của Menu, News
│   ├── Enums                         # Enum trạng thái của news
│   ├── Exceptions                    # Custom Exception (Domain Exception, Business Exception,...)
│   └── Interfaces                    # Các interface phục vụ cho Use case
│
└── DemoCleanArchitecture.Infrastructure
    ├── Data
    │   └── PersistenceModels         # Model được sinh ra khi sử dụng lệnh Scaffold DB FIrst của EF Core
    ├── Mappings                      # Mapping model của database sang domain
    └── Repositories                  # Định nghĩa các interface của domain
```

### Em cảm ơn đã review code!
> NO PAIN NO GAIN 💪
