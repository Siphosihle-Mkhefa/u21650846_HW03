using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21650846_HW03.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;

namespace u21650846_HW03.Controllers
{
    public class HomeController : Controller
    {
        private LibraryEntities db = new LibraryEntities();
        public async Task <ActionResult> Index(int? page, int? bookPage, int? studentPage)
        {
            int pageSize = 10; 

            
            int studentPageNumber = studentPage ?? 1;
            var students = await db.students.OrderBy(s => s.studentId).ToListAsync();
            var pagedStudent = students.ToPagedList((int)studentPageNumber, pageSize);

          
            int bookPageNumber = bookPage ?? 1;
            var books = await db.books.OrderBy(b => b.bookId).ToListAsync();
            var pagedBook = books.ToPagedList((int)bookPageNumber, pageSize);
            
            var viewModel = new CombinedViewModel
            {
                students = pagedStudent,
                books = pagedBook
               
            };

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "studentId,name,surname,birthdate,gender,class,point")] students students)
        {
            if (ModelState.IsValid)
            {
                db.students.Add(students);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(students);
        }

    }
}