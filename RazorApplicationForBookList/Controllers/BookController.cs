using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorApplicationForBookList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RazorApplicationForBookList.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(x => x.Id == Id);
            if(bookFromDb == null)
            {
                return Json(new { Success = false, message = "Error While Deleteing" });
            }
            _db.Book.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { Success = true, message ="Delete Successfull"});
        }
    }
}
