using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class DetailsController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public IActionResult Index( string name)
        {
            var details = _context.Details.ToList();
            ViewBag.name = name;
           
            return View(details);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string title, string description, DateTime date)
        {
            _context.Details.Add(new()
            {
                Title = title,
                Description = description,
                Date = date
            });
            _context.SaveChanges();
            TempData["success"] = "Add New Title successfuly";
            return RedirectToAction("Index");
            
        }
        public IActionResult Edit(int userId)
        {
            var user = _context.Details.Find(userId);
            if (user != null) return View(user);
            return RedirectToAction("NotFoundPage");
        }
        [HttpPost]
        public IActionResult Edit(int userId, string title, string description, DateTime date)
        {
            var user = _context.Details.Update(new()
            {
                Id = userId,
                Title = title,
                Description = description,
                Date = date
            });
            _context.SaveChanges();
            TempData["success"] = "Update Title successfuly";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int userId)
        {
            var user = _context.Details.Find(userId);
            if (user != null)
            {
                _context.Details.Remove(user);
                _context.SaveChanges();
                TempData["success"] = "Delete Title successfuly";
                return RedirectToAction("Index");
            }
            return RedirectToAction("NotFoundPage");
        }
        public IActionResult NotFoundPage()
        {
            return View();
        }


        }
}