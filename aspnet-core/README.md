# HƯỚNG DẪN CÀI ĐẶT VÀ KHỞI CHẠY DỰ ÁN (GETTING STARTED)

Dự án phát triển dựa trên nền tảng **ASP.NET Boilerplate (ABP Framework)** kết hợp **ASP.NET Core 9**, sử dụng kiến trúc phân tầng chuẩn Domain-Driven Design (DDD) cùng hệ quản trị cơ sở dữ liệu **Microsoft SQL Server**.

---

## 1. Yêu cầu môi trường (Prerequisites)

Trước khi khởi chạy, vui lòng đảm bảo máy tính của bạn đã cài đặt các công cụ sau:
* **[.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)** (Kiểm tra bằng lệnh: `dotnet --version`)
* **[Microsoft SQL Server](https://www.microsoft.com/sql-server)** (Bản Standard, Developer, Express hoặc LocalDB)
* **[Node.js](https://nodejs.org/) & npm** (Khuyến nghị v18 trở lên - dùng cho việc bundle tài nguyên frontend nếu cần)
* **IDE khuyến nghị**: Visual Studio 2022 (v17.12 trở lên), Visual Studio Code hoặc JetBrains Rider.

---

## 2. Cấu hình Chuỗi kết nối Cơ sở dữ liệu (Database Connection)

> ⚠️ **LƯU Ý QUAN TRỌNG:**
> Người kiểm tra / cài đặt **tự nhập Tên đăng nhập (User Id) và Mật khẩu (Password)** của SQL Server tương ứng với môi trường trên máy cá nhân của mình.

Cấu hình chuỗi kết nối tại 2 file sau:
1. `src/Demo.Web.Mvc/appsettings.json` (Dành cho ứng dụng Web)
2. `src/Demo.Migrator/appsettings.json` (Dành cho công cụ chạy Migration)

### Ví dụ cấu hình chuỗi kết nối:

#### Trường hợp 1: Dùng tài khoản SQL Server Authentication (Username & Password tự điền)
```json
"ConnectionStrings": {
  "Default": "Server=localhost; Database=DemoDb; User Id=<TÊN_ĐĂNG_NHẬP_CỦA_BẠN>; Password=<MẬT_KHẨU_CỦA_BẠN>; TrustServerCertificate=True;"
}
```
*(Nếu bạn dùng bản SQL Server Express, đổi `Server=localhost` thành `Server=.\\SQLEXPRESS`)*

#### Trường hợp 2: Dùng xác thực tài khoản Windows (Windows Authentication)
```json
"ConnectionStrings": {
  "Default": "Server=localhost; Database=DemoDb; Trusted_Connection=True; TrustServerCertificate=True;"
}
```

---

## 3. Khởi tạo Cơ sở dữ liệu và Dữ liệu mẫu (Migration & Data Seeding)

Dự án đã tích hợp sẵn cơ chế **Data Seeder** tự động khởi tạo **30 câu hỏi FAQ mẫu phong phú** (đầy đủ các chủ đề, phân bổ trạng thái Published/Hidden, SortOrder từ 1 đến 30) cùng tài khoản Quản trị viên ban đầu.

Bạn có thể tạo Database theo một trong hai cách dưới đây:

### Cách 1: Chạy công cụ `Demo.Migrator` (Khuyên dùng - Nhanh nhất)
Mở terminal tại thư mục gốc của dự án và chạy các lệnh:
```bash
cd src/Demo.Migrator
dotnet run
```
*Công cụ sẽ tự động tạo cơ sở dữ liệu `DemoDb`, áp dụng toàn bộ các migration và nạp 30 câu hỏi FAQ mẫu cùng dữ liệu mặc định vào database.*

### Cách 2: Sử dụng lệnh Entity Framework Core
Nếu bạn muốn tự chạy migration qua CLI hoặc Package Manager Console:
* **Qua Dotnet CLI:**
  ```bash
  dotnet ef database update --project src/Demo.EntityFrameworkCore --startup-project src/Demo.Web.Mvc
  ```
* **Hoặc qua Visual Studio Package Manager Console:**
  * Chọn Default project là `Demo.EntityFrameworkCore`
  * Chạy lệnh:
    ```powershell
    Update-Database
    ```

> 💡 **Ghi chú về Seed Data:**
> Ngay khi bạn chạy ứng dụng Web ở bước 4, hệ thống ABP sẽ tự động kích hoạt seeder và nạp 30 bản ghi FAQ vào database nếu bảng đang trống.

---

## 4. Khởi chạy Ứng dụng Web (Running Application)

### Cách 1: Chạy bằng Visual Studio
1. Mở giải pháp `Demo.sln` trong Visual Studio.
2. Thiết lập `Demo.Web.Mvc` làm **Startup Project** (Chuột phải vào project `Demo.Web.Mvc` -> chọn *Set as Startup Project*).
3. Nhấn **F5** (hoặc `Ctrl + F5`) để bắt đầu chạy.

### Cách 2: Chạy bằng dòng lệnh (CLI)
Mở terminal tại thư mục gốc và chạy:
```bash
cd src/Demo.Web.Mvc
dotnet run
```

### Địa chỉ truy cập ứng dụng:
* **HTTPS**: `https://localhost:44312/`
* **HTTP**: `http://localhost:44311/`

---

## 5. Thông tin Tài khoản Đăng nhập Mặc định

Sau khi vào màn hình Đăng nhập (Login):
* **Tenancy Name**: Để trống (Đăng nhập với tư cách Host)
* **Tên đăng nhập (Username)**: `admin`
* **Mật khẩu (Password)**: `123qwe`

---

## 6. Hướng dẫn Kiểm tra Chức năng Module FAQ

Sau khi đăng nhập thành công vào hệ thống:
1. Điều hướng đến menu bên trái: chọn **FAQ Management** (hoặc truy cập trực tiếp URL: `/Faq`).
2. **Các tính năng có sẵn để kiểm thử**:
   * **Dữ liệu mẫu**: Đã có sẵn 30 câu hỏi thường gặp phục vụ việc kiểm thử ngay lập tức.
   * **Phân trang (Pagination)**: Dữ liệu chia thành 3 trang (10 bản ghi/trang), có thể chuyển trang, đổi số lượng hiển thị.
   * **Tìm kiếm từ khóa (Search)**: Tìm kiếm tức thời theo nội dung câu hỏi hoặc câu trả lời (ví dụ gõ: *mật khẩu, bảo mật, email, 2FA, backup...*).
   * **Bộ lọc trạng thái (Filter)**: Lọc xem *Tất cả*, *Hiển thị (Published)* hoặc *Ẩn (Hidden)*.
   * **Sắp xếp (Sorting)**: Bấm vào tiêu đề cột để sắp xếp tăng/giảm theo *Thứ tự (Sort Order)*, *Ngày tạo*, *Trạng thái*.
   * **Thiết kế Responsive & Cắt gọn chữ**: Thử thu hẹp độ rộng trình duyệt để kiểm tra khả năng hiển thị co giãn linh hoạt và cơ chế ẩn bớt văn bản quá dài bằng dấu `...` (3 dòng).
   * **Thêm mới / Chỉnh sửa (Modal Popup)**: Hỗ trợ form nhập liệu có validation đầy đủ.
   * **Xử lý lỗi tập trung (`ErrorHandler.js`)**: Kiểm tra khi nhập trùng câu hỏi đã có, hệ thống hiển thị thông báo lỗi nghiệp vụ rõ ràng, phân biệt lỗi 4xx (lỗi nghiệp vụ/validation) và 5xx (lỗi hệ thống).
   * **Xóa dữ liệu (Soft Delete)**: Hỗ trợ hộp thoại xác nhận SweetAlert thân thiện và cơ chế xóa mềm an toàn.

---

## 7. Cấu trúc Source Code Chính của Module FAQ

* **Domain Layer (`Demo.Core`)**:
  * [FrequentlyAskedQuestion.cs](file:///d:/IT/dotnet_workspace/demo/aspnet-core/src/Demo.Core/Faqs/FrequentlyAskedQuestion.cs): Entity chính kế thừa `FullAuditedEntity`.
  * [FaqStatus.cs](file:///d:/IT/dotnet_workspace/demo/aspnet-core/src/Demo.Core/Faqs/FaqStatus.cs): Enum trạng thái (Published, Hidden).
  * [FrequentlyAskedQuestionConsts.cs](file:///d:/IT/dotnet_workspace/demo/aspnet-core/src/Demo.Core/Faqs/FrequentlyAskedQuestionConsts.cs): Ràng buộc độ dài ký tự tối đa.
* **Infrastructure Layer (`Demo.EntityFrameworkCore`)**:
  * [DemoDbContext.cs](file:///d:/IT/dotnet_workspace/demo/aspnet-core/src/Demo.EntityFrameworkCore/EntityFrameworkCore/DemoDbContext.cs): Cấu hình DbSet, Unique Index lọc `[IsDeleted] = 0`.
  * [DefaultFaqCreator.cs](file:///d:/IT/dotnet_workspace/demo/aspnet-core/src/Demo.EntityFrameworkCore/EntityFrameworkCore/Seed/Host/DefaultFaqCreator.cs): Bộ nạp 30 bản ghi FAQ mẫu.
* **Application Layer (`Demo.Application`)**:
  * [IFaqAppService.cs](file:///d:/IT/dotnet_workspace/demo/aspnet-core/src/Demo.Application/Faqs/IFaqAppService.cs) & [FaqAppService.cs](file:///d:/IT/dotnet_workspace/demo/aspnet-core/src/Demo.Application/Faqs/FaqAppService.cs): Cung cấp các API nghiệp vụ CRUD, kiểm tra trùng lặp và phân quyền.
  * Các DTOs: `FaqDto`, `CreateFaqDto`, `UpdateFaqDto`, `GetFaqInput`.
* **Presentation Layer (`Demo.Web.Mvc`)**:
  * [FaqController.cs](file:///d:/IT/dotnet_workspace/demo/aspnet-core/src/Demo.Web.Mvc/Controllers/FaqController.cs): Điều hướng giao diện và partial view modal.
  * [Index.cshtml](file:///d:/IT/dotnet_workspace/demo/aspnet-core/src/Demo.Web.Mvc/Views/Faq/Index.cshtml): Giao diện danh sách FAQ, bộ lọc, bảng dữ liệu.
  * [faq.js](file:///d:/IT/dotnet_workspace/demo/aspnet-core/src/Demo.Web.Mvc/wwwroot/view-resources/Views/Faq/Index.js): Xử lý DataTables, AJAX CRUD.
  * [faq.css](file:///d:/IT/dotnet_workspace/demo/aspnet-core/src/Demo.Web.Mvc/wwwroot/view-resources/Views/Faq/Index.css): Tùy biến giao diện responsive và cắt dòng văn bản.
  * [ErrorHandler.js](file:///d:/IT/dotnet_workspace/demo/aspnet-core/src/Demo.Web.Mvc/wwwroot/js/ErrorHandler.js): Module chuẩn hóa thông báo lỗi 4xx/5xx cho toàn bộ ứng dụng.
