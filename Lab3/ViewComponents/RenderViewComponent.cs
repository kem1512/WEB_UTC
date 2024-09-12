using Lab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.ViewComponents
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
                    Id = 1, Name = "Branche", Link = "Branches/List"
                },
                new MenuItem()
                {
                    Id = 2, Name = "Student", Link = "Students/List"
                },
                new MenuItem()
                {
                    Id = 3, Name = "Subject", Link = "Subjects/List"
                },
                new MenuItem()
                {
                    Id = 4, Name = "Course", Link = "Courses/List"
                },
            };
        }

        public IViewComponentResult Invoke()
        {
            return View("RenderLeftMenu", MenuItems);
        }
    }
}
