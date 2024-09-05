using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab1.Controllers
{
    public class StudentController : Controller
    {
        private List<Student> Students = new List<Student>();

        public StudentController()
        {
            Students = new List<Student>()
            {
                new Student()
                {
                    Id = 101,
                    Name = "Hải Nam",
                    Branch = Branch.IT,
                    Gender = Gender.Male,
                    IsRegular = true,
                    Address = "A1-2018",
                    Email = "nam@g.com"
                },
                new Student()
                {
                    Id = 102,
                    Name = "Minh Tú",
                    Branch = Branch.BE,
                    Gender = Gender.Female,
                    IsRegular = true,
                    Address = "A1-2019",
                    Email = "tu@g.com"
                },
                new Student()
                {
                    Id = 103,
                    Name = "Hoàng Phong",
                    Branch = Branch.CE,
                    Gender = Gender.Male,
                    IsRegular = false,
                    Address = "A1-2020",
                    Email = "phong@g.com"
                },
                new Student()
                {
                    Id = 104,
                    Name = "Xuân Mai",
                    Branch = Branch.EE,
                    Gender = Gender.Female,
                    IsRegular = false,
                    Address = "A1-2021",
                    Email = "mai@g.com"
                },
            };
        }

        [Route("Admin/Student/List")]
        public IActionResult Index()
        {
            return View(Students);
        }

        [Route("Admin/Student/Add")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "IT", Value = "1" },
                new SelectListItem(){ Text = "BE", Value = "2" },
                new SelectListItem(){ Text = "CE", Value = "3" },
                new SelectListItem(){ Text = "EE", Value = "4" },
            };

            return View("Create/Index");
        }

        [HttpPost]
        public IActionResult Create(Student student, IFormFile ProfilePicture)
        {
            if (ModelState.IsValid)
            {
                if (ProfilePicture != null && ProfilePicture.Length > 0)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/students");

                    // Ensure the directory exists
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var fileName = Path.GetFileName(ProfilePicture.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ProfilePicture.CopyTo(stream);
                    }

                    student.ProfilePicturePath = "/images/students/" + fileName;
                }

                student.Id = Students.Last().Id + 1;

                Students.Add(student);

                return View("Index", Students);
            }

            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "IT", Value = "1" },
                new SelectListItem(){ Text = "BE", Value = "2" },
                new SelectListItem(){ Text = "CE", Value = "3" },
                new SelectListItem(){ Text = "EE", Value = "4" },
            };

            return View("Create/Index");
        }
    }
}
