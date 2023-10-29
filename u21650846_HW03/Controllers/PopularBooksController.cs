using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using Newtonsoft.Json;

using u21650846_HW03.Models;

namespace u21650846_HW03.Controllers
{
    public class PopularBooksController : Controller
    {
        public PopularBooksController()
        {
            
        }

        private LibraryEntities db = new LibraryEntities();

        public async Task <ActionResult> Reports(DateTime? startDate, DateTime? endDate)
        {
           

            var popularBooks = await db.borrows
            .Where(b => b.takenDate >= startDate && b.takenDate <= endDate)
            .GroupBy(b => b.bookId)
            .Select(group => new PopularBooks
            {
                BookId = group.Key,
                BorrowCount = group.Count()
            })
            .OrderByDescending(b => b.BorrowCount)
            .Take(10)
            .ToListAsync();

            // Populate book details for the popular books
            foreach (var book in popularBooks)
            {
                var bookDetails = await db.books.FindAsync(book.BookId);
                if (bookDetails != null)
                {
                    book.BookName = bookDetails.name;
                    book.PageCount = bookDetails.pagecount;
                    book.Point = bookDetails.point;
                }
            }

            return View(popularBooks);
        }
    }
}