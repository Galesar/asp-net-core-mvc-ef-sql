using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using MVC_lib.Models;

namespace MVC_lib.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;

        public int count_of_books(Book book)
        {
            Book[] books = db.Books.ToArray();
            int count_of_books_of_user = 0;
            for (int i = 0; i < books.Count(); i++)
            {
                if (books[i].UserID == book.UserID)
                {
                    count_of_books_of_user++;
                }
            }
            return count_of_books_of_user;
        }
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
            if (count_of_books(book) < 4)
            {
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else return Redirect($"ErrUserHaveBooksOverLimit?id={book.UserID}");

        }

        public async Task<IActionResult> EditBook(int? id)
        {
            if (id != null)
            {
                Book book = db.Books.Find(id);
                return View(book);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditBook(Book book)
        {
            if (count_of_books(book) < 4)
            {
                db.UpdateRange(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return Redirect($"ErrUserHaveBooksOverLimit?id={book.UserID}");
            }
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

        public async Task<IActionResult> ErrUserHaveBooksOverLimit(int? id)
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

        public async Task<IActionResult> DetailsBook(int? id)
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

    }
}
