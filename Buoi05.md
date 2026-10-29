BUỔI 5: ADO.NET NÂNG CAO, STORED PROCEDURE & GIAO DỊCH POS CHECKOUT
TIẾT 1: KIẾN TRÚC ADO.NET CĂN BẢN VÀ CÁC THÀNH PHẦN CỐT LÕI
1. Mục tiêu bài học (Tiết 1)
Sau khi hoàn thành Tiết 1, sinh viên có khả năng:
Hiểu bản chất: Trình bày được kiến trúc phân tầng kết nối dữ liệu của ADO.NET trong ứng dụng .NET Core.
Phân biệt thành phần: Giải thích rõ vai trò, nhiệm vụ của 5 đối tượng trung tâm: SqlConnection, SqlCommand, SqlParameter, SqlDataReader, DataTable.
Làm chủ phương thức thực thi: Phân biệt chính xác mục đích sử dụng của 3 hàm: ExecuteScalar(), ExecuteNonQuery(), ExecuteReader().
An toàn dữ liệu: Giải thích được cơ chế tấn công SQL Injection và biết cách dùng SqlParameter để triệt tiêu hoàn toàn lỗ hổng này.
Tự thực hành: Tự tay viết được mã nguồn C# kết nối tới cơ sở dữ liệu SQL Server, thực thi thành công các truy vấn cơ bản thông qua 3 bài tập thực hành mẫu.
2. ADO.NET là gì? Hình tượng hóa đời thường
ADO.NET (ActiveX Data Objects for .NET) là bộ thư viện tầng thấp cung cấp khả năng giao tiếp dữ liệu có cấu trúc giữa mã nguồn C# và hệ quản trị cơ sở dữ liệu quan hệ (ở đây là Microsoft SQL Server).
Để sinh viên dễ hình dung cơ chế hoạt động, hãy xem quy trình này giống như việc đặt hàng từ xa tới Siêu thị Mini:



Plaintext
┌─────────────────────────────────────────────────────────────────────────────┐
│                   MÔ HÌNH SO SÁNH ĐỜI THƯỜNG CỦA ADO.NET                   │
├────────────────────────┬────────────────────────────────────────────────────┤
│ Đối tượng ADO.NET      │ Hình tượng đời thường                               │
├────────────────────────┼────────────────────────────────────────────────────┤
│ SqlConnection          │ Đường dây điện thoại kết nối tới siêu thị            │
│ SqlCommand             │ Tờ đơn đặt hàng / Lời yêu cầu cụ thể                 │
│ SqlParameter           │ Chi tiết món hàng được ghi rõ ràng, an toàn         │
│ SqlDataReader          │ Băng chuyền quét mã tính tiền siêu tốc (Forward)    │
│ DataTable / Adapter    │ Thùng hàng đóng gói gửi về nhà (Ngắt kết nối)       │
└────────────────────────┴────────────────────────────────────────────────────┘


SqlConnection (Đường dây liên lạc): Trước khi muốn mua hàng hay hỏi giá, bạn bắt buộc phải quay số gọi tới siêu thị. Nếu không mở máy (Open()), bạn không thể trao đổi bất kỳ thông tin nào. Khi nói chuyện xong, bạn phải cúp máy (Close()) để người khác còn gọi vào.
SqlCommand (Tờ đơn đặt hàng): Sau khi máy đã thông, bạn cần đọc yêu cầu của mình: "Cho tôi kiểm tra giá hộp sữa" (SELECT), "Tôi muốn lấy thêm 2 gói mì" (INSERT), hoặc "Hủy món đồ này" (DELETE).
SqlParameter (Chi tiết an toàn): Khi đọc tên hàng, bạn đọc chính xác mã vạch cụ thể thay vì nói mập mờ. Điều này giúp nhân viên siêu thị không nhầm lẫn và ngăn kẻ gian chèn lệnh giả mạo phá hoại kho hàng (chống lỗi SQL Injection).
SqlDataReader (Băng chuyền tính tiền siêu tốc): Nhân viên siêu thị đưa từng món đồ lướt qua máy quét mã vạch. Hàng chạy tới đâu, máy quét đọc tới đó theo một chiều tiến về phía trước (Read()). Cách này cực nhẹ, không chiếm diện tích mặt bàn (rất ít tốn RAM máy tính).
DataTable (Thùng hàng đóng hộp mang về nhà): Toàn bộ đồ được nhân viên đóng sẵn vào một thùng các-tông (DataTable) rồi chuyển về tận nhà bạn thông qua nhân viên giao hàng (SqlDataAdapter). Lúc này cuộc gọi đã kết thúc (Close), bạn có thể ngồi mở thùng hàng ra xem đi xem lại mà không phụ thuộc vào siêu thị nữa.

3. Sơ đồ luồng xử lý dữ liệu tổng quát
       ┌──────────────────────────────────────────────┐
       │     Ứng dụng C# (.NET Core Web API / WinForms)│
       └──────────────────────┬───────────────────────┘
                              │
               1. Thiết lập chuỗi kết nối (ConnectionString)
               2. Mở kết nối: connection.Open()
                              ▼
                 ┌──────────────────────────┐
                 │      SqlConnection       │
                 └────────────┬─────────────┘
                              │
               3. Đưa câu lệnh và tham số vào:
                  SqlCommand cmd = new SqlCommand(sql, connection);
                  cmd.Parameters.AddWithValue(...)
                              ▼
                 ┌──────────────────────────┐
                 │       SqlCommand         │
                 └────────────┬─────────────┘
                              │
               4. Chọn phương thức thực thi phù hợp:
                              ├───────────────────────────────────────┐
                              │                                       │
                A. Đọc trực tiếp (Connected)            B. Đổ vào RAM (Disconnected)
                              ▼                                       ▼
                 ┌──────────────────────────┐            ┌──────────────────────────┐
                 │      SqlDataReader       │            │      SqlDataAdapter      │
                 └────────────┬─────────────┘            └────────────┬─────────────┘
                              │ while(reader.Read())                  │ adapter.Fill(...)
                              ▼                                       ▼
                       Đọc từng dòng dữ liệu                     DataTable
                              │                                       │
               5. Đóng kết nối vật lý (Close / Dispose qua using)      │
                              ▼                                       ▼
       ┌─────────────────────────────────────────────────────────────────────┐
       │                 Trả dữ liệu về giao diện hoặc API Client              │
     └──────────────────────────────────────────────────┘


4. Chi tiết 5 thành phần cốt lõi trong ADO.NET
4.1. Kênh kết nối: SqlConnection
Nhiệm vụ: Quản lý phiên kết nối vật lý thực tế từ ứng dụng .NET tới máy chủ CSDL SQL Server.
Chuỗi kết nối (ConnectionString): Chứa các tham số định danh quan trọng:
Server (hoặc Data Source): Địa chỉ máy chủ CSDL (ví dụ: localhost, ., hoặc SERVER\SQLEXPRESS).
Database (hoặc Initial Catalog): Tên CSDL muốn làm việc (ví dụ: MiniSupermarketDb).
Trusted_Connection=True;TrustServerCertificate=True;: Chế độ xác thực Windows (Windows Authentication) và tin cậy chứng chỉ máy chủ.
Quy tắc vàng: Luôn bọc đối tượng SqlConnection trong khối lệnh using (...) { ... } để đảm bảo khi kết thúc công việc hoặc gặp ngoại lệ bất ngờ, kết nối luôn được tự động đóng lại (Close()), không làm nghẽn tài nguyên Connection Pool của máy chủ.
4.2. Đối tượng thực thi lệnh: SqlCommand
Nhiệm vụ: Đóng gói câu lệnh SQL (SELECT, INSERT, UPDATE, DELETE) hoặc tên Stored Procedure gửi xuống SQL Server để thực thi.
Cú pháp khởi tạo chuẩn:
C#
using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
{
    // Thiết lập tham số và gọi hàm thực thi tại đây
}


4.3. Tham số hóa an toàn: SqlParameter
Tại sao không được cộng chuỗi SQL?
Cách viết sai lầm (Cực kỳ nguy hiểm):
C#
// NGUY HIỂM: Bị tấn công phá hoại CSDL qua lỗi SQL Injection
string sql = "SELECT * FROM Users WHERE Username = '" + txtUser.Text + "' AND Password = '" + txtPass.Text + "'";

Nếu người dùng nhập vào ô tài khoản chuỗi: ' OR '1'='1, câu lệnh SQL sẽ bị biến đổi thành điều kiện luôn đúng, cho phép xâm nhập trái phép vào hệ thống mà không cần mật khẩu.
Cách khắc phục chuẩn: Luôn sử dụng ký tự @ đại diện cho tham số trong câu lệnh SQL và gán giá trị qua SqlParameter:
C#
string sql = "SELECT * FROM Users WHERE Username = @User AND Password = @Pass";
cmd.Parameters.AddWithValue("@User", txtUser.Text.Trim());
cmd.Parameters.AddWithValue("@Pass", txtPass.Text.Trim());

Hệ thống sẽ coi giá trị nhập vào đơn thuần là chuỗi dữ liệu (Data), không thể biến thành mã lệnh thực thi, ngăn chặn 100% nguy cơ tấn công SQL Injection.
4.4. Luồng đọc nhanh: SqlDataReader
Đặc tính kỹ thuật:
Connected Mode: Đòi hỏi SqlConnection phải luôn ở trạng thái mở (Open()) trong suốt quá trình đọc.
Forward-Only (Một chiều tiến): Con trỏ bản ghi chỉ dịch chuyển từ dòng đầu tiên xuống dòng cuối cùng qua hàm reader.Read(), không thể nhảy lùi lại dòng trước.
Read-Only (Chỉ đọc): Không cho phép chỉnh sửa dữ liệu trực tiếp trên luồng đọc.
Ưu điểm lớn: Tiêu thụ cực kỳ ít bộ nhớ RAM vì dữ liệu được xử lý tuần tự từng dòng, là lựa chọn số 1 cho các bài toán tra cứu tức thì và xuất báo cáo lớn.
4.5. Bảng ảo ngoại tuyến trên RAM: DataTable
Đặc tính kỹ thuật:
Disconnected Mode: Là đối tượng thuộc không gian tên System.Data, đại diện cho một bảng dữ liệu đầy đủ (gồm các cột Columns và các dòng Rows) nằm hoàn toàn trên bộ nhớ RAM.
Kết hợp cùng SqlDataAdapter: Adapter sẽ mở kết nối, đổ đầy dữ liệu vào bảng (adapter.Fill(dataTable)), sau đó tự động đóng kết nối ngay lập tức.
Ưu điểm lớn: Dễ dàng gán trực tiếp làm nguồn dữ liệu cho lưới hiển thị (ví dụ: dgvProducts.DataSource = dataTable trên WinForms) mà không cần duy trì kết nối mạng liên tục.
5. Phân biệt 3 phương thức thực thi của SqlCommand
Sinh viên cần phân biệt rõ khi nào sử dụng phương thức nào để tránh viết nhầm mã nguồn:
Phương thức
Kiểu trả về
Khi nào dùng?
Ví dụ thực tế
ExecuteScalar()
object (Ép kiểu về int, decimal, string...)
Khi câu truy vấn trả về chính xác 1 ô duy nhất (dòng 1, cột 1).
Đếm số lượng (COUNT), tính tổng tiền (SUM), lấy giá trị mã vừa tạo tự tăng (SCOPE_IDENTITY()).
ExecuteNonQuery()
int (Số dòng bị tác động)
Khi thực hiện các lệnh ghi/sửa đổi dữ liệu, không cần nhận bảng kết quả về.
Thêm mới (INSERT), Cập nhật (UPDATE), Xóa bỏ (DELETE).
ExecuteReader()
SqlDataReader (Luồng đọc dữ liệu)
Khi câu lệnh SELECT trả về danh sách nhiều dòng, nhiều cột.
Đọc danh sách sản phẩm, tra cứu thông tin khách hàng, xuất báo cáo.

