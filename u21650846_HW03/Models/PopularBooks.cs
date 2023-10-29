using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21650846_HW03.Models
{
    public class PopularBooks
    {
        public int? BookId { get; set; }
        public string BookName { get; set; }
        public int? PageCount { get; set; }
        public int? Point { get; set; }
        public int AuthorId { get; set; }
        public int TypeId { get; set; }
        public int? BorrowCount { get; set; }
    }
}