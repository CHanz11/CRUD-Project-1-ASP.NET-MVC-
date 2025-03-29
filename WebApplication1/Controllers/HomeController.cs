using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Linq;
using System;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        //Using Dependency Injection to inject an instance of AppDbContext into the HomeController
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        //LIST: Display all record
        public IActionResult List()
        {
            var student = _context.Student.ToList();
            return View(student);
        }

        //CREATE: Display form
        public IActionResult Create()
        {
            return View();
        }

        //CREATE: Save new record
        [HttpPost]
        public IActionResult Create(Students student)
        {
            if(ModelState.IsValid)
            {
                _context.Student.Add(student);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View(student);
        }

        // EDIT: Display existing record in form
        public IActionResult Edit(int id)
        {
            var student = _context.Student.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // EDIT: Save updated record
        [HttpPost]
        public IActionResult Edit(Students student)
        {
            if (ModelState.IsValid)
            {
                _context.Student.Update(student);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View(student);
        }

        // DELETE: Confirm delete
        public IActionResult Delete(int id)
        {
            var student = _context.Student.Find(id);
            if (student== null)
            {
                return NotFound();
            }
            return View(student);
        }

        // DELETE: Remove from database
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var person = _context.Student.Find(id);
            if (person != null)
            {
                _context.Student.Remove(person);
                _context.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }

}
