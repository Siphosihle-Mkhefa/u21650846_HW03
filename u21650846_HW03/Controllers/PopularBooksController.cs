using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using Newtonsoft.Json;
using GemBox.Document;
using u21650846_HW03.Models;
using System.IO;
using System.Xml.Linq;
using SaveOptions = GemBox.Document.SaveOptions;
using LoadOptions = GemBox.Document.LoadOptions;

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

        [HttpPost]
        public FileResult SAVE(PopularBooks model)
        {
            // If using Professional version, put your serial key below.
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var templateFile = Server.MapPath("~/App_Data/DocumentTemplate.docx");

            // Load template document.
            var document = DocumentModel.Load(templateFile);

            // Insert content from HTML editor.
            var bookmark = document.Bookmarks["HtmlBookmark"];
            bookmark.GetContent(true).LoadText(model.FileModel.Content, LoadOptions.HtmlDefault);

            // Save document to stream in specified format.
            var saveOptions = GetSaveOptions(model.FileModel.Extension);
            var stream = new MemoryStream();
            document.Save(stream, saveOptions);

            // Download document.
            var downloadDirectory = Server.MapPath("~/Documents/");
            var downloadFile = $"{model.FileModel.FileName}{model.FileModel.Extension}";
            var fullPath = Path.Combine(downloadDirectory, downloadFile);
            System.IO.File.WriteAllBytes(fullPath, stream.ToArray());

            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Documents/"));
            List<FileModel> files = new List<FileModel>();
            foreach (string filePath in filePaths)
            {
                files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
            }
            return File(fullPath, saveOptions.ContentType, downloadFile);
        }


        private static SaveOptions GetSaveOptions(string extension)
        {
            switch (extension)
            {
                case ".docx": return SaveOptions.DocxDefault;
                case ".pdf": return SaveOptions.PdfDefault;
                case ".xps": return SaveOptions.XpsDefault;
                case ".html": return SaveOptions.HtmlDefault;
                case ".mhtml": return new HtmlSaveOptions() { HtmlType = HtmlType.Mhtml };
                case ".rtf": return SaveOptions.RtfDefault;
                case ".xml": return SaveOptions.XmlDefault;
                case ".png": return SaveOptions.ImageDefault;
                case ".jpeg": return new ImageSaveOptions(ImageSaveFormat.Jpeg);
                case ".gif": return new ImageSaveOptions(ImageSaveFormat.Gif);
                case ".bmp": return new ImageSaveOptions(ImageSaveFormat.Bmp);
                case ".tiff": return new ImageSaveOptions(ImageSaveFormat.Tiff);
                case ".wmp": return new ImageSaveOptions(ImageSaveFormat.Wmp);
                default: return SaveOptions.TxtDefault;
            }
        }

    }
}