6. Hướng dẫn thực hành từng bước (3 Bài tập nền tảng)
Cấu hình môi trường trước khi chạy:
Chuỗi kết nối mẫu được thống nhất sử dụng trong phòng máy thực hành:



C#
string connString = "Server=localhost;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;";


(Lưu ý: Nếu máy tính cá nhân cài đặt SQL Server Express, hãy đổi Server=localhost thành Server=.\\SQLEXPRESS).

BÀI TẬP THỰC HÀNH 1: Đếm tổng số mặt hàng bằng ExecuteScalar()
Mục tiêu: Sử dụng ExecuteScalar() để lấy về duy nhất một con số nguyên đại diện cho số lượng mặt hàng trong siêu thị.
Thời gian thực hiện dự kiến: 10 phút.
Mã nguồn chi tiết có chú thích:



C#
using System;
using Microsoft.Data.SqlClient;

namespace ThucHanhAdoNet_Tiet1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Chuỗi kết nối trỏ tới CSDL MiniSupermarketDb
            string connString = "Server=localhost;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;";

            Console.WriteLine("=== BÀI TẬP 1: ĐẾM TỔNG SỐ LƯỢNG MẶT HÀNG ===");

            // 2. Khởi tạo đối tượng SqlConnection trong khối using để tự động giải phóng
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    // 3. Câu lệnh SQL đếm toàn bộ số dòng trong bảng Products
                    string query = "SELECT COUNT(*) FROM Products";

                    // 4. Khởi tạo đối tượng SqlCommand gắn câu lệnh với kết nối
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // 5. Bắt buộc phải mở kết nối trước khi thực thi
                        connection.Open();
                        Console.WriteLine("-> Đã kết nối thành công tới SQL Server!");

                        // 6. ExecuteScalar() trả về một đối tượng kiểu object
                        // Cần ép kiểu tường minh sang kiểu int
                        object result = command.ExecuteScalar();
                        int totalCount = Convert.ToInt32(result);

                        // 7. Hiển thị kết quả ra màn hình
                        Console.WriteLine($"-> Tổng số mặt hàng hiện có trong kho siêu thị: {totalCount} sản phẩm.");
                    }
                }
                catch (SqlException ex)
                {
                    // Xử lý lỗi nếu sai chuỗi kết nối hoặc sai tên bảng CSDL
                    Console.WriteLine($"[LỖI SQL]: Không thể truy vấn CSDL. Chi tiết: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Xử lý các lỗi ngoại lệ chung
                    Console.WriteLine($"[LỖI HỆ THỐNG]: {ex.Message}");
                }
            } // Kết thúc using, connection.Close() và connection.Dispose() tự động được gọi

            Console.WriteLine("-> Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
    }
}


Kết quả hiển thị mong đợi trên màn hình Console:

=== BÀI TẬP 1: ĐẾM TỔNG SỐ LƯỢNG MẶT HÀNG ===
-> Đã kết nối thành công tới SQL Server!
-> Tổng số mặt hàng hiện có trong kho siêu thị: 20 sản phẩm.
-> Nhấn phím bất kỳ để tiếp tục...


BÀI TẬP THỰC HÀNH 2: Cập nhật tồn kho sản phẩm bằng ExecuteNonQuery() và SqlParameter
Mục tiêu: Thực hiện thao tác sửa đổi dữ liệu an toàn, sử dụng SqlParameter để cập nhật số lượng tồn kho của một sản phẩm cụ thể.
Thời gian thực hiện dự kiến: 15 phút.
Mã nguồn chi tiết có chú thích:



C#
using System;
using Microsoft.Data.SqlClient;

namespace ThucHanhAdoNet_Tiet1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connString = "Server=localhost;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;";

            Console.WriteLine("=== BÀI TẬP 2: CẬP NHẬT TỒN KHO AN TOÀN VỚI SQLPARAMETER ===");

            // Giả lập thông tin do nhân viên nhập từ bàn phím
            int maSanPhamCanSua = 1;      // Mã sản phẩm Snack O'Star
            int soLuongTonKhoMoi = 250;    // Số lượng kiểm kê mới nhập kho

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    // 1. Viết câu lệnh UPDATE có chứa 2 tham số @Stock và @Id (Tuyệt đối không cộng chuỗi)
                    string updateSql = @"UPDATE Products 
                                         SET StockQuantity = @Stock 
                                         WHERE ProductId = @Id";

                    using (SqlCommand command = new SqlCommand(updateSql, connection))
                    {
                        // 2. Gán giá trị an toàn vào tham số thông qua AddWithValue
                        command.Parameters.AddWithValue("@Stock", soLuongTonKhoMoi);
                        command.Parameters.AddWithValue("@Id", maSanPhamCanSua);

                        // 3. Mở kết nối
                        connection.Open();

                        // 4. Thực thi ExecuteNonQuery() để nhận về số dòng bị thay đổi trong CSDL
                        int rowsAffected = command.ExecuteNonQuery();

                        // 5. Kiểm tra kết quả thực thi
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"-> THÀNH CÔNG: Đã cập nhật tồn kho cho Sản phẩm ID = {maSanPhamCanSua} thành {soLuongTonKhoMoi} đơn vị.");
                            Console.WriteLine($"-> Số dòng dữ liệu bị ảnh hưởng: {rowsAffected} dòng.");
                        }
                        else
                        {
                            Console.WriteLine($"-> CẢNH BÁO: Không tìm thấy sản phẩm có ID = {maSanPhamCanSua} để cập nhật.");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"[LỖI SQL]: {ex.Message}");
                }
            }

            Console.WriteLine("-> Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
    }
}


Kết quả hiển thị mong đợi trên màn hình Console:
=== BÀI TẬP 2: CẬP NHẬT TỒN KHO AN TOÀN VỚI SQLPARAMETER ===
-> THÀNH CÔNG: Đã cập nhật tồn kho cho Sản phẩm ID = 1 thành 250 đơn vị.
-> Số dòng dữ liệu bị ảnh hưởng: 1 dòng.
-> Nhấn phím bất kỳ để tiếp tục...


BÀI TẬP THỰC HÀNH 3: Đọc danh sách sản phẩm bằng SqlDataReader
Mục tiêu: Sử dụng SqlDataReader để đọc duyệt toàn bộ danh sách sản phẩm với tốc độ cao và định dạng dữ liệu đầu ra chuyên nghiệp.
Thời gian thực hiện dự kiến: 15 phút.
Mã nguồn chi tiết có chú thích:



C#
using System;
using Microsoft.Data.SqlClient;

namespace ThucHanhAdoNet_Tiet1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connString = "Server=localhost;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;";

            Console.WriteLine("=== BÀI TẬP 3: DUYỆT DANH SÁCH BẰNG SQLDATAREADER ===");

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    // 1. Câu truy vấn lấy danh sách mặt hàng
                    string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity 
                                     FROM Products 
                                     ORDER BY ProductId ASC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        // 2. Kích hoạt luồng đọc trực tiếp bằng ExecuteReader()
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("-----------------------------------------------------------------------------------------");
                            Console.WriteLine(string.Format("{0,-6} | {1,-14} | {2,-35} | {3,12} | {4,8}", 
                                "MÃ SP", "MÃ VẠCH", "TÊN SẢN PHẨM", "ĐƠN GIÁ", "TỒN KHO"));
                            Console.WriteLine("-----------------------------------------------------------------------------------------");

                            // 3. Vòng lặp while(reader.Read()): Dịch chuyển con trỏ xuống từng dòng dữ liệu tiếp theo
                            // Khi nào hết dữ liệu, hàm Read() sẽ tự động trả về giá trị false
                            int stt = 0;
                            while (reader.Read())
                            {
                                stt++;

                                // Đọc giá trị từng cột theo tên cột hoặc chỉ mục (index)
                                int id = Convert.ToInt32(reader["ProductId"]);
                                string barcode = reader["Barcode"].ToString() ?? "";
                                string name = reader["ProductName"].ToString() ?? "";
                                decimal price = Convert.ToDecimal(reader["Price"]);
                                int stock = Convert.ToInt32(reader["StockQuantity"]);

                                // In dòng dữ liệu căn lề đẹp mắt
                                Console.WriteLine(string.Format("{0,-6} | {1,-14} | {2,-35} | {3,12:N0} đ | {4,8}", 
                                    id, barcode, name, price, stock));
                            }

                            Console.WriteLine("-----------------------------------------------------------------------------------------");
                            Console.WriteLine($"-> Tổng cộng đã đọc thành công: {stt} mặt hàng từ SQL Server.");
                        } // Kết thúc using, reader tự động đóng lại
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"[LỖI SQL]: {ex.Message}");
                }
            }

            Console.WriteLine("-> Hoàn thành Tiết 1. Nhấn phím bất kỳ để kết thúc...");
            Console.ReadKey();
        }
    }
}


Kết quả hiển thị mong đợi trên màn hình Console:
=== BÀI TẬP 3: DUYỆT DANH SÁCH BẰNG SQLDATAREADER ===
-----------------------------------------------------------------------------------------
MÃ SP  | MÃ VẠCH        | TÊN SẢN PHẨM                        |      ĐƠN GIÁ |  TỒN KHO
-----------------------------------------------------------------------------------------
1      | 893456789001   | Snack Khoai Tây O'Star Gói 60g      |     12,500 đ |      250
2      | 893456789002   | Bánh Quy Bơ Danisa Hộp Thiếc 454g   |    135,000 đ |       40
3      | 893456789003   | Kẹo Dẻo Chupa Chups Hương Trái Cây  |     15,000 đ |      120
4      | 893456789004   | Bánh Chocopie Orion Hộp 12 Cái      |     54,000 đ |       60
5      | 893456789005   | Nước Ngọt Coca Cola Lon 320ml       |     10,000 đ |      320
-----------------------------------------------------------------------------------------
-> Tổng cộng đã đọc thành công: 20 mặt hàng từ SQL Server.
-> Hoàn thành Tiết 1. Nhấn phím bất kỳ để kết thúc...


