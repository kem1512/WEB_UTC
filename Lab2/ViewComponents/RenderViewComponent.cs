using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.ViewComponents
{
    public class RenderViewComponent : ViewComponent
    {
        private readonly List<MenuItem> MenuItems;

        public RenderViewComponent()
        {
            MenuItems = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = 1, Name = "Branches", Link = "Branches/List",
                },
                new MenuItem()
                {
                    Id = 2, Name = "Student", Link = "Students/List",
                },
                new MenuItem()
                {
                    Id = 3, Name = "Subjects", Link = "Subjects/List",
                },
                new MenuItem()
                {
                    Id = 4, Name = "Courses", Link = "Courses/List",
                },
            };
        }

        public IViewComponentResult Invoke()
        {
            return View("RenderLeftMenu", MenuItems);
        }
    }
}
