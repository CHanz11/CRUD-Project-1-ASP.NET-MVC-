using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using X.PagedList;
using X.PagedList.Extensions;
using X.PagedList.Mvc.Core;

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
        public IActionResult List(string searchString, int? page)
        {
            int pageSize = 10;  // Number of records per page
            int pageNumber = page ?? 1; // Default page number is 1

            // Fetch student records
            var students = _context.Student.AsQueryable();

            // If search string is not empty, filter results
            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s =>
                    s.Name.Contains(searchString) ||
                    s.LastName.Contains(searchString) ||
                    s.Grade.ToString().Contains(searchString));
            }

            // Apply pagination
            var paginatedStudents = students.OrderBy(s => s.Id).ToPagedList(pageNumber, pageSize);

            // Pass search term to the view for display
            ViewBag.SearchString = searchString;

            return View(paginatedStudents);
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
            try
            {
                if (ModelState.IsValid)
                {
                    //Calculate letter grade and GPA
                    var result = GradeCalculator.CalculateLetterGrade(student.Grade);
                    student.LetterGrade = result.LetterGrade;
                    student.GPA = result.GPA;

                    _context.Student.Add(student);
                    _context.SaveChanges();
                    return RedirectToAction("List");
                }
                return View(student);
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", "An error occurred while saving: " + ex.Message);
                return View(student);
            }
            
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
                //Calculate letter grade and GPA
                var result = GradeCalculator.CalculateLetterGrade(student.Grade);
                student.LetterGrade = result.LetterGrade;
                student.GPA = result.GPA;


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
