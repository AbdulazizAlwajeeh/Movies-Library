using Humanizer;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MyMVCApp.Data;
using MyMVCApp.Models;
using NuGet.ContentModel;
using NuGet.DependencyResolver;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics;

namespace MyMVCApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MyDbContext _context;
        public MoviesController(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.ToListAsync();
            return View(movies);
        }
        //public async Task<IActionResult> Details(Movie? obj, int id)
        //{
        //    obj = await _context.Movies.FindAsync(id);
        //    return obj == null ? NotFound() : View(obj);
        //}
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie obj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");      
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var obj = await _context.Movies.FindAsync(id);
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Movie obj)
        {
            if (ModelState.IsValid)
        {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
            return View(obj);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Movie? obj, int id)
        {
            obj = await _context.Movies.FindAsync(id);
            if (obj != null)
        {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
        }
            return RedirectToAction("Index");
        }
    }
}
