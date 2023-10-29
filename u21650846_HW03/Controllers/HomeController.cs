using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21650846_HW03.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
namespace u21650846_HW03.Controllers
{
    public class HomeController : Controller
    {
        private LibraryEntities db = new LibraryEntities();
        public async Task <ActionResult> Index()
        {
            var viewmodel = new CombinedViewModel()
            {
                students= await db.students.ToListAsync(),
                books= await db.books.Include(a => a.authors).Include(t => t.types).ToListAsync(),   
                borrows= await db.borrows.ToListAsync(),
            };
            return View(viewmodel);
        }

       
    }
}