7. Bảng tự kiểm tra kiến thức và đánh giá Tiết 1 (Checklist)
Sinh viên tự đánh giá mức độ đạt được của bản thân bằng cách đánh dấu vào các tiêu chí sau trước khi chuyển sang Tiết 2:
[ ] Tiêu chí 1: Đã giải thích được tại sao luôn bọc SqlConnection trong khối lệnh using (...).
[ ] Tiêu chí 2: Phân biệt được khi nào thì dùng ExecuteScalar() (lấy 1 ô), khi nào dùng ExecuteNonQuery() (lấy số dòng sửa đổi), khi nào dùng ExecuteReader() (duyệt danh sách nhiều dòng).
[ ] Tiêu chí 3: Biết cách gắn tham số @Parameter qua phương thức AddWithValue() để chống lỗi bảo mật SQL Injection.
[ ] Tiêu chí 4: Đã tự tay gõ, biên dịch không lỗi và chạy hiển thị kết quả đúng chuẩn cho cả 3 bài tập thực hành trên máy tính cá nhân/phòng máy.

TIẾT 2: API TRA CỨU MÃ VẠCH (BARCODE) SIÊU TỐC VÀ MÔ HÌNH NGẮT KẾT NỐI VỚI DATATABLE
1. Mục tiêu bài học (Tiết 2)
Sau khi hoàn thành Tiết 2, sinh viên có khả năng:
Hiểu nghiệp vụ thực tế: Hiểu vì sao tại quầy thu ngân Siêu thị Mini, thao tác quét mã Barcode đòi hỏi API ADO.NET đọc trực tiếp thay vì qua ORM cồng kềnh.
Làm chủ SqlDataReader: Áp dụng luồng đọc nhanh một chiều (Forward-Only) để bóc tách dữ liệu sản phẩm theo mã vạch với thời gian phản hồi dưới 100ms.
Làm chủ DataTable & SqlDataAdapter: Phân biệt và ứng dụng thành thạo mô hình ngắt kết nối (Disconnected Mode) để tải dữ liệu lưu đệm trên bộ nhớ RAM.
Tự xây dựng Web API: Viết hoàn chỉnh AdoProductController cung cấp 2 Endpoint chuyên biệt: tra cứu theo mã vạch và tải danh sách ngoại tuyến.
Kiểm thử độc lập: Sử dụng giao diện Swagger UI để gửi tham số, đối soát kết quả và phân tích mã lỗi HTTP phản hồi.
2. Bối cảnh nghiệp vụ quầy POS: Vì sao cần API ADO.NET tra cứu siêu tốc?
Tại các quầy thu ngân của siêu thị mini trong giờ cao điểm:
Nhân viên thu ngân cầm súng quét mã vạch (Barcode Scanner) và quét liên tục qua các món hàng.
Súng quét mã hoạt động như một bàn phím gửi chuỗi ký tự số (ví dụ: 893456789001) kèm phím Enter lên ứng dụng máy trạm.
Nếu mỗi lần quét mã, hệ thống phải nạp toàn bộ cấu trúc bảng quan hệ, khởi tạo ngữ cảnh theo dõi thay đổi phức tạp thì sẽ gây ra hiện tượng giật, trễ màn hình quầy thu ngân.
Giải pháp: Sử dụng ADO.NET thuần túy kết hợp SqlDataReader. Khi có yêu cầu, một luồng đọc trực tiếp một chiều được mở ra, đọc đúng 1 dòng dữ liệu rồi đóng ngay lập tức, đảm bảo hiệu năng phản hồi tức thì.
3. So sánh trực quan: Mô hình Kết nối (Connected) vs Ngắt kết nối (Disconnected)



Plaintext
┌─────────────────────────────────────────────────────────────────────────────┐
│              SO SÁNH HAI MÔ HÌNH TRUY XUẤT DỮ LIỆU TRONG ADO.NET             │
├───────────────────────┬──────────────────────────┬──────────────────────────┤
│ Tiêu chí              │ Connected Mode           │ Disconnected Mode        │
│                       │ (Mô hình Kết nối)        │ (Mô hình Ngắt kết nối)   │
├───────────────────────┼──────────────────────────┼──────────────────────────┤
│ Đối tượng đại diện    │ SqlDataReader            │ DataTable, DataSet       │
│ Cầu nối trung gian    │ SqlCommand               │ SqlDataAdapter           │
│ Trạng thái kết nối    │ SqlConnection PHẢI MỞ    │ SqlConnection ĐÃ ĐÓNG    │
│                       │ trong suốt lúc đọc       │ ngay sau khi Fill() xong │
│ Chiếm dụng bộ nhớ RAM │ Cực kỳ ít (đọc từng dòng)│ Nhiều hơn (lưu cả bảng)  │
│ Tốc độ xử lý          │ Cực nhanh (Forward-Only) │ Trung bình               │
│ Ứng dụng thực tế      │ Quét mã vạch quầy POS,   │ Đổ dữ liệu DataGridView, │
│                       │ xuất báo cáo nghìn dòng  │ làm việc khi mất mạng    │
└───────────────────────┴──────────────────────────┴──────────────────────────┘


4. Hướng dẫn lập trình chi tiết: Xây dựng AdoProductController.cs
Tạo mới tệp mã nguồn AdoProductController.cs trong thư mục Controllers của project MiniSupermarket.API:



