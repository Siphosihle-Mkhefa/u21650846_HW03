using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace u21650846_HW03.Models
{
    public class CombinedViewModel
    {
        public IEnumerable <authors > authors { get; set; }
        public IEnumerable <books > books { get; set; }
        public IEnumerable <borrows> borrows { get; set; }
        public IEnumerable <students > students { get; set; }
        public IEnumerable < types > types { get; set; }
        public int TotalItems { get;private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

       


    }
}