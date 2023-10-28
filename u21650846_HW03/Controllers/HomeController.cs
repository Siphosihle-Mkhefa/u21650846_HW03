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
        public ActionResult Index()
        {
            var viewmodel = new CombinedViewModel()
            {
                students= db.students.ToList(),
                books= db.books.Include(a => a.authors).Include(t => t.types).ToList(),   
            };
            return View(viewmodel);
        }

       
    }
}