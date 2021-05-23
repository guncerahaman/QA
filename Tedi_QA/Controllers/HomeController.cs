using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tedi_QA.Data;
using Tedi_QA.Data.Entities;
using Tedi_QA.Models;

namespace Tedi_QA.Controllers
{
    public class HomeController : Controller
    {


        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string searchString)
        {
            var applicationDbContext = _context.Answers.Where(a => a.isPublished == true).Include(a => a.question);

            if (!string.IsNullOrEmpty(searchString))
            {
                applicationDbContext = _context.Answers.Where(a => a.isPublished == true).Where(a => a.question.message.Contains(searchString)).Include(a => a.question);

            }
           
            return View(await applicationDbContext.ToListAsync());
        }

      [HttpGet("message")]
        public IActionResult Message()
        {
            return View();
        }
        [HttpPost("message")]
        public async Task<IActionResult> Create(Question question)
        {
            if (ModelState.IsValid)
            {
                question.questionDate = DateTime.Now;
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(question);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
