using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_lib.Models;

namespace MVC_lib.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = db.Books
                .Include(c => c.User)
                .AsNoTracking();
            return View(await books.ToListAsync());
        }
        
        
        public IActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
        {
            db.Books.Add(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditBook(int? id)
        {
            if (id != null)
            {
                var book = db.Books
                        .Include(c => c.User)
                        .AsNoTracking()
                        .Single(i => id == i.m_ID);
                if (book != null)
                    return View(book);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditBook(Book book)
        {
            db.Books.UpdateRange(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        [ActionName("DeleteBook")]
        public async Task<IActionResult> ConfirmDeleteBook(int? id)
        {
            if (id != null)
            {
                var book = db.Books
                        .Include(c => c.User)
                        .AsNoTracking()
                        .Single(i => id == i.m_ID);
                if (book != null)
                    return View(book);
                }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int? id)
        {
            if (id != null)
            {
                Book book = await db.Books.FirstOrDefaultAsync(p => p.m_ID == id);
                if (book != null)
                {
                    db.Books.Remove(book);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> UsersList()
        {
            return View(await db.Users.ToListAsync());
        }


        public async Task<IActionResult> UserDetails(int? id)
        {
            if (id != null)
            {
                var user = db.Users
                        .Include(c => c.Book)
                        .AsNoTracking()
                        .Single(i => id == i.m_ID);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
    }
}