C#
/*
 * Ho ten sinh vien: Nguyen Van Teo
 * Ma sinh vien: 123456789
 * Ngay tao: 15/09/2026
 * Mo ta: Controller ADO.NET thuan - Tra cuu ma vach sieu toc va demo DataTable
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    // DTO tra cuu san pham cho quay POS
    public class ProductLookupDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    [Route("api/ado-products")]
    [ApiController]
    public class AdoProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Constructor tiêm IConfiguration de doc chuoi ket noi tu appsettings.json
        public AdoProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =========================================================================
        // THAO TÁC 1: TRA CỨU THEO MÃ VẠCH (CONNECTED MODE - SQLDATAREADER)
        // =========================================================================
        // GET: api/ado-products/barcode/893456789001
        [HttpGet("barcode/{barcode}")]
        public IActionResult GetProductByBarcode(string barcode)
        {
            // 1. Kiem tra tinh hop le cua tham so dau vao
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return BadRequest(new { message = "Mã vạch không được để trống!" });
            }

            // 2. Lay chuoi ket noi toi SQL Server
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            // 3. Su dung khoi using de dam bao SqlConnection luon duoc dong va giai phong
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // 4. Cau lenh SELECT co tham so hoa @Barcode chong SQL Injection
                string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity 
                                 FROM Products 
                                 WHERE Barcode = @Barcode";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 5. Gan gia tri an toan qua SqlParameter
                    command.Parameters.AddWithValue("@Barcode", barcode.Trim());

                    try
                    {
                        // 6. Mo ket noi vat ly toi SQL Server
                        connection.Open();

                        // 7. Khoi tao luong doc nhanh Forward-Only SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // 8. Doc ban ghi: reader.Read() tra ve true neu tim thay dong du lieu
                            if (reader.Read())
                            {
                                var productDto = new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                                };

                                // Tra ve ma 200 OK kem du lieu san pham
                                return Ok(productDto);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Bat loi neu ket noi CSDL gap su co
                        return StatusCode(500, new { message = "Lỗi truy vấn SQL: " + ex.Message });
                    }
                }
            } // Ket thuc using, connection.Close() tu dong duoc thuc thi tai day

            // 9. Neu khong tim thay san pham nao khop ma vach -> Tra ve 404 Not Found
            return NotFound(new { message = $"Không tìm thấy sản phẩm có mã vạch: {barcode}" });
        }

        // =========================================================================
        // THAO TÁC 2: TẢI DANH SÁCH LƯU ĐỆM (DISCONNECTED MODE - DATATABLE & ADAPTER)
        // =========================================================================
        // GET: api/ado-products/disconnected-cache
        [HttpGet("disconnected-cache")]
        public IActionResult GetProductsDisconnected()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            // 1. Khoi tao bang ao DataTable tren RAM
            DataTable productTable = new DataTable("CachedProducts");

            // 2. Mo ket noi va su dung SqlDataAdapter de nap du lieu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity 
                                 FROM Products 
                                 ORDER BY ProductId ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 3. SqlDataAdapter lam cau noi giua Command va DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // 4. Phuong thuc Fill(): Tu dong Open connection -> Doc du lieu do vao DataTable -> Tu dong Close connection
                        adapter.Fill(productTable);
                    }
                }
            } // Tai day, ket noi CSDL DA DUOC DONG HOAN TOAN!

            // 5. Thao tac doc lap tren bo nho RAM (Khong can ket noi toi SQL Server nua)
            var resultList = new List<ProductLookupDto>();

            // Duyet qua tung dong (DataRow) trong tap hop Rows cua DataTable
            foreach (DataRow row in productTable.Rows)
            {
                resultList.Add(new ProductLookupDto
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Barcode = row["Barcode"].ToString() ?? string.Empty,
                    ProductName = row["ProductName"].ToString() ?? string.Empty,
                    Price = Convert.ToDecimal(row["Price"]),
                    StockQuantity = Convert.ToInt32(row["StockQuantity"])
                });
            }

            // Tra ve danh sach san pham da duoc luu dem tren RAM
            return Ok(new
            {
                totalLoaded = productTable.Rows.Count,
                cachedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                data = resultList
            });
        }
    }
}


5. Các bước thực hành kiểm thử trên lớp (Step-by-Step Testing)
Bước 1: Khởi chạy dự án và mở Swagger UI
Nhấn F5 hoặc Ctrl + F5 để khởi chạy MiniSupermarket.API.
Trình duyệt tự động mở trang Swagger UI tại địa chỉ https://localhost:7xxx/swagger/index.html.
Tìm đến nhóm AdoProduct.
Bước 2: Kiểm thử Tra cứu mã vạch có thật (Kịch bản Thành công)
Thao tác:
Mở rộng mục GET /api/ado-products/barcode/{barcode}  Bấm Try it out  Nhập tham số:
barcode: 893456789001  Bấm Execute.
Kết quả phản hồi (Response 200 OK):
JSON
{
  "productId": 1,
  "barcode": "893456789001",
  "productName": "Snack Khoai Tây O'Star Gói 60g",
  "price": 12500,
  "stockQuantity": 150
}


Bước 3: Kiểm thử Tra cứu mã vạch không tồn tại (Kịch bản Ngoại lệ)
Thao tác:
Nhập tham số barcode: 999999999999  Bấm Execute.
Kết quả phản hồi (Response 404 Not Found):
{
  "message": "Không tìm thấy sản phẩm có mã vạch: 999999999999"
}


Bước 4: Kiểm thử Mô hình ngắt kết nối (DataTable Cache)
Thao tác:
Mở rộng mục GET /api/ado-products/disconnected-cache  Bấm Try it out  Bấm Execute.
Kết quả phản hồi (Response 200 OK):
JSON
{
  "totalLoaded": 20,
  "cachedAt": "2026-09-15 08:45:10",
  "data": [
    {
      "productId": 1,
      "barcode": "893456789001",
      "productName": "Snack Khoai Tây O'Star Gói 60g",
      "price": 12500,
      "stockQuantity": 150
    },
    {
      "productId": 2,
      "barcode": "893456789002",
      "productName": "Bánh Quy Bơ Danisa Hộp Thiếc 454g",
      "price": 135000,
      "stockQuantity": 40
    }
  ]
}


6. Bài tập thực hành tại chỗ (Dành cho sinh viên tự rèn luyện)
Bài tập 2.1: Bổ sung Endpoint tra cứu theo khoảng giá tồn kho
Yêu cầu: Viết thêm một Action [HttpGet("filter-stock")] sử dụng SqlDataReader, nhận tham số minStock (ví dụ: lấy các sản phẩm có tồn kho dưới 50 đơn vị để cảnh báo nhập thêm hàng).
Gợi ý viết code:
C#
string query = "SELECT ProductId, Barcode, ProductName, Price, StockQuantity FROM Products WHERE StockQuantity <= @MinStock";
cmd.Parameters.AddWithValue("@MinStock", minStock);


Bài tập 2.2: Đếm số lượng sản phẩm theo từng nhóm danh mục bằng DataTable
Yêu cầu: Sử dụng SqlDataAdapter.Fill() đổ dữ liệu truy vấn SELECT CategoryId, COUNT(*) AS Total FROM Products GROUP BY CategoryId vào một DataTable, sau đó duyệt in kết quả ra màn hình.
7. Tiêu chí nghiệm thu Tiết 2 (Checklist)
[ ] Tiêu chí 1: Tạo thành công file AdoProductController.cs trong thư mục Controllers không bị lỗi biên dịch.
[ ] Tiêu chí 2: Thực thi thành công GET /api/ado-products/barcode/893456789001 trên Swagger, nhận về mã 200 OK kèm thông tin sản phẩm.
[ ] Tiêu chí 3: Nhập mã vạch sai kiểm tra hệ thống trả về đúng mã lỗi 404 Not Found thân thiện.
[ ] Tiêu chí 4: Hiểu và giải thích được cơ chế tự động mở và đóng kết nối của phương thức adapter.Fill(dataTable).

TIẾT 3: TỐI ƯU TRUY VẤN VỚI STORED PROCEDURE & BÁO CÁO DOANH THU CA BÁN LẺ
1. Mục tiêu bài học (Tiết 3)
Sau khi hoàn thành Tiết 3, sinh viên có khả năng:
Hiểu bản chất Stored Procedure: Trình bày được lý do các hệ thống doanh nghiệp sử dụng Stored Procedure (thủ tục lưu trữ) thay vì viết câu lệnh SQL động trực tiếp trong ứng dụng.
Viết câu lệnh SQL tổng hợp: Tự tay viết và thực thi đoạn mã T-SQL tạo Stored Procedure trên SQL Server, sử dụng các hàm gom nhóm và tính toán (GROUP BY, SUM, COUNT, ORDER BY).
Làm chủ SqlCommand với Stored Procedure: Biết cách cấu hình thuộc tính CommandType = CommandType.StoredProcedure trên C# ADO.NET để gọi thủ tục lưu trữ từ xa.
Kết hợp SqlDataReader xuất báo cáo: Áp dụng luồng đọc nhanh một chiều để duyệt qua tập kết quả thống kê doanh thu và ánh xạ thành danh sách DTO nhẹ, tối ưu RAM.
Tự xây dựng API Báo cáo Doanh thu: Hiện thực hóa Controller ReportsController trả về dữ liệu thống kê ca làm việc theo từng nhân viên thu ngân (CashierUsername), kiểm thử thành công trên Swagger UI.
2. Vì sao hệ thống Siêu thị sử dụng Stored Procedure cho Báo cáo?
Trong môi trường thực tế của một chuỗi bán lẻ:
Tốc độ biên dịch sẵn: Khi viết câu lệnh SQL động, mỗi lần ứng dụng gọi tới, SQL Server phải phân tích cú pháp (Parse), tối ưu hóa và lập kế hoạch thực thi (Execution Plan). Với Stored Procedure, đoạn script được biên dịch và lưu sẵn trong bộ nhớ đệm của CSDL, giúp truy vấn chạy nhanh hơn đáng kể.
Giảm tải băng thông mạng: Thay vì gửi một đoạn truy vấn T-SQL dài hàng chục dòng từ C# qua đường truyền mạng, ứng dụng chỉ cần gửi duy nhất một dòng ngắn gọn: tên thủ tục (ví dụ: sp_GetRevenueByCashier).
Tách biệt tầng dữ liệu: Nếu phòng kế toán thay đổi công thức tính doanh thu (ví dụ: trừ thêm thuế VAT hay chiết khấu), lập trình viên CSDL chỉ cần cập nhật lại Stored Procedure trên SQL Server mà không cần biên dịch lại hay triển khai lại toàn bộ Web API.
Bảo mật tuyệt đối: Cấp quyền cho nhân viên thu ngân thực thi Stored Procedure mà không cần cấp quyền đọc trực tiếp vào các bảng nhạy cảm như Orders hay Users.
┌─────────────────────────────────────────────────────────────────────────────┐
│                 LUỒNG GỌI STORED PROCEDURE TRONG ADO.NET                    │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│   [ C# Web API (ReportsController) ]                                        │
│            │                                                                │
│            │ 1. Gửi lệnh: cmd.CommandText = "sp_GetRevenueByCashier"        │
│            │    cmd.CommandType = CommandType.StoredProcedure               │
│            ▼                                                                │
│   [ Kênh kết nối SqlConnection ] ═══════════════► [ CSDL SQL Server ]       │
│                                                           │                 │
│                                                           │ 2. Thực thi mã  │
│                                                           │    đã biên dịch │
│                                                           ▼                 │
│   [ Nhận luồng dữ liệu bảng ] ◄══════════════════ [ sp_GetRevenueByCashier ]│
│            │                                                                │
│            │ 3. SqlDataReader reader = cmd.ExecuteReader()                  │
│            │    Duyệt tuần tự: while(reader.Read())                         │
│            ▼                                                                │
│   [ Danh sách DTO Báo cáo JSON ] ──► Trả về Client (Swagger/WinForms POS)   │
│                                                                             │
└─────────────────────────────────────────────────────────────────────────────┘


3. Khảo sát cấu trúc bảng Orders phục vụ báo cáo
Báo cáo doanh thu ca trực được tính toán dựa trên dữ liệu các hóa đơn đã phát sinh trong bảng Orders (tương ứng với 15 giao dịch mẫu đã có sẵn trong CSDL):

┌──────────────────────────────────────────────────────────────────────────────┐
│ CẤU TRÚC BẢNG Orders LIÊN QUAN ĐẾN BÁO CÁO                                  │
├──────────────────┬─────────────────┬─────────────────────────────────────────┤
│ Tên cột          │ Kiểu dữ liệu    │ Ý nghĩa nghiệp vụ                         │
├──────────────────┼─────────────────┼─────────────────────────────────────────┤
│ OrderId          │ INT (PK)        │ Khóa chính định danh hóa đơn             │
│ OrderCode        │ VARCHAR(30)     │ Mã hóa đơn hiển thị (VD: HD-20260830-01) │
│ CreatedDate      │ DATETIME        │ Thời điểm thanh toán                     │
│ CashierUsername  │ VARCHAR(50)     │ Tài khoản thu ngân lập đơn (Gom nhóm)    │
│ CustomerId       │ INT (FK)        │ Khách hàng mua (nếu có)                  │
│ TotalItems       │ INT             │ Tổng số lượng sản phẩm trong đơn          │
│ FinalAmount      │ DECIMAL(18,2)   │ Tổng tiền thanh toán thực tế (Doanh thu)  │
│ RewardPoints     │ INT             │ Điểm tích lũy tặng cho khách            │
└──────────────────┴─────────────────┴─────────────────────────────────────────┘


4. Hướng dẫn thực hành từng bước (Step-by-Step)
BƯỚC 1: TẠO STORED PROCEDURE TRÊN SQL SERVER MANAGEMENT STUDIO (SSMS)
Mở SQL Server Management Studio (SSMS) và đăng nhập vào máy chủ CSDL.
Tại cây thư mục bên trái, chọn CSDL MiniSupermarketDb.
Bấm nút New Query (hoặc nhấn Ctrl + N), dán đoạn mã script T-SQL dưới đây và bấm Execute (F5):
-- =========================================================================
-- Ho ten sinh vien: Nguyen Van Teo
-- Ma sinh vien: 123456789
-- Mo ta: Thu tuc luu tru thong ke tong hop doanh thu theo thu ngan
-- =========================================================================

-- Xoa thu tuc cu neu da ton tai truoc do de tao lai sach se
IF OBJECT_ID('sp_GetRevenueByCashier', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetRevenueByCashier;
GO

CREATE PROCEDURE sp_GetRevenueByCashier
AS
BEGIN
    -- SET NOCOUNT ON: Ngan SQL Server gui thong bao so dong bi anh huong (DoneInProc)
    -- Giup toi uu toc do truyen goi tin qua mang cho SqlDataReader
    SET NOCOUNT ON;

    -- Truy van gom nhom theo ten dang nhap thu ngan (CashierUsername)
    -- Tinh tong hoa don, tong doanh thu va tong diem thuong da phat hanh
    SELECT 
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(FinalAmount) AS TotalRevenue,
        SUM(RewardPoints) AS TotalPointsAwarded
    FROM Orders
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC; -- Sap xep doanh thu tu cao xuong thap
END
GO


Kiểm tra trực tiếp kết quả trên SSMS:
Mở một tab truy vấn mới và gõ lệnh sau để chạy thử:
SQL
EXEC sp_GetRevenueByCashier;

Bảng kết quả trả về đúng theo bộ dữ liệu 15 hóa đơn mẫu:
Plaintext
CashierUsername | TotalOrders | TotalRevenue  | TotalPointsAwarded
----------------+-------------+---------------+-------------------
cashier05       | 3           | 1328000.00    | 132
cashier03       | 2           | 1435000.00    | 143
cashier01       | 4           | 1322000.00    | 131
cashier04       | 3           | 484000.00     | 48
cashier02       | 3           | 253000.00     | 24


BƯỚC 2: ĐỊNH NGHĨA DTO BÁO CÁO DOANH THU TRÊN C#
Mở project MiniSupermarket.API trong Visual Studio. Trong thư mục Models (hoặc tạo mới thư mục con DTOs), thêm file CashierRevenueDto.cs:
C#
/*
 * Ho ten sinh vien: Nguyen Van Teo
 * Ma sinh vien: 123456789
 * Mo ta: DTO hung du lieu ket qua thong ke tu Stored Procedure
 */
namespace MiniSupermarket.API.Models
{
    public class CashierRevenueDto
    {
        // Ten dang nhap cua thu ngan chiu trach nhiem ca ban
        public string CashierUsername { get; set; } = string.Empty;

        // Tong so luong don hang thu ngan da xu ly thanh cong
        public int TotalOrders { get; set; }

        // Tong so tien thu ve cho sieu thi (VND)
        public decimal TotalRevenue { get; set; }

        // Tong diem thuong da tich luy cho khach hang
        public int TotalPointsAwarded { get; set; }
    }
}


BƯỚC 3: XÂY DỰNG API KẾT XUẤT BÁO CÁO (ReportsController.cs)
Trong thư mục Controllers của project MiniSupermarket.API, tạo mới file ReportsController.cs:



