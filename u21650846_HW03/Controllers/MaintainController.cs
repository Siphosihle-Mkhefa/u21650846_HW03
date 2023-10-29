using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using u21650846_HW03.Models;
using PagedList;

namespace u21650846_HW03.Controllers
{
    public class MaintainController : Controller
    {
        // GET: Maintain
        private LibraryEntities db = new LibraryEntities();
        public async Task<ActionResult> Maintain(int? borrowpage, int? authorPage, int? typePage)
        {
            int pageSize = 10;

            
            int authorPageNumber = authorPage ?? 1;
            var author = await db.authors.OrderBy(a => a.authorId).ToListAsync();
            var pagedAuthor = author.ToPagedList((int)authorPageNumber, pageSize);

            int typePageNumber = typePage ?? 1;
            var type = await db.types.OrderBy(a => a.typeId).ToListAsync();
            var pagedtype = type.ToPagedList((int)typePageNumber, pageSize);

           
            int borrowPageNumber = borrowpage ?? 1;
            var borrow = await db.borrows.OrderBy(b => b.studentId).ToListAsync();
            var pagedBorrow = borrow.ToPagedList((int)borrowPageNumber, pageSize);

            var viewModel = new CombinedViewModel
            {
               authors= pagedAuthor,
               types= pagedtype,    
               borrows= pagedBorrow,

                
            };

            return View(viewModel);
        }
    }
}