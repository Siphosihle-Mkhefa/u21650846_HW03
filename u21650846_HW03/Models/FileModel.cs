﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace u21650846_HW03.Models
{
    public class FileModel
    {
        [AllowHtml]
        public string Content { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
    }
}