C#
/*
 * Ho ten sinh vien: Nguyen Van Teo
 * Ma sinh vien: 123456789
 * Ngay tao: 15/09/2026
 * Mo ta: Controller ADO.NET thuc thi Stored Procedure xuat bao cao ca ban
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Models;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Dependency Injection lay chuoi ket noi tu appsettings.json
        public ReportsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =========================================================================
        // GET: api/reports/revenue-by-cashier
        // Chuc nang: Goi Stored Procedure sp_GetRevenueByCashier qua SqlDataReader
        // =========================================================================
        [HttpGet("revenue-by-cashier")]
        public IActionResult GetRevenueByCashier()
        {
            var reportList = new List<CashierRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            // 1. Khoi tao SqlConnection trong khoi using dam bao tu dong dong ket noi
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // 2. Gan CommandText la CHINH XAC ten Stored Procedure tren SQL Server
                using (SqlCommand command = new SqlCommand("sp_GetRevenueByCashier", connection))
                {
                    // 3. DIEM KHOA HOC QUAN TRONG: Thiet lap CommandType la StoredProcedure
                    // Neu thieu dong nay, SQL Server se hieu nham day la chuoi SQL thuong va bao loi cu phap!
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        // 4. Mo ket noi vat ly toi SQL Server
                        connection.Open();

                        // 5. Thuc thi bang ExecuteReader(): Nhan ve luong du lieu forward-only
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // 6. Vong lap doc tung dong ket qua cua Stored Procedure
                            while (reader.Read())
                            {
                                // Anh xa tung cot du lieu tu reader vao doi tuong DTO
                                var item = new CashierRevenueDto
                                {
                                    CashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                                    TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                    TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]),
                                    TotalPointsAwarded = reader["TotalPointsAwarded"] != DBNull.Value 
                                        ? Convert.ToInt32(reader["TotalPointsAwarded"]) 
                                        : 0
                                };

                                reportList.Add(item);
                            }
                        } // reader tu dong Close() tai day
                    }
                    catch (SqlException ex)
                    {
                        // Bat loi neu quen tao Stored Procedure tren CSDL hoac sai chuoi ket noi
                        return StatusCode(500, new 
                        { 
                            success = false,
                            message = "Lỗi thực thi Stored Procedure trên SQL Server: " + ex.Message 
                        });
                    }
                }
            } // connection tu dong Close() va Dispose() tai day

            // 7. Tra ve ma 200 OK kem danh sach bao cao da tong hop
            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                totalCashiers = reportList.Count,
                grandTotalRevenue = reportList.Sum(x => x.TotalRevenue),
                data = reportList
            });
        }
    }
}


5. Các bước thực hành kiểm thử trên lớp (Step-by-Step Testing)
Bước 1: Khởi chạy ứng dụng Web API
Trong Visual Studio, nhấn F5 (hoặc Ctrl + F5) để khởi chạy MiniSupermarket.API.
Trình duyệt tự động mở giao diện Swagger UI (https://localhost:7xxx/swagger/index.html).
Kéo xuống danh mục tìm thẻ Reports.
Bước 2: Kiểm thử gọi API Báo cáo Doanh thu
Thao tác:
Bấm mở rộng thanh màu xanh GET /api/Reports/revenue-by-cashier.
Bấm nút Try it out ở góc bên phải.
Bấm nút Execute màu xanh dương lớn.
Kết quả hiển thị mong đợi (Response Status: 200 OK):

JSON
{
  "success": true,
  "generatedAt": "2026-09-15 09:30:45",
  "totalCashiers": 5,
  "grandTotalRevenue": 4822000.00,
  "data": [
    {
      "cashierUsername": "cashier03",
      "totalOrders": 2,
      "totalRevenue": 1435000.00,
      "totalPointsAwarded": 143
    },
    {
      "cashierUsername": "cashier05",
      "totalOrders": 3,
      "totalRevenue": 1328000.00,
      "totalPointsAwarded": 132
    },
    {
      "cashierUsername": "cashier01",
      "totalOrders": 4,
      "totalRevenue": 1322000.00,
      "totalPointsAwarded": 131
    },
    {
      "cashierUsername": "cashier04",
      "totalOrders": 3,
      "totalRevenue": 484000.00,
      "totalPointsAwarded": 48
    },
    {
      "cashierUsername": "cashier02",
      "totalOrders": 3,
      "totalRevenue": 253000.00,
      "totalPointsAwarded": 24
    }
  ]
}


Bước 3: Phân tích lỗi thường gặp khi thực hành Tiết 3
Triệu chứng lỗi trên Swagger
Nguyên nhân cốt lõi
Cách khắc phục triệt để
HTTP 500: Could not find stored procedure 'sp_GetRevenueByCashier'
Sinh viên chưa bấm Execute script tạo thủ tục trên SSMS, hoặc tạo nhầm vào database khác (như master).
Mở lại SSMS, chọn đúng database MiniSupermarketDb, chạy lại đoạn script CREATE PROCEDURE.
HTTP 500: Incorrect syntax near 'sp_GetRevenueByCashier'
Thiếu câu lệnh command.CommandType = CommandType.StoredProcedure; trong C#.
Bổ sung dòng khai báo CommandType trước khi gọi connection.Open().
Lỗi dữ liệu InvalidCastException: Specified cast is not valid
Cột TotalRevenue trong SQL là DECIMAL, nhưng trong C# lại ép kiểu nhầm thành (int) hoặc (double).
Sử dụng hàm chuyển đổi chuẩn: Convert.ToDecimal(reader["TotalRevenue"]).

6. Bài tập thực hành mở rộng tại lớp (Sinh viên tự luyện)
Bài tập 3.1: Stored Procedure Báo cáo Doanh thu theo Khoảng ngày (Có tham số)
Yêu cầu: Viết thủ tục lưu trữ sp_GetRevenueByDateRange nhận vào 2 tham số: @FromDate DATETIME và @ToDate DATETIME.
Gợi ý viết T-SQL trên SSMS:
SQL
CREATE PROCEDURE sp_GetRevenueByDateRange
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(FinalAmount) AS TotalRevenue
    FROM Orders
    WHERE CreatedDate >= @FromDate AND CreatedDate <= @ToDate
    GROUP BY CashierUsername;
END
GO


Gợi ý truyền tham số từ C#:
C#
command.Parameters.AddWithValue("@FromDate", fromDate);
command.Parameters.AddWithValue("@ToDate", toDate);


Bài tập 3.2: Stored Procedure Top 5 Sản phẩm Bán chạy nhất
Yêu cầu: Viết Stored Procedure sp_GetTop5SellingProducts kết hợp bảng OrderDetails và Products để thống kê 5 món hàng có số lượng bán nhiều nhất, xuất ra API phục vụ Dashboard Admin.
7. Tiêu chí nghiệm thu Tiết 3 (Checklist)
[ ] Tiêu chí 1: Tạo thành công Stored Procedure sp_GetRevenueByCashier trên SQL Server Management Studio không có lỗi cú pháp.
[ ] Tiêu chí 2: Khai báo chính xác thuộc tính command.CommandType = CommandType.StoredProcedure; trong mã nguồn C#.
[ ] Tiêu chí 3: Gọi GET /api/Reports/revenue-by-cashier trên Swagger UI trả về mã 200 OK với đủ 5 dòng nhân viên thu ngân khớp với dữ liệu 15 hóa đơn mẫu.
[ ] Tiêu chí 4: Xử lý an toàn giá trị DBNull.Value khi đọc các cột tính toán tổng hợp từ SqlDataReader.
(Nội dung Tiết 3 đã hoàn thiện chuẩn mực. Bạn hãy xem qua và cho tín hiệu để tiếp tục biên soạn sang Tiết 4: "Chuẩn ACID và Kỹ thuật Điều khiển Giao dịch bằng SqlTransaction".)
TIẾT 4: CHUẨN ACID & KỸ THUẬT ĐIỀU KHIỂN GIAO DỊCH VỚI SQLTRANSACTION
1. Mục tiêu bài học (Tiết 4)
Sau khi hoàn thành Tiết 4, sinh viên có khả năng:
Hiểu bản chất Transaction: Trình bày được khái niệm giao dịch dữ liệu (Database Transaction) và sự cần thiết của nó trong các bài toán thương mại, bán lẻ.
Làm chủ 4 đặc tính ACID: Phân tích và giải thích được ý nghĩa thực tiễn của 4 tiêu chuẩn vàng: Atomicity (Nguyên tử), Consistency (Nhất quán), Isolation (Cô lập), Durability (Bền vững).
Nắm vững vòng đời SqlTransaction: Hiểu rõ cơ chế khởi tạo (BeginTransaction()), xác nhận ghi đĩa vĩnh viễn (Commit()), và hoàn tác sạch dữ liệu khi có lỗi (Rollback()).
Làm chủ cấu trúc an toàn: Viết thành thạo khối lệnh xử lý ngoại lệ try ... catch ... finally bọc kín phiên giao dịch để tránh hiện tượng treo kết nối hoặc khóa chết tài nguyên (Deadlock) trên SQL Server.
Gắn kết câu lệnh: Hiểu vì sao mọi đối tượng SqlCommand tham gia vào phiên giao dịch bắt buộc phải gán thuộc tính command.Transaction = transaction.
2. Bài toán thực tế tại Quầy thu ngân: Rủi ro khi không dùng Transaction
Hãy phân tích quy trình nghiệp vụ khi một khách hàng đặt mua 3 lon nước ngọt và 2 gói snack khoai tây tại quầy POS:
┌─────────────────────────────────────────────────────────────────────────────┐
│             CHUỖI 4 THAO TÁC CƠ SỞ DỮ LIỆU KHI BẤM "THANH TOÁN"             │
├─────────────────────────────────────────────────────────────────────────────┤
│ 1. INSERT INTO Orders          --> Sinh ra hóa đơn mới (Mã đơn, Tổng tiền)  │
│ 2. INSERT INTO OrderDetails    --> Lưu chi tiết 2 dòng sản phẩm đã mua      │
│ 3. UPDATE Products             --> Trừ số lượng tồn kho của 2 sản phẩm      │
│ 4. UPDATE Customers            --> Cộng điểm tích lũy thưởng cho khách hàng │
└─────────────────────────────────────────────────────────────────────────────┘


Điều gì sẽ xảy ra nếu KHÔNG CÓ Transaction?
Trường hợp đứt gãy: Bước 1 tạo hóa đơn thành công, bước 2 lưu chi tiết xong, nhưng đến bước 3 thì máy chủ bị sập nguồn hoặc sản phẩm thứ hai bị người khác mua hết trước khiến số lượng tồn bị âm.
Hậu quả nghiêm trọng:
Tiền đã tính cho khách nhưng kho hàng không trừ được.
Dữ liệu trong CSDL rơi vào trạng thái "nửa vời" (rác dữ liệu).
Kế toán và thủ kho đối chiếu số liệu cuối ngày bị lệch, không thể kiểm toán.
Giải pháp: Phải có một "chiếc khiên bảo vệ" sao cho hoặc là cả 4 thao tác cùng thành công, hoặc không có bất kỳ thao tác nào được lưu lại. Chiếc khiên đó chính là Transaction.
3. Khám phá 4 đặc tính ACID (Giải thích dễ hiểu cho sinh viên)
Chuẩn ACID là 4 tiêu chuẩn bắt buộc phải có của mọi hệ thống quản trị cơ sở dữ liệu giao dịch:
┌───┬─────────────────────────────────────────────────────────────────────────┐
│ A │ ATOMICITY (Tính nguyên tử - "Tất cả hoặc không có gì"):                 │
│   │ Giao dịch được xem như một khối thống nhất không thể chia cắt.          │
│   │ Nếu 1 câu lệnh bị lỗi, toàn bộ các câu lệnh trước đó đều bị xóa sạch!   │
├───┼─────────────────────────────────────────────────────────────────────────┤
│ C │ CONSISTENCY (Tính nhất quán):                                           │
│   │ CSDL chuyển từ trạng thái hợp lệ này sang trạng thái hợp lệ khác.       │
│   │ Không bao giờ có chuyện hóa đơn tồn tại mà không có dòng chi tiết nào.  │
├───┼─────────────────────────────────────────────────────────────────────────┤
│ I │ ISOLATION (Tính cô lập / Độc lập):                                      │
│   │ Các giao dịch chạy đồng thời ở các quầy POS khác nhau không can thiệp,  │
│   │ không nhìn thấy dữ liệu "tạm bợ" chưa chốt của nhau (Chống Dirty Read). │
├───┼─────────────────────────────────────────────────────────────────────────┤
│ D │ DURABILITY (Tính bền vững):                                             │
│   │ Một khi giao dịch đã Commit thành công, dữ liệu được ghi vào đĩa cứng.  │
│   │ Cho dù sau đó máy chủ mất điện ngay lập tức, dữ liệu vẫn không bị mất.  │
└───┴─────────────────────────────────────────────────────────────────────────┘


4. Vòng đời và Cơ chế hoạt động của SqlTransaction trong ADO.NET
Để điều khiển giao dịch trong C#, ta sử dụng đối tượng SqlTransaction thông qua 3 bước cốt lõi:

       [ SqlConnection.Open() ]
                  │
                  │ 1. connection.BeginTransaction()
                  ▼
       ┌─────────────────────────────────────────────────────────┐
       │             BẮT ĐẦU PHIÊN TRANSACTION BẢO VỆ            │
       │                                                         │
       │  cmd1.Transaction = trans;  --> Thực thi câu lệnh 1...  │
       │  cmd2.Transaction = trans;  --> Thực thi câu lệnh 2...  │
       │  cmd3.Transaction = trans;  --> Thực thi câu lệnh 3...  │
       └────────────────────────────┬────────────────────────────┘
                                    │
                     ┌──────────────┴──────────────┐
                     │ Kiểm tra kết quả thực thi?  │
                     └──────────────┬──────────────┘
                    Thành công 100% │              │ Có bất kỳ lỗi nào (Exception)
                                    ▼              ▼
                          [ trans.Commit() ]   [ trans.Rollback() ]
                                    │              │
                           (Chốt lưu vĩnh viễn)  (Hoàn tác về ban đầu)


BeginTransaction(): Được gọi từ đối tượng SqlConnection sau khi đã mở kết nối. Lệnh này báo hiệu cho SQL Server: "Hãy gom các câu lệnh tiếp theo vào bộ đệm giao dịch tạm thời, chưa được lưu chính thức".
Gắn kết SqlCommand: Mọi SqlCommand muốn nằm trong sự bảo vệ của giao dịch bắt buộc phải gán:
C#
command.Transaction = transaction;

(Nếu quên gắn, C# sẽ ném ngoại lệ InvalidOperationException ngay lập tức).
Commit(): Chỉ gọi khi tất cả các câu lệnh chạy xong suôn sẻ. Lúc này SQL Server mới chính thức ghi dữ liệu xuống đĩa cứng.
Rollback(): Đặt bên trong khối catch. Khi phát sinh bất kỳ lỗi nào, phương thức này được kích hoạt để hủy bỏ toàn bộ mọi thao tác đã thực hiện trước đó trong phiên.
5. Mẫu cấu trúc (Template) chuẩn mực điều khiển Transaction
Dưới đây là khung mẫu chuẩn bắt buộc sinh viên phải ghi nhớ và áp dụng:
using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    // 1. Khởi tạo SqlTransaction từ connection đã mở
    SqlTransaction transaction = connection.BeginTransaction();

    try
    {
        // 2. Thực thi thao tác thứ nhất (Gắn transaction vào command)
        using (SqlCommand cmd1 = new SqlCommand(sql1, connection, transaction))
        {
            cmd1.ExecuteNonQuery();
        }

        // 3. Thực thi thao tác thứ hai
        using (SqlCommand cmd2 = new SqlCommand(sql2, connection, transaction))
        {
            cmd2.ExecuteNonQuery();
        }

        // 4. Nếu chạy đến đây không có lỗi -> XÁC NHẬN LƯU VĨNH VIỄN
        transaction.Commit();
        Console.WriteLine("Giao dịch thành công trọn vẹn!");
    }
    catch (Exception ex)
    {
        // 5. Nếu gặp bất kỳ lỗi nào -> HOÀN TÁC TOÀN BỘ VỀ NGUYÊN TRẠNG
        try
        {
            transaction.Rollback();
            Console.WriteLine("Đã Rollback an toàn do phát sinh lỗi: " + ex.Message);
        }
        catch (Exception rollbackEx)
        {
            Console.WriteLine("Lỗi nghiêm trọng khi Rollback: " + rollbackEx.Message);
        }
    }
}


6. Hướng dẫn thực hành: Bài tập mô phỏng Trừ kho kết hợp Transaction
Mục tiêu: Viết một chương trình Console nhỏ để kiểm chứng trực quan cơ chế Rollback: Thực hiện cập nhật tồn kho cho 2 sản phẩm, trong đó sản phẩm thứ 2 cố tình gây lỗi để quan sát sản phẩm 1 được tự động khôi phục.
Thời gian thực hiện dự kiến: 20 phút.
Mã nguồn chi tiết có chú thích:
/*
 * Ho ten sinh vien: Nguyen Van Teo
 * Ma sinh vien: 123456789
 * Mo ta: Chuong trinh kiem chung co che Rollback cua SqlTransaction
 */
