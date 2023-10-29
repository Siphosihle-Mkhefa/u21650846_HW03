using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace u21650846_HW03.Models
{
    public class CombinedViewModel
    {
        public IPagedList <authors > authors { get; set; }
        public IPagedList <books > books { get; set; }
        public  IPagedList<borrows> borrows { get; set; }
        public  IPagedList<students > students { get; set; }
        public IPagedList < types > types { get; set; }
       
        public int PageCount { get;private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }


        


    }
}