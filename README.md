# 🛍️ Website Thương Mại Điện Tử Bán Đồ Handmade

## 📌 Giới thiệu
Đây là dự án **đồ án tốt nghiệp** với mục tiêu xây dựng một hệ thống website thương mại điện tử chuyên kinh doanh các sản phẩm **handmade**.  
Website hỗ trợ cửa hàng Thái Sơn mở rộng kênh bán hàng, tiếp cận khách hàng trực tuyến hiệu quả hơn và hiện đại hóa quy trình kinh doanh.

- **Công nghệ Backend:** ASP.NET Core  
- **Công nghệ Frontend:** Razor Pages / MVC (ASP.NET Core)  
- **Cơ sở dữ liệu:** SQL Server  
- **ORM:** Entity Framework Core  
- **Quản lý gói:** NuGet  
- **Công cụ hỗ trợ:** Bootstrap 5, jQuery, AJAX  

---

## ⚙️ Chức năng chính

### 👨‍💻 Người dùng (Khách hàng)
- Đăng ký, đăng nhập, quản lý tài khoản cá nhân  
- Xem danh mục sản phẩm handmade  
- Tìm kiếm và lọc sản phẩm theo nhiều tiêu chí  
- Thêm sản phẩm vào giỏ hàng, cập nhật số lượng, xóa sản phẩm  
- Thanh toán và quản lý đơn hàng  

### 🛒 Quản trị viên (Admin)
- Quản lý sản phẩm  
- Quản lý danh mục sản phẩm  
- Quản lý đơn hàng  
- Quản lý khách hàng và phân quyền tài khoản  
- Quản lý mã giảm giá 
- Thống kê, báo cáo doanh thu  

---

## 🚀 Cách chạy dự án

### 1. Yêu cầu môi trường
- Visual Studio 2022 hoặc Rider  
- .NET 8.0
- SQL Server 2019 trở lên  
- Git  

### 2. Clone dự án
```bash
git clone https://github.com/your-username/PhamVanDai_Handmade.git
cd PhamVanDai_Handmade

3. Cấu hình CSDL
Mở file appsettings.json
Sửa lại chuỗi kết nối:
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=HandmadeDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}

4. Apply migration
dotnet ef database update

5. Chạy dự án
dotnet run

🎯 Mục tiêu đề tài
Xây dựng một website thương mại điện tử hiện đại, tối ưu trải nghiệm khách hàng
Hỗ trợ cửa hàng kinh doanh sản phẩm handmade hiệu quả, minh bạch và tiện lợi
Ứng dụng công nghệ ASP.NET Core và Entity Framework Core vào thực tế

📌 Tác giả
👤 Phạm Văn Đại
📧 Email: phamdaimp40@gmail.com
📅 Đồ án tốt nghiệp 2025