using System;
using Microsoft.Data.SqlClient;

namespace ThucHanhTransaction_Tiet4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connString = "Server=localhost;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;";

            Console.WriteLine("=== THỬ NGHIỆM ĐIỀU KHIỂN SQLTRANSACTION ===");

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                // 1. Khởi tạo Transaction
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    Console.WriteLine("-> Bắt đầu phiên giao dịch...");

                    // 2. Thao tác 1: Trừ 5 đơn vị tồn kho của Sản phẩm ID = 1 (Hợp lệ)
                    string sqlStep1 = "UPDATE Products SET StockQuantity = StockQuantity - @Qty WHERE ProductId = @Id";
                    using (SqlCommand cmd1 = new SqlCommand(sqlStep1, connection, transaction))
                    {
                        cmd1.Parameters.AddWithValue("@Qty", 5);
                        cmd1.Parameters.AddWithValue("@Id", 1);
                        int r1 = cmd1.ExecuteNonQuery();
                        Console.WriteLine($"-> Bước 1: Đã trừ tồn kho SP 1 (Số dòng tác động: {r1}).");
                    }

                    // 3. Thao tác 2: Cố tình gây lỗi cú pháp SQL hoặc cập nhật sản phẩm không hợp lệ
                    Console.WriteLine("-> Bước 2: Chuẩn bị thực thi thao tác lỗi giả lập...");
                    
                    // Giả lập tình huống kiểm tra logic bị lỗi: Ném ngoại lệ thủ công
                    bool isInventoryShortage = true; // Giả lập mặt hàng thứ 2 bị hết kho
                    if (isInventoryShortage)
                    {
                        throw new InvalidOperationException("Mặt hàng thứ hai đã hết tồn kho thực tế!");
                    }

                    // 4. Nếu không có lỗi thì mới Commit
                    transaction.Commit();
                    Console.WriteLine("-> GIAO DỊCH COMMIT THÀNH CÔNG!");
                }
                catch (Exception ex)
                {
                    // 5. Khi bắt được lỗi -> Lập tức Rollback
                    Console.WriteLine($"\n[PHÁT SINH LỖI]: {ex.Message}");
                    transaction.Rollback();
                    Console.WriteLine("-> [HỆ THỐNG]: Đã kích hoạt transaction.Rollback()!");
                    Console.WriteLine("-> Toàn bộ thay đổi của Bước 1 đã được hoàn trả, tồn kho SP 1 không bị trừ!");
                }
            }

            Console.WriteLine("\n-> Nhấn phím bất kỳ để kết thúc Tiết 4...");
            Console.ReadKey();
        }
    }
}


Kết quả quan sát trên màn hình Console:
=== THỬ NGHIỆM ĐIỀU KHIỂN SQLTRANSACTION ===
-> Bắt đầu phiên giao dịch...
-> Bước 1: Đã trừ tồn kho SP 1 (Số dòng tác động: 1).
-> Bước 2: Chuẩn bị thực thi thao tác lỗi giả lập...

[PHÁT SINH LỖI]: Mặt hàng thứ hai đã hết tồn kho thực tế!
-> [HỆ THỐNG]: Đã kích hoạt transaction.Rollback()!
-> Toàn bộ thay đổi của Bước 1 đã được hoàn trả, tồn kho SP 1 không bị trừ!

-> Nhấn phím bất kỳ để kết thúc Tiết 4...


Kiểm chứng trên SSMS:
Mở SSMS và chạy SELECT StockQuantity FROM Products WHERE ProductId = 1;. Sinh viên sẽ thấy số lượng tồn kho vẫn giữ nguyên con số ban đầu, chứng minh lệnh UPDATE ở Bước 1 đã bị thu hồi hoàn toàn.
7. Các lỗi sinh viên thường gặp khi dùng SqlTransaction
Triệu chứng lỗi
Nguyên nhân cốt lõi
Cách xử lý
InvalidOperationException: Execute requires the Command object to have a Transaction object when the Connection assigned to the command is in a pending local transaction.
Khi đã gọi BeginTransaction(), nếu khởi tạo SqlCommand mà quên truyền biến transaction vào constructor hoặc thuộc tính cmd.Transaction.
Luôn truyền biến transaction vào lệnh: new SqlCommand(sql, connection, transaction).
Giao diện hoặc phần mềm bị treo đơ (Lock table / Deadlock)
Sau khi gọi BeginTransaction(), có lỗi xảy ra nhưng quên gọi Rollback() trong khối catch, làm CSDL bị khóa dòng dữ liệu vô thời hạn.
Luôn đặt transaction.Rollback() bên trong khối catch.
InvalidOperationException: This SqlTransaction has completed; it is no longer usable.
Gọi phương thức Commit() hoặc Rollback() hai lần trên cùng một đối tượng transaction.
Kiểm tra luồng rẽ nhánh, đảm bảo mỗi nhánh chỉ gọi đúng 1 lần xác nhận hoặc hoàn tác.

