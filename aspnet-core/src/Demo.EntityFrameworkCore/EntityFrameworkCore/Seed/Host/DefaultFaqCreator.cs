using System.Linq;
using Microsoft.EntityFrameworkCore;
using Demo.Faqs;

namespace Demo.EntityFrameworkCore.Seed.Host;

public class DefaultFaqCreator
{
    private readonly DemoDbContext _context;

    public DefaultFaqCreator(DemoDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        if (!_context.FrequentlyAskedQuestions.IgnoreQueryFilters().Any())
        {
            _context.FrequentlyAskedQuestions.AddRange(
                new FrequentlyAskedQuestion
                {
                    Question = "Làm thế nào để đổi mật khẩu tài khoản?",
                    Answer = "Bạn có thể vào mục hồ sơ cá nhân trên góc phải màn hình, chọn Đổi mật khẩu, nhập mật khẩu cũ và mật khẩu mới để lưu lại.",
                    Status = FaqStatus.Published,
                    SortOrder = 1
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Tôi quên mật khẩu thì phải làm sao?",
                    Answer = "Vui lòng nhấn vào đường dẫn 'Quên mật khẩu' ở màn hình đăng nhập, hệ thống sẽ gửi hướng dẫn khôi phục qua email của bạn.",
                    Status = FaqStatus.Published,
                    SortOrder = 2
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Hệ thống có phiên bản ứng dụng di động (Mobile App) không?",
                    Answer = "Hiện tại hệ thống hoạt động hoàn toàn trên trình duyệt web, nhưng giao diện đã được tối ưu hoá tự động để hiển thị rất tốt trên cả điện thoại di động và máy tính bảng.",
                    Status = FaqStatus.Published,
                    SortOrder = 3
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Dữ liệu của tôi có được bảo mật không?",
                    Answer = "Tuyệt đối an toàn. Chúng tôi áp dụng các tiêu chuẩn mã hoá dữ liệu hiện đại và cơ chế phân quyền bảo mật chặt chẽ nhất từ hệ thống.",
                    Status = FaqStatus.Hidden,
                    SortOrder = 4
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Làm sao để liên hệ với bộ phận hỗ trợ kỹ thuật?",
                    Answer = "Bạn có thể gửi yêu cầu hỗ trợ qua email support@example.com hoặc gọi tới số hotline 1900-xxxx trong giờ hành chính từ thứ Hai đến thứ Sáu.",
                    Status = FaqStatus.Published,
                    SortOrder = 5
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Hệ thống hỗ trợ những trình duyệt web nào?",
                    Answer = "Hệ thống tương thích tốt nhất với Google Chrome, Microsoft Edge, Mozilla Firefox và Safari phiên bản mới nhất.",
                    Status = FaqStatus.Published,
                    SortOrder = 6
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Tôi có thể xuất dữ liệu ra file Excel hoặc PDF không?",
                    Answer = "Có, tại hầu hết các danh sách dữ liệu, hệ thống cung cấp nút 'Xuất Excel' hoặc 'In báo cáo' để bạn tải dữ liệu về máy.",
                    Status = FaqStatus.Published,
                    SortOrder = 7
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Quy định đặt mật khẩu an toàn như thế nào?",
                    Answer = "Mật khẩu an toàn phải có độ dài tối thiểu 8 ký tự, bao gồm ít nhất một chữ hoa, một chữ thường, một chữ số và một ký tự đặc biệt.",
                    Status = FaqStatus.Published,
                    SortOrder = 8
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Làm cách nào để thêm một người dùng mới vào hệ thống?",
                    Answer = "Quản trị viên có thể truy cập mục 'Quản lý người dùng', nhấn nút 'Thêm mới', điền đầy đủ thông tin tài khoản và gán vai trò phù hợp.",
                    Status = FaqStatus.Published,
                    SortOrder = 9
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Tại sao tài khoản của tôi bị tạm khóa?",
                    Answer = "Tài khoản sẽ bị tạm khóa tự động nếu nhập sai mật khẩu quá 5 lần liên tiếp nhằm ngăn chặn truy cập trái phép. Vui lòng liên hệ Admin để mở khóa.",
                    Status = FaqStatus.Hidden,
                    SortOrder = 10
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Tôi có thể thay đổi địa chỉ email đăng ký không?",
                    Answer = "Bạn có thể cập nhật email cá nhân trong phần 'Thông tin tài khoản'. Một email xác thực sẽ được gửi đến hộp thư mới để hoàn tất.",
                    Status = FaqStatus.Published,
                    SortOrder = 11
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Tính năng thông báo trên hệ thống hoạt động như thế nào?",
                    Answer = "Hệ thống sẽ gửi thông báo theo thời gian thực (Real-time) qua chuông thông báo trên thanh điều hướng khi có tác vụ mới hoặc cập nhật trạng thái.",
                    Status = FaqStatus.Published,
                    SortOrder = 12
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Làm sao để cấu hình phân quyền cho từng vai trò (Role)?",
                    Answer = "Vào mục 'Vai trò', chọn vai trò cần chỉnh sửa và nhấn 'Phân quyền' (Permissions). Bạn có thể tích chọn các quyền tương ứng với từng chức năng.",
                    Status = FaqStatus.Published,
                    SortOrder = 13
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Hệ thống có tự động sao lưu (Backup) dữ liệu không?",
                    Answer = "Có, dữ liệu cơ sở dữ liệu được sao lưu định kỳ hàng ngày vào lúc 00:00 và lưu trữ tại máy chủ an toàn trong vòng 30 ngày.",
                    Status = FaqStatus.Published,
                    SortOrder = 14
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Phiên đăng nhập (Session) duy trì trong bao lâu?",
                    Answer = "Mặc định phiên làm việc duy trì trong 60 phút không có hoạt động. Nếu bạn chọn 'Ghi nhớ đăng nhập', phiên có thể kéo dài tối đa 30 ngày.",
                    Status = FaqStatus.Published,
                    SortOrder = 15
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Hệ thống có hỗ trợ xác thực hai bước (2FA) không?",
                    Answer = "Có, bạn có thể bật tính năng xác thực hai yếu tố (2FA) thông qua Google Authenticator hoặc ứng dụng xác thực trong phần Cài đặt bảo mật.",
                    Status = FaqStatus.Hidden,
                    SortOrder = 16
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Làm thế nào để tìm kiếm thông tin nhanh trong danh sách?",
                    Answer = "Bạn có thể sử dụng thanh tìm kiếm ở đầu bảng dữ liệu, nhập từ khóa liên quan và nhấn Enter hoặc biểu tượng kính lúp.",
                    Status = FaqStatus.Published,
                    SortOrder = 17
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Dung lượng tệp đính kèm tối đa được phép tải lên là bao nhiêu?",
                    Answer = "Hệ thống cho phép tải lên các tệp đính kèm với dung lượng tối đa là 10MB cho mỗi tệp. Các định dạng được hỗ trợ gồm PDF, DOCX, XLSX, PNG và JPG.",
                    Status = FaqStatus.Published,
                    SortOrder = 18
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Tôi có thể xem lịch sử thao tác (Audit Log) của người dùng ở đâu?",
                    Answer = "Chỉ người dùng có quyền quản trị mới có thể truy cập mục 'Nhật ký kiểm toán' (Audit Logs) để xem chi tiết thời gian, người thực hiện và nội dung thay đổi.",
                    Status = FaqStatus.Published,
                    SortOrder = 19
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Làm sao để cấu hình gửi email tự động từ hệ thống?",
                    Answer = "Quản trị viên có thể vào phần 'Cài đặt hệ thống' -> 'Cấu hình SMTP' để điền thông tin máy chủ mail, cổng kết nối, tài khoản và mật khẩu ứng dụng.",
                    Status = FaqStatus.Published,
                    SortOrder = 20
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Chính sách hủy tài khoản hoặc xóa dữ liệu diễn ra như thế nào?",
                    Answer = "Khi xóa tài khoản, hệ thống áp dụng cơ chế Soft Delete (xóa mềm). Dữ liệu vẫn được lưu trữ tạm thời trong cơ sở dữ liệu và chỉ Admin cấp cao mới có thể khôi phục.",
                    Status = FaqStatus.Hidden,
                    SortOrder = 21
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Hệ thống có hỗ trợ đa ngôn ngữ không?",
                    Answer = "Có, hệ thống hỗ trợ Tiếng Việt và Tiếng Anh. Bạn có thể chuyển đổi ngôn ngữ nhanh chóng từ thanh menu góc trên màn hình.",
                    Status = FaqStatus.Published,
                    SortOrder = 22
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Giao diện tối (Dark Mode) có được hỗ trợ không?",
                    Answer = "Tính năng giao diện tối hiện đang trong giai đoạn thử nghiệm (Beta) và dự kiến sẽ được phát hành chính thức trong bản cập nhật quý tới.",
                    Status = FaqStatus.Hidden,
                    SortOrder = 23
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Làm thế nào để lọc danh sách câu hỏi theo trạng thái?",
                    Answer = "Trên trang quản lý câu hỏi thường gặp (FAQ), bạn có thể sử dụng bộ lọc trạng thái để chọn xem tất cả, chỉ hiển thị (Published) hoặc chỉ câu hỏi ẩn (Hidden).",
                    Status = FaqStatus.Published,
                    SortOrder = 24
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Làm sao để sắp xếp thứ tự hiển thị của các câu hỏi?",
                    Answer = "Mỗi câu hỏi có thuộc tính 'Thứ tự hiển thị' (Sort Order). Câu hỏi có số thứ tự nhỏ hơn sẽ được ưu tiên hiển thị trước trên trang người dùng.",
                    Status = FaqStatus.Published,
                    SortOrder = 25
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Có giới hạn số lượng câu hỏi FAQ được tạo không?",
                    Answer = "Không có giới hạn về số lượng câu hỏi FAQ. Hệ thống hỗ trợ phân trang tự động để đảm bảo tốc độ tải trang luôn mượt mà.",
                    Status = FaqStatus.Published,
                    SortOrder = 26
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Làm thế nào khi gặp lỗi 500 hoặc sự cố hệ thống?",
                    Answer = "Khi gặp lỗi hệ thống, vui lòng chụp ảnh màn hình thông báo lỗi và gửi cho đội ngũ kỹ thuật cùng với thời điểm xảy ra sự cố để được xử lý nhanh nhất.",
                    Status = FaqStatus.Published,
                    SortOrder = 27
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Tôi có thể tùy chỉnh logo và tiêu đề hệ thống không?",
                    Answer = "Có, bạn vào mục 'Cài đặt giao diện' để tải lên logo công ty, favicon và thay đổi tiêu đề hiển thị ở đầu trang.",
                    Status = FaqStatus.Published,
                    SortOrder = 28
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Dữ liệu có bị mất khi làm mới (Refresh) trang không?",
                    Answer = "Các biểu mẫu đang nhập dở có thể bị mất nếu bạn tải lại trang trước khi bấm 'Lưu'. Hãy đảm bảo lưu lại thông tin trước khi chuyển trang hoặc tải lại.",
                    Status = FaqStatus.Published,
                    SortOrder = 29
                },
                new FrequentlyAskedQuestion
                {
                    Question = "Làm sao để cập nhật phiên bản mới của phần mềm?",
                    Answer = "Hệ thống sẽ hiển thị biểu ngữ thông báo khi có bản cập nhật mới. Quá trình nâng cấp thường được thực hiện tự động vào ban đêm để tránh gián đoạn sử dụng.",
                    Status = FaqStatus.Published,
                    SortOrder = 30
                }
            );

            _context.SaveChanges();
        }
    }
}
