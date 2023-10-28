using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21650846_HW03.Models
{
    public class CombinedViewModel
    {
        public IEnumerable <authors > authors { get; set; }
        public IEnumerable <books > books { get; set; }
        public IEnumerable <borrows> borrows { get; set; }
        public IEnumerable <students > students { get; set; }
        public IEnumerable < types > types { get; set; }
    }
}