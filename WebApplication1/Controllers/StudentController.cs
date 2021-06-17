using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _context;

        public StudentController(StudentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public  IActionResult Index()
        {
            return View( _context.Students.ToList());
        }
         [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Student());
            else
                return View(_context.Students.Find(id));
        }

      
        [HttpPost]
      
        public  IActionResult AddOrEdit( Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Id == 0)
                    _context.Add(student);
                else
                    _context.Update(student);
                 _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }


      
        public  IActionResult Delete(int? id)
        {
            var student =  _context.Students.Find(id);
            _context.Students.Remove(student);
             _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
