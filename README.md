# 📦 Inventory Management System (StarterKITNET)

## 👨‍🎓 Giới thiệu

Đây là project em thực hiện trong quá trình thực tập nhằm xây dựng một hệ thống **quản lý nhập – xuất kho cơ bản** sử dụng **ASP.NET Core Web API** và **Entity Framework Core**.

Hệ thống cho phép:

* Quản lý sản phẩm
* Nhập kho (Import)
* Xuất kho (Export)
* Theo dõi lịch sử giao dịch kho
* Tìm kiếm sản phẩm
* Kiểm soát tồn kho

---

## 🛠 Công nghệ sử dụng

* ASP.NET Core 9
* Entity Framework Core 9
* PostgreSQL
* RESTful API
* Clean structure (Controller – DbContext – Entity)

---

## 🏗 Kiến trúc hệ thống

Hệ thống gồm 2 bảng chính:

### 1️⃣ Products

Lưu thông tin sản phẩm

| Trường | Kiểu   | Mô tả                 |
| ------ | ------ | --------------------- |
| Id     | Guid   | Khóa chính            |
| Code   | string | Mã sản phẩm (unique)  |
| Name   | string | Tên sản phẩm          |
| Stock  | int    | Số lượng tồn hiện tại |

---

### 2️⃣ InventoryTransactions

Lưu lịch sử nhập – xuất

| Trường    | Kiểu     | Mô tả                  |
| --------- | -------- | ---------------------- |
| Id        | Guid     | Khóa chính             |
| Time      | DateTime | Thời gian giao dịch    |
| Type      | smallint | 1 = Import, 2 = Export |
| ProductID | Guid     | FK tới Products        |
| Quantity  | int      | Số lượng               |
| Note      | string   | Ghi chú                |

---

## 🔄 Nguyên tắc hoạt động

* Không chỉnh sửa trực tiếp `Stock`
* Mọi thay đổi số lượng đều phải thông qua:

  * API nhập kho
  * API xuất kho
* Mỗi lần nhập/xuất sẽ:

  1. Cập nhật tồn kho
  2. Ghi 1 bản ghi vào InventoryTransactions

Công thức tồn kho:

```
Stock = Tổng Import - Tổng Export
```

---

## 🚀 Cài đặt & Chạy project

### 1️⃣ Clone project

```
git clone <repository-url>
```

### 2️⃣ Cấu hình connection string

Trong `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=InventoryDb;Username=postgres;Password=123456"
}
```

### 3️⃣ Chạy migration

```
dotnet ef database update
```

### 4️⃣ Run project

```
dotnet run
```

API sẽ chạy tại:

```
https://localhost:5001
```

---

## 📌 Các API chính

### 🔍 Lấy danh sách sản phẩm

```
GET /api/products
```

### ➕ Tạo sản phẩm

```
POST /api/products
```

### 📥 Nhập kho

```
POST /api/products/{id}/import?quantity=10&note=Nhập hàng
```

### 📤 Xuất kho

```
POST /api/products/{id}/export?quantity=5&note=Bán hàng
```

### 📜 Xem lịch sử giao dịch

```
GET /api/products/{id}/transactions
```

---

## 🎯 Mục tiêu học tập đạt được

Trong quá trình thực hiện project, em đã:

* Hiểu cách hoạt động của EF Core Migration
* Nắm được cơ chế DbContext và Dependency Injection
* Thực hành RESTful API
* Thiết kế quan hệ 1-N giữa Product và Transaction
* Xử lý nghiệp vụ kiểm tra tồn kho trước khi xuất
* Làm việc với PostgreSQL
* Hiểu cơ chế design-time khi chạy `dotnet ef`

---
## 🔮 Hướng phát triển tương lai

* Áp dụng Service Layer
* Sử dụng DTO thay vì Entity trực tiếp
* Thêm xác thực (Authentication / Authorization)
* Thêm phân quyền người dùng
* Áp dụng Clean Architecture
* Triển khai Docker
---
## 👨‍💻 Thông tin thực tập sinh
* Họ tên: [Vũ Văn Hà Công]
* Vị trí: Intern Backend Developer
* Thời gian thực tập: 2025-2026
* Công nghệ chính: .NET
---
