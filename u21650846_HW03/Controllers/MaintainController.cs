using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using u21650846_HW03.Models;

namespace u21650846_HW03.Controllers
{
    public class MaintainController : Controller
    {
        // GET: Maintain
        private LibraryEntities db = new LibraryEntities();
        public async Task< ActionResult> Maintain()
        {
            var viewmodel = new CombinedViewModel()
            {
                authors= await db.authors.ToListAsync(),
                types=await db.types.ToListAsync(),
                borrows=await db.borrows.Include(b => b.books).Include(b => b.students).ToListAsync(),

            };
            return View();
        }
    }
}