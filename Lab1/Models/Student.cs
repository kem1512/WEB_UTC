using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Student
    {
        public int Id { get; set; }

        [DisplayName("Tên Sinh Viên")]
        [Required(ErrorMessage = "Tên không thể bỏ trống")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? Email { get; set; }

        [DisplayName("Mật Khẩu")]
        [Required(ErrorMessage = "Mật khẩu không thể bỏ trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string? Password { get; set; }

        [DisplayName("Ngành Học")]
        [Required(ErrorMessage = "Ngành học không thể bỏ trống")]
        public Branch Branch { get; set; }

        [DisplayName("Giới Tính")]
        [Required(ErrorMessage = "Giới tính không thể bỏ trống")]
        public Gender Gender { get; set; }

        [DisplayName("Chính Quy")]
        [Required(ErrorMessage = "Vui lòng chọn loại hình học")]
        public bool IsRegular { get; set; }

        [DisplayName("Địa Chỉ")]
        public string? Address { get; set; }

        [DisplayName("Ngày Sinh")]
        [Required(ErrorMessage = "Ngày sinh không thể bỏ trống")]
        public DateTime DateOfBirth { get; set; }
        
        //[FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Chỉ chấp nhận các tệp hình ảnh có định dạng .jpg, .jpeg, .png")]
        public IFormFile? ProfilePicture { get; set; }

        public string? ProfilePicturePath { get; set; }
    }
}