8. Tiêu chí nghiệm thu Tiết 4 (Checklist)
[ ] Tiêu chí 1: Trình bày được ý nghĩa của 4 ký tự viết tắt ACID bằng ví dụ bán hàng siêu thị.
[ ] Tiêu chí 2: Giải thích được sự khác nhau giữa transaction.Commit() và transaction.Rollback().
[ ] Tiêu chí 3: Viết được khối lệnh try ... catch chuẩn mực có gán transaction cho từng câu lệnh SqlCommand.
[ ] Tiêu chí 4: Chạy thử nghiệm thành công bài tập mô phỏng, kiểm chứng được dữ liệu không bị thay đổi khi có ngoại lệ phát sinh.
(Nội dung Tiết 4 đã hoàn tất. Bạn hãy xem qua và cho tín hiệu để tiếp tục sang Tiết 5 – Tiết cuối cùng: "Hiện thực hóa toàn diện Web API Thanh toán POS đa bảng bọc kín trong SqlTransaction và Kịch bản kiểm thử Rollback khi hết kho".)

TIẾT 5: HIỆN THỰC HÓA API THANH TOÁN POS BỌC TRONG SQLTRANSACTION & KỊCH BẢN KIỂM THỬ
1. Mục tiêu bài học (Tiết 5)
Sau khi hoàn thành Tiết 5, sinh viên có khả năng:
Lắp ráp hoàn chỉnh luồng nghiệp vụ POS: Tự tay xây dựng API thanh toán hóa đơn bán lẻ khép kín gồm 4 bước liên hoàn (Tạo Orders --> Lưu OrderDetails --> Trừ tồn kho Products --> Cộng điểm thưởng Customers).
Thực thi giao dịch an toàn đa bảng: Áp dụng thuần thục đối tượng SqlTransaction để bảo vệ chuỗi ghi dữ liệu, đảm bảo đáp ứng chuẩn ACID.
Lấy mã khóa chính tự tăng: Sử dụng mệnh đề OUTPUT INSERTED.OrderId kết hợp hàm ExecuteScalar() để nhận lại ID vừa sinh ra ngay trong cùng phiên giao dịch.
Thành thạo kịch bản kiểm thử (Test Cases): Kiểm thử độc lập trên Swagger UI đối với cả 2 kịch bản: Thanh toán thành công (200 OK) và Kích hoạt Rollback khi cố tình mua vượt quá tồn kho (400 BadRequest).
Đối soát tính toàn vẹn trên SQL Server: Sử dụng SSMS kiểm tra trực tiếp các bảng để xác nhận không phát sinh dữ liệu rác khi giao dịch thất bại.
2. Thiết kế kiến trúc DTO nhận dữ liệu từ Client
Khi nhân viên thu ngân quét các mặt hàng và nhấn nút THANH TOÁN trên giao diện quầy POS, toàn bộ dữ liệu giỏ hàng sẽ được đóng gói thành một đối tượng JSON gửi về Web API.
Tạo mới tệp CheckoutDtos.cs trong thư mục Models của project MiniSupermarket.API:



C#
/*
 * Ho ten sinh vien: Nguyen Van Teo
 * Ma sinh vien: 123456789
 * Ngay tao: 15/09/2026
 * Mo ta: Cac lop DTO phuc vu nghiep vu thanh toan gio hang POS
 */
namespace MiniSupermarket.API.Models
{
    // DTO bieu dien tung dong san pham trong gio hang
    public class CartItemCheckoutDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    // DTO nhan yeu cau thanh toan tong the tu may POS
    public class CheckoutRequestDto
    {
        // Tai khoan thu ngan dang dung ca truc
        public string CashierUsername { get; set; } = string.Empty;

        // Ma dinh danh khach hang (neu la khach vang lai, truyen CustomerId = 15)
        public int CustomerId { get; set; }

        // Danh sach cac mon hang can thanh toan
        public List<CartItemCheckoutDto> Items { get; set; } = new();
    }
}


3. Mã nguồn hoàn chỉnh: Xây dựng CheckoutController.cs
Tạo mới tệp CheckoutController.cs trong thư mục Controllers của project MiniSupermarket.API:



