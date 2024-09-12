﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;

namespace Lab4.Controllers
{
    public class LearnerController : Controller
    {
        private SchoolContext db;           
        public LearnerController(SchoolContext context)
        {
            db = context;
        }
        //khai báo biến toàn cụ pageSize
        private int pageSize = 3;
        public IActionResult Index(int? mid)
        {
            var learners = (IQueryable<Learner>)db.Learners
                    .Include(m => m.Major);
            if (mid != null)
            {
                learners = (IQueryable<Learner>)db.Learners
                    .Where(l => l.MajorId == mid)
                    .Include(m => m.Major);
            }
            //tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            //trả số trang về view để hiển thị nav-trang
            ViewBag.pageNum = pageNum;
            //lấy dữ liệu trang đầu
            var result = learners.Take(pageSize).ToList();           
            return View(result);
        }
        
        public IActionResult LearnerFilter(int? mid, int? keyword, int? pageIndex)
        {
            //lấy toàn bộ learners trong dbset chuyển về IQuerrable<Learner> để dùng Lingq
            var learners = (IQueryable<Learner>) db.Learners;
            //lấy chỉ số trang, nếu chỉ số trang null thì gán ngầm định bằng 1
            int page = (int) (pageIndex == null || pageIndex <= 0 ? 1 : pageIndex);
            //nếu có mid thì lọc learner theo mid (chuyên ngành)
            if (mid != null)
            {
                learners = learners.Where(l => l.MajorId == mid); //lọc
                ViewBag.mid = mid; //gửi mid về view để ghi lại trên nav-trang
            }
            //nếu có keyword thì tìm kiếm theo tên
            if (keyword != null)
            {
                learners = learners.Where(l => l.EnrollmentDate.Year==2022); //tìm kiếm
                ViewBag.keyword = keyword; //gửi keyword về view để ghi lại trên nav-trang
            }
            //tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);            
            ViewBag.pageNum = pageNum; //gửi số trang về view để hiển thị nav-trang
            ViewBag.page = pageIndex;
            //chọn dữ liệu trong trang hiện tại
            var result = learners.Skip(pageSize * (page - 1))
                            .Take(pageSize)
                            .Include(m => m.Major);
            return PartialView("LearnerTable", result);
        }
        public IActionResult LearnerByMajorId(int mid)
        {
            var learners = db.Learners
                .Where(l=>l.MajorId == mid)
                .Include(m=>m.Major).ToList();
            return PartialView("LearnerTable", learners);
        }
        
        public IActionResult SearchByName(string keyword)
        {
            var learners = db.Learners
                .Where(l => l.FirstMidName.ToLower()
                            .Contains(keyword.ToLower()))
                .Include(m => m.Major).ToList();
            return PartialView("LearnerTable", learners);
        }


        //thêm 2 action create
        public IActionResult Create()
        {
            //Hai cách tạo SelectList Major hiển thị select-option
            //cách 1
            var majors = new List<SelectListItem>();
            foreach (var item in db.Majors)
            {
                majors.Add(new SelectListItem { Text = item.MajorName, 
                    Value = item.MajorId.ToString() });
            }
            ViewBag.MajorId = majors;
            //cách 2
            ViewBag.MajorId = new SelectList(db.Majors, "MajorId", "MajorName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorId,EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            var majors = new List<SelectListItem>();
            foreach (var item in db.Majors)
            {
                majors.Add(new SelectListItem
                {
                    Text = item.MajorName,
                    Value = item.MajorId.ToString()
                });
            }
            ViewBag.Majors = majors;
            return View();
        }
        public IActionResult Edit(int id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Find(id);
            if (learner == null)
            {
                return NotFound();
            }
            ViewBag.MajorId = new SelectList(db.Majors, "MajorId", "MajorName", learner.MajorId);
            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerId, FirstMidName,LastName,MajorId,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorId = new SelectList(db.Majors, "MajorId", "MajorName", learner.MajorId);
            return View(learner);
        }
        private bool LearnerExists(int id)
        {
            return (db.Learners?.Any(e => e.LearnerId == id)).GetValueOrDefault();
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Include(l => l.Major)
                .Include(e => e.Enrollments)
                .FirstOrDefault(m => m.LearnerId == id);
            if (learner == null)
            {
                return NotFound();
            }
            if(learner.Enrollments.Count()>0)
            {
                return Content("This learner has some enrollments, can't delete!");
            }    
            return View(learner);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (db.Learners == null)
            {
                return Problem("Entity set 'Learners' is null.");
            }
            var learner = db.Learners.Find(id);
            if (learner != null)
            {
                db.Learners.Remove(learner);
            }

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}