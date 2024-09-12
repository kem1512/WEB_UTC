using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Student
    {
        public int Id { get; set; }

        [DisplayName("Tên Sinh Viên")]
        [Required(ErrorMessage = "Tên Sinh Viên Không Thể Bỏ Trống")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email Không Được Bỏ Trống")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string? Email { get; set; }

        [DisplayName("Mật Khẩu")]
        [Required(ErrorMessage = "Mật Khẩu Không Được Bỏ Trống")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật Khẩu Phải Ít Nhất {2} Ký Tự Và Tối Đa {1} Ký Tự")]
        public string? Password { get; set; }

        [DisplayName("Ngành Học")]
        [Required(ErrorMessage = "Ngành Học Không Thể Bỏ Trống")]
        public Branch Branch { get; set; }

        [DisplayName("Giới Tính")]
        [Required(ErrorMessage = "Giới Tính Không Thể Bỏ Trống")]
        public Gender Gender { get; set; }

        [DisplayName("Chính Quy")]
        [Required(ErrorMessage = "Vui Lòng Chọn Loại Hình Học")]
        public bool IsRegular { get; set; }

        [DisplayName("Địa Chỉ")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Địa Chỉ Không Thể Bỏ Trống")]
        public string? Address { get; set; }

        [DisplayName("Ngày Sinh")]
        [Range(typeof(DateTime), "1/1/1963", "12/31/2005", ErrorMessage = "Ngày Sinh Không Hợp Lệ")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày Sinh Không Thể Bỏ Trống")]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Ảnh Đại Diện")]
        public IFormFile? ProfilePicture { get; set; }

        public string? ProfilePicturePath { get; set; }

        [DisplayName("Điểm")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm Phải Từ {1} Đến {2}")]
        [Required(ErrorMessage = "Điểm Không Được Bỏ Trống")]
        public double? Score { get; set; }
    }
}
