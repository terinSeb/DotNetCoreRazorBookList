using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorApplicationForBookList.Model;

namespace RazorApplicationForBookList.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }
        public async  Task<IActionResult> OnGet(int? Id)
        {
            Book = new Book();
            if(Id == null)
            {
                return Page();
            }
            Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == Id);
            if(Book == null)
            {
                return NotFound();
            }
            return Page();            
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Book.Id == 0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    _db.Book.Update(Book);
                }
                //var BookFromDb = await _db.Book.FindAsync(Book.Id);
                //BookFromDb.ISBN = Book.ISBN;
                //BookFromDb.Author = Book.Author;
                //BookFromDb.Name = Book.Name;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
