11	"Cấu trúc Solution 2 Project
Cấu hình tệp Program.cs"	"Phân chia tách biệt đúng 2 project độc lập trong Solution: API (Backend) và WinForms (Client).
Đăng ký dịch vụ gọn gàng, kích hoạt Swagger, bật CORS cho WinForms gọi vào thành công."
12	Chuỗi kết nối appsettings.json	Chuỗi kết nối cơ sở dữ liệu lưu tập trung trong file json, không viết cứng trong code C#.
13	"Thiết kế Controller chuyên biệt
Định tuyến đường dẫn API ([Route])"	"Tách thành các file riêng: AuthController, CategoriesController, ProductsController.
Đặt đường dẫn chuẩn RESTful: api/categories, api/products (danh từ số nhiều)."
14	"Sử dụng đúng phương thức HTTP
Trả về mã trạng thái HTTP chuẩn"	"Dùng đúng chuẩn: GET (lấy tin), POST (thêm mới), PUT (cập nhật), DELETE (xóa).
Trả đúng mã nghiệp vụ: 200 (Thành công), 201 (Tạo mới), 400 (Sai dữ liệu), 404 (Không thấy)."
15	Sử dụng DTO truyền dữ liệu	Dùng các class DTO riêng để nhận/trả dữ liệu qua mạng, không trả nguyên Entity CSDL.
16	Lập trình bất đồng bộ (async / await)	Toàn bộ các hàm trong Controller đều áp dụng async Task<IActionResult> và await.
17	Khả năng kiểm thử trên Swagger UI	Mở Swagger UI lên bấm "Execute" chạy thử nghiệm các API mượt mà, không phát sinh lỗi.
18	Tạo các class Model (Entity)	Viết đủ 6 class: User, Category, Product, Customer, Order, OrderDetail đúng kiểu dữ liệu.
19	Cấu hình DbContext & Khóa ngoại	Khai báo đủ các DbSet, cấu hình đúng quan hệ 1-N giữa Category - Product, Order - Detail.
20	Ràng buộc kiểm tra dữ liệu Entity	Có đặt thuộc tính kiểm tra: Tên bắt buộc nhập ([Required]), giá bán > 0, độ dài chuỗi.
21	Tạo CSDL bằng EF Core Migrations	Tự động tạo và cập nhật CSDL bằng lệnh Add-Migration và Update-Database.
22	Nạp sẵn dữ liệu mẫu (Seed Data)	Có sẵn dữ liệu mồi trong CSDL (tối thiểu 15 sản phẩm, 5 nhóm hàng) khi tạo xong.
23	Thao tác Thêm, Sửa, Xóa (CRUD)	Thực hiện đầy đủ và chạy đúng: Thêm mới, Cập nhật, Xóa nhóm hàng/sản phẩm/.... qua EF Core.
24	Lọc và Tìm kiếm bằng LINQ	Viết câu lệnh LINQ tìm kiếm tên sản phẩm gần đúng (.Contains()), lọc theo nhóm hàng.
25	Phân trang dữ liệu máy chủ	API lấy danh sách mặt hàng có chia trang bằng cặp lệnh LINQ .Skip() và .Take().
26	Nạp bảng liên quan (.Include)	Dùng .Include(p => p.Category) để hiển thị kèm tên nhóm hàng của sản phẩm, không bị null.
27	Tối ưu truy vấn với AsNoTracking()	Sử dụng .AsNoTracking() trong các câu truy vấn GET chỉ đọc để giải phóng bộ nhớ RAM.
28	Đăng nhập cấp mã Token JWT	Nhập đúng tài khoản/mật khẩu, API trả về chuỗi mã JWT Token hợp lệ.
29	Chặn API khi chưa đăng nhập	Gắn thuộc tính [Authorize] lên API, chưa đăng nhập mà gọi thì trả về lỗi 401 Unauthorized.
30	Phân quyền theo vai trò (Role)	Dùng [Authorize(Roles = "...")] phân đúng: Admin toàn quyền, Thu ngân chỉ bán hàng.
31	Gắn Token khi kiểm thử Swagger	Swagger có nút ổ khóa "Authorize", dán chuỗi Token vào thì gọi được các API bảo mật.
32	Tra cứu Barcode bằng ADO.NET	Sử dụng SqlConnection, SqlCommand để tìm nhanh thông tin sản phẩm qua mã vạch.
33	"Đọc dữ liệu bằng SqlDataReader
Chống tấn công SQL Injection"	"Dùng SqlDataReader duyệt dữ liệu một chiều siêu tốc và bọc trong khối using an toàn.
Sử dụng cmd.Parameters.AddWithValue cho mọi tham số truyền vào, tuyệt đối không cộng chuỗi."
34	Chạy Stored Procedure báo cáo	Tạo thủ tục trên SQL Server và gọi từ C# bằng CommandType.StoredProcedure để tính doanh thu ca.
35	"Thanh toán bọc trong Transaction
Tự động hoàn tác (Rollback)"	"Khi thanh toán: Lưu hóa đơn, trừ tồn kho, tích điểm nằm trọn trong 1 SqlTransaction.
Khi kho hết hàng: Tự gọi transaction.Rollback(), không lưu hóa đơn rác vào CSDL."
36	Giao diện Shell phân vùng cố định	Bố cục chuẩn mực: Menu bên trái, Tiêu đề bên trên, Vùng làm việc ở giữa (không bật form con ra ngoài).
37	Đổi màn hình không bị giật lag	Bấm menu thì form con nạp mượt mà vào panel trung tâm (TopLevel = false, Dock = Fill).
38	Màn hình Đăng nhập lưu Token	Nhập tài khoản, gọi API đăng nhập, lưu lại Token vào biến tĩnh để các form sau sử dụng.
39	Ẩn/Hiện nút bấm theo quyền	Thu ngân đăng nhập chỉ thấy nút Bán hàng POS; Quản lý đăng nhập thấy đầy đủ các nút quản trị.
40	Gọi API tập trung qua HttpClient	Xây dựng class dùng chung ApiClientService, tự động đính kèm Bearer Token vào Request Header.
41	Bảng danh mục sản phẩm DataGridView	Lưới DataGridView tải đủ danh sách từ Web API, các nút Thêm/Sửa/Xóa hoạt động trơn tru.
42	"Quầy bán hàng tính tiền (POS)
Thông báo lỗi thân thiện"	"Quét mã ra tên hàng, tự cộng dồn số lượng, tự tính tổng tiền và tính số tiền thối lại cho khách.
Mất mạng hoặc nhập sai thì hiện hộp thoại tiếng Việt rõ ràng, phần mềm không bị văng (crash)."
43	Huy hiệu & Cảnh báo Hàng sắp hết kho (Low-Stock Alert)	Nghiệp vụ: Trên lưới DataGridView hoặc trên thanh trạng thái, các sản phẩm có số lượng tồn <= 10 sẽ được tự động tô màu nền đỏ/cam.Kỹ thuật: Bắt sự kiện CellFormatting của DataGridView trong WinForms: nếu cột StockQuantity <= 10 thì đổi e.CellStyle.BackColor = Color.LightCoral.
44	Dashboard Thẻ chỉ số tổng quan (KPI Summary Cards)	Nghiệp vụ: Hiển thị 4 thẻ số liệu to, nổi bật trên đỉnh màn hình:Doanh thu hôm nay (VNĐ)Tổng số hóa đơn đã xuấtSố lượng khách hàng mới đăng kýSố mặt hàng sắp hết hàng (tồn kho <= 10)Kỹ thuật: Viết 1 Action API GET api/reports/dashboard-summary dùng ADO.NET ExecuteScalar hoặc LINQ Count(), Sum() để trả về các con số tức thì.
45	Biểu đồ Cột Doanh thu theo ngày / tuần / tháng (Bar Chart)	Nghiệp vụ: Quản lý chọn khoảng thời gian (Từ ngày --> Đến ngày), hệ thống hiển thị biểu đồ so sánh doanh thu từng ngày.   Kỹ thuật:Phía API: Viết Stored Procedure gom nhóm GROUP BY CAST(CreatedDate AS DATE).Phía WinForms: Dùng điều khiển kéo thả có sẵn System.Windows.Forms.DataVisualization.Charting.Chart (Series ChartType = Column) để vẽ cột tự động từ dữ liệu API.
46	Thống kê Top 5 Sản phẩm Bán chạy nhất (Best-Sellers)	Nghiệp vụ: Liệt kê 5 mặt hàng tiêu thụ nhanh nhất tại quầy POS để thủ kho chủ động nhập hàng.Kỹ thuật: LINQ GroupBy(od => od.ProductId)--> OrderByDescending(Sum Quantity) --> Take(5).
47	Đổi Điểm Thưởng Trừ Tiền Hóa Đơn (Loyalty Points Redemption)	"Nghiệp vụ: Khách hàng thân thiết có điểm tích lũy (RewardPoints), thu ngân có thể bấm chọn ""Dùng 50 điểm trừ 50.000 đ vào hóa đơn"".
Kỹ thuật: Cập nhật đồng thời trừ điểm trong bảng Customers và trừ tiền thanh toán FinalAmount trong cùng một SqlTransaction."
48	Xuất Báo cáo Danh mục & Hóa đơn ra Excel/CSV	"Nghiệp vụ: Quản lý có thể bấm nút ""Xuất Excel"" để tải danh sách toàn bộ mặt hàng kèm số lượng tồn kho hoặc danh sách doanh thu ra tệp .xlsx / .csv.
Kỹ thuật: Sử dụng thư viện ClosedXML hoặc EPPlus phía API hoặc xuất chuỗi CSV trực tiếp phía WinForms."