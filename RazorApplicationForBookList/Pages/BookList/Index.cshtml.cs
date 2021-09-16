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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }
        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int Id)
        {
            var BookFromDb = await _db.Book.FindAsync(Id);
            if(BookFromDb == null)
            {
                return NotFound();
            }
            _db.Book.Remove(BookFromDb);
            await _db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