C#
/*
 * Ho ten sinh vien: Nguyen Van Teo
 * Ma sinh vien: 123456789
 * Ngay tao: 15/09/2026
 * Mo ta: API Thanh toan POS da bang duoc bao ve boi SqlTransaction (ACID)
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Tiêm IConfiguration de doc chuoi ket noi CSDL
        public CheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =========================================================================
        // POST: api/checkout
        // Chuc nang: Thanh toan hoa don, tru ton kho va tich diem trong 1 Transaction
        // =========================================================================
        [HttpPost]
        public IActionResult ProcessCheckout([FromBody] CheckoutRequestDto request)
        {
            // 1. Kiem tra tinh hop le cua gio hang
            if (request.Items == null || request.Items.Count == 0)
            {
                return BadRequest(new { message = "Giỏ hàng rỗng, không thể tiến hành thanh toán!" });
            }

            if (string.IsNullOrWhiteSpace(request.CashierUsername))
            {
                return BadRequest(new { message = "Thiếu thông tin tài khoản thu ngân lập hóa đơn!" });
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // 2. KHOI TAO TRANSACTION (Bat dau phien giao dich khep kin bao ve ACID)
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Tinh toan tong tien, tong so luong va diem thuong tich luy
                    decimal finalAmount = request.Items.Sum(i => i.Quantity * i.UnitPrice);
                    int totalItems = request.Items.Sum(i => i.Quantity);

                    // Quy tac tich diem sieu thi: Cu moi 10.000d mua hang duoc tang 1 diem thuong
                    int rewardPoints = (int)(finalAmount / 10000);

                    // Sinh ma hoa don theo cau truc: HD-YYYYMMDD-XXXX (4 ky tu ngau nhien)
                    string orderCode = $"HD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

                    // =====================================================================
                    // BƯỚC 1: TAO HOA DON TONG THE TRONG BANG Orders
                    // =====================================================================
                    // Su dung OUTPUT INSERTED.OrderId de lay ve gia tri khoa chinh vua tu dong tang
                    string insertOrderSql = @"
                        INSERT INTO Orders (OrderCode, CreatedDate, CashierUsername, CustomerId, TotalItems, FinalAmount, RewardPoints)
                        OUTPUT INSERTED.OrderId
                        VALUES (@OrderCode, GETDATE(), @CashierUsername, @CustomerId, @TotalItems, @FinalAmount, @RewardPoints)";

                    int orderId = 0;
                    using (SqlCommand cmdOrder = new SqlCommand(insertOrderSql, connection, transaction))
                    {
                        // Gan tham so an toan
                        cmdOrder.Parameters.AddWithValue("@OrderCode", orderCode);
                        cmdOrder.Parameters.AddWithValue("@CashierUsername", request.CashierUsername.Trim());
                        cmdOrder.Parameters.AddWithValue("@CustomerId", request.CustomerId);
                        cmdOrder.Parameters.AddWithValue("@TotalItems", totalItems);
                        cmdOrder.Parameters.AddWithValue("@FinalAmount", finalAmount);
                        cmdOrder.Parameters.AddWithValue("@RewardPoints", rewardPoints);

                        // ExecuteScalar() tra ve gia tri OrderId vua duoc he thong sinh ra
                        orderId = (int)cmdOrder.ExecuteScalar();
                    }

                    // =====================================================================
                    // BƯỚC 2 & 3: XU LY TUNG MON HANG (KIEM TRA, LUU CHI TIET VA TRU TON KHO)
                    // =====================================================================
                    foreach (var item in request.Items)
                    {
                        // 3.1. Kiem tra so luong ton kho thuc te truoc khi tru
                        string checkStockSql = "SELECT StockQuantity, ProductName FROM Products WHERE ProductId = @ProductId";
                        int currentStock = 0;
                        string productName = string.Empty;

                        using (SqlCommand cmdCheck = new SqlCommand(checkStockSql, connection, transaction))
                        {
                            cmdCheck.Parameters.AddWithValue("@ProductId", item.ProductId);

                            using (SqlDataReader reader = cmdCheck.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    throw new InvalidOperationException($"Không tìm thấy sản phẩm có ID: {item.ProductId}");
                                }
                                currentStock = Convert.ToInt32(reader["StockQuantity"]);
                                productName = reader["ProductName"].ToString() ?? string.Empty;
                            }
                        }

                        // 3.2. DIEM KIEM SOAT LOGIC: Neu so luong mua vuot qua ton kho -> Kich hoat Exception
                        if (currentStock < item.Quantity)
                        {
                            throw new InvalidOperationException(
                                $"Sản phẩm '{productName}' (ID: {item.ProductId}) không đủ tồn kho! " +
                                $"(Tồn kho thực tế: {currentStock}, Số lượng yêu cầu mua: {item.Quantity})");
                        }

                        // 3.3. Them dong chi tiet vao bang OrderDetails
                        string insertDetailSql = @"
                            INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, LineTotal)
                            VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, @LineTotal)";

                        using (SqlCommand cmdDetail = new SqlCommand(insertDetailSql, connection, transaction))
                        {
                            cmdDetail.Parameters.AddWithValue("@OrderId", orderId);
                            cmdDetail.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdDetail.Parameters.AddWithValue("@Quantity", item.Quantity);
                            cmdDetail.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                            cmdDetail.Parameters.AddWithValue("@LineTotal", item.Quantity * item.UnitPrice);
                            cmdDetail.ExecuteNonQuery();
                        }

                        // 3.4. Khau tru so luong ton kho trong bang Products
                        string updateStockSql = @"
                            UPDATE Products 
                            SET StockQuantity = StockQuantity - @Qty 
                            WHERE ProductId = @ProductId";

                        using (SqlCommand cmdStock = new SqlCommand(updateStockSql, connection, transaction))
                        {
                            cmdStock.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmdStock.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdStock.ExecuteNonQuery();
                        }
                    }

                    // =====================================================================
                    // BƯỚC 4: CONG DIEM TICH LUY CHO KHACH HANG TRONG BANG Customers
                    // =====================================================================
                    if (rewardPoints > 0)
                    {
                        string updatePointsSql = @"
                            UPDATE Customers 
                            SET RewardPoints = RewardPoints + @Points 
                            WHERE CustomerId = @CustomerId";

                        using (SqlCommand cmdCustomer = new SqlCommand(updatePointsSql, connection, transaction))
                        {
                            cmdCustomer.Parameters.AddWithValue("@Points", rewardPoints);
                            cmdCustomer.Parameters.AddWithValue("@CustomerId", request.CustomerId);
                            cmdCustomer.ExecuteNonQuery();
                        }
                    }

                    // =====================================================================
                    // BƯỚC 5: XAC NHAN LUU VINH VIEN TOAN BO THAO TAC (COMMIT)
                    // =====================================================================
                    transaction.Commit();

                    // Tra ve ket qua thanh cong voi ma 200 OK
                    return Ok(new
                    {
                        success = true,
                        message = "Thanh toán hóa đơn POS thành công!",
                        orderId = orderId,
                        orderCode = orderCode,
                        totalItems = totalItems,
                        finalAmount = finalAmount,
                        rewardPointsAdded = rewardPoints
                    });
                }
                catch (Exception ex)
                {
                    // =====================================================================
                    // BƯỚC 6: HOAN TAC TOAN BO DU LIEU SACH SE KHI CO BAT KY LOI NAO (ROLLBACK)
                    // =====================================================================
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        return StatusCode(500, new 
                        { 
                            success = false,
                            message = "Lỗi nghiêm trọng khi thực thi Rollback: " + rollbackEx.Message 
                        });
                    }

                    // Tra ve thong bao loi than thien va ma 400 BadRequest
                    return BadRequest(new
                    {
                        success = false,
                        message = "Giao dịch thanh toán bị hủy và đã được Rollback an toàn!",
                        errorDetail = ex.Message
                    });
                }
            }
        }
    }
}


4. Kịch bản kiểm thử toàn diện trên lớp (Test Cases)
KỊCH BẢN 1: THANH TOÁN HỢP LỆ (GIAO DỊCH THÀNH CÔNG - COMMIT)
Mục tiêu: Mua các mặt hàng có sẵn trong kho với số lượng hợp lệ.
Thao tác trên Swagger UI:
Mở rộng mục POST /api/Checkout --> Bấm Try it out.
Dán đoạn mã JSON sau vào phần Request body:
JSON
{
  "cashierUsername": "cashier01",
  "customerId": 1,
  "items": [
    {
      "productId": 1,
      "quantity": 2,
      "unitPrice": 12500
    },
    {
      "productId": 5,
      "quantity": 3,
      "unitPrice": 10000
    }
  ]
}


Bấm Execute.
Kết quả phản hồi mong đợi (Status: 200 OK):
JSON
{
  "success": true,
  "message": "Thanh toán hóa đơn POS thành công!",
  "orderId": 16,
  "orderCode": "HD-20261006-A1B2",
  "totalItems": 5,
  "finalAmount": 55000.0,
  "rewardPointsAdded": 5
}


Đối soát thực tế trên SQL Server (SSMS):
Chạy SELECT * FROM Orders WHERE OrderId = 16; --> Thấy hóa đơn mới được lưu.
Chạy SELECT * FROM OrderDetails WHERE OrderId = 16; --> Thấy đúng 2 dòng chi tiết mặt hàng.
Chạy SELECT ProductId, StockQuantity FROM Products WHERE ProductId IN (1, 5); --> Thấy tồn kho SP 1 bị trừ 2 đơn vị, SP 5 bị trừ 3 đơn vị.
Chạy SELECT CustomerId, RewardPoints FROM Customers WHERE CustomerId = 1; --> Thấy điểm thưởng tăng thêm 5 điểm.
KỊCH BẢN 2: MUA VƯỢT QUÁ TỒN KHO THỰC TẾ (KÍCH HOẠT ROLLBACK)
Mục tiêu: Cố tình đặt mua số lượng vượt quá số lượng hàng tồn trong kho để kiểm chứng cơ chế tự động hoàn tác sạch của SqlTransaction.
Thao tác trên Swagger UI:
Dán đoạn mã JSON sau vào phần Request body (Trong đó sản phẩm ID 2 chỉ còn tồn kho 40 hộp Danisa, nhưng cố tình đặt mua 999 hộp):
JSON
{
  "cashierUsername": "cashier01",
  "customerId": 1,
  "items": [
    {
      "productId": 1,
      "quantity": 1,
      "unitPrice": 12500
    },
    {
      "productId": 2,
      "quantity": 999,
      "unitPrice": 135000
    }
  ]
}


Bấm Execute.
Kết quả phản hồi mong đợi (Status: 400 BadRequest):
JSON
{
  "success": false,
  "message": "Giao dịch thanh toán bị hủy và đã được Rollback an toàn!",
  "errorDetail": "Sản phẩm 'Bánh Quy Bơ Danisa Hộp Thiếc 454g' (ID: 2) không đủ tồn kho! (Tồn kho thực tế: 40, Số lượng yêu cầu mua: 999)"
}


Đối soát thực tế trên SQL Server (Minh chứng ACID tuyệt đối):
Chạy lệnh kiểm tra hóa đơn mới nhất:
SQL
SELECT TOP 1 * FROM Orders ORDER BY OrderId DESC;

--> Không có hóa đơn mới nào được tạo thêm (Không sinh ra dòng hóa đơn rác).
Kiểm tra tồn kho của sản phẩm ID 1:
SQL
SELECT ProductId, StockQuantity FROM Products WHERE ProductId = 1;

--> Số lượng tồn kho của sản phẩm 1 vẫn giữ nguyên, không hề bị trừ 1 đơn vị dù nó đứng trước sản phẩm 2 trong giỏ hàng.
5. Hướng dẫn Bài tập mở rộng: API Hủy đơn hàng và Hoàn trả tồn kho
Đề bài
Viết API Hủy đơn hàng bán lẻ (DELETE /api/checkout/cancel/{orderId}) bọc toàn bộ chuỗi xử lý trong SqlTransaction:
Kiểm tra hóa đơn có tồn tại hay không.
Đọc toàn bộ danh sách chi tiết các mặt hàng từ bảng OrderDetails thuộc hóa đơn này.
Cộng trả lại số lượng tồn kho cho từng sản phẩm tương ứng trong bảng Products.
Khấu trừ lại số điểm thưởng đã tặng cho khách hàng trong bảng Customers.
Xóa các dòng trong bảng OrderDetails, sau đó xóa hóa đơn trong bảng Orders.
Nếu có lỗi, gọi Rollback(); nếu hoàn tất, gọi Commit().
Hướng dẫn mã nguồn gợi ý cho sinh viên tự luyện
Thêm Action sau vào tệp CheckoutController.cs:
// DELETE: api/checkout/cancel/{orderId}
[HttpDelete("cancel/{orderId}")]
public IActionResult CancelOrder(int orderId)
{
    string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            // 1. Kiem tra hoa don va lay thong tin khach hang, diem thuong
            string getOrderSql = "SELECT CustomerId, RewardPoints FROM Orders WHERE OrderId = @OrderId";
            int customerId = 0;
            int pointsToDeduct = 0;

            using (SqlCommand cmdGet = new SqlCommand(getOrderSql, connection, transaction))
            {
                cmdGet.Parameters.AddWithValue("@OrderId", orderId);
                using (SqlDataReader reader = cmdGet.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException($"Không tìm thấy hóa đơn ID = {orderId} để hủy!");
                    }
                    customerId = Convert.ToInt32(reader["CustomerId"]);
                    pointsToDeduct = Convert.ToInt32(reader["RewardPoints"]);
                }
            }

            // 2. Lay danh sach san pham da mua de hoan tra kho
            var itemsToRestore = new List<(int ProductId, int Quantity)>();
            string getDetailsSql = "SELECT ProductId, Quantity FROM OrderDetails WHERE OrderId = @OrderId";

            using (SqlCommand cmdDetails = new SqlCommand(getDetailsSql, connection, transaction))
            {
                cmdDetails.Parameters.AddWithValue("@OrderId", orderId);
                using (SqlDataReader reader = cmdDetails.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itemsToRestore.Add((
                            Convert.ToInt32(reader["ProductId"]), 
                            Convert.ToInt32(reader["Quantity"])
                        ));
                    }
                }
            }

            // 3. Hoan tra ton kho trong bang Products
            foreach (var item in itemsToRestore)
            {
                string restoreStockSql = @"
                    UPDATE Products 
                    SET StockQuantity = StockQuantity + @Qty 
                    WHERE ProductId = @ProductId";

                using (SqlCommand cmdRestore = new SqlCommand(restoreStockSql, connection, transaction))
                {
                    cmdRestore.Parameters.AddWithValue("@Qty", item.Quantity);
                    cmdRestore.Parameters.AddWithValue("@ProductId", item.ProductId);
                    cmdRestore.ExecuteNonQuery();
                }
            }

            // 4. Khau tru diem thuong da tich cua khach hang
            if (pointsToDeduct > 0)
            {
                string deductPointsSql = @"
                    UPDATE Customers 
                    SET RewardPoints = CASE 
                        WHEN RewardPoints >= @Pts THEN RewardPoints - @Pts 
                        ELSE 0 
                    END 
                    WHERE CustomerId = @CustomerId";

                using (SqlCommand cmdDeduct = new SqlCommand(deductPointsSql, connection, transaction))
                {
                    cmdDeduct.Parameters.AddWithValue("@Pts", pointsToDeduct);
                    cmdDeduct.Parameters.AddWithValue("@CustomerId", customerId);
                    cmdDeduct.ExecuteNonQuery();
                }
            }

            // 5. Xoa chi tiet hoa don truoc (tranh vi pham rang buoc khoa ngoai)
            string deleteDetailsSql = "DELETE FROM OrderDetails WHERE OrderId = @OrderId";
            using (SqlCommand cmdDelDetails = new SqlCommand(deleteDetailsSql, connection, transaction))
            {
                cmdDelDetails.Parameters.AddWithValue("@OrderId", orderId);
                cmdDelDetails.ExecuteNonQuery();
            }

            // 6. Xoa hoa don tong trong bang Orders
            string deleteOrderSql = "DELETE FROM Orders WHERE OrderId = @OrderId";
            using (SqlCommand cmdDelOrder = new SqlCommand(deleteOrderSql, connection, transaction))
            {
                cmdDelOrder.Parameters.AddWithValue("@OrderId", orderId);
                cmdDelOrder.ExecuteNonQuery();
            }

            // 7. Xac nhan luu vinh vien (Commit)
            transaction.Commit();

            return Ok(new
            {
                success = true,
                message = $"Đã hủy thành công hóa đơn #{orderId} và hoàn trả toàn bộ tồn kho vào CSDL!"
            });
        }
        catch (Exception ex)
        {
            // Hoan tac sach se khi co loi
            transaction.Rollback();
            return BadRequest(new
            {
                success = false,
                message = "Hủy hóa đơn thất bại, giao dịch đã được Rollback!",
                errorDetail = ex.Message
            });
        }
    }
}


6. Tổng kết đánh giá kết quả thực hành Buổi 5 (Rubric Checklist)
Sinh viên tự đối soát toàn bộ sản phẩm thực hành Buổi 5 theo bảng kiểm tra sau:

STT
Hạng mục kiểm tra
Yêu cầu đạt chuẩn
Đạt
1
Thành phần ADO.NET (Tiết 1)


Viết đúng khối using, sử dụng SqlParameter chống SQL Injection, hiểu bản chất ExecuteScalar, ExecuteNonQuery, ExecuteReader.
[ ]
2
Tra cứu Barcode (Tiết 2)


Gọi GET /api/ado-products/barcode/{barcode} trả về kết quả nhanh dưới 100ms bằng SqlDataReader.
[ ]
3
Stored Procedure (Tiết 3)


Tạo thành công sp_GetRevenueByCashier trên SSMS, gọi API Reports xuất báo cáo doanh thu đủ 5 nhân viên thu ngân.
[ ]
4
Chuẩn ACID (Tiết 4)


Trình bày được ý nghĩa 4 tiêu chí ACID và giải thích được cơ chế hoạt động của BeginTransaction, Commit, Rollback.
[ ]
5
API Checkout (Tiết 5)


Gọi POST /api/checkout thực hiện đồng thời tạo Order, thêm OrderDetails, trừ tồn kho và tích điểm.
[ ]
6
Kiểm chứng Rollback (Tiết 5)


Đặt hàng vượt quá tồn kho --> API ném lỗi và tự động Rollback, không để lại bất kỳ dữ liệu rác nào trong database.
[ ]
7
Bài tập mở rộng (Tiết 5)


Hoàn thành API Hủy đơn hàng và hoàn trả tồn kho tự động bằng SqlTransaction.
[ ]



