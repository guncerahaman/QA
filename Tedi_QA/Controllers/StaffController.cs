using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tedi_QA.Data;
using Tedi_QA.Data.Entities;

namespace Tedi_QA.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {

        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Questions()
        {
            var applicationDbContext = _context.Questions.Where(q => q.isAnswered == false);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Question(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
               .FirstOrDefaultAsync(m => m.Qid == id);
            
            if (question.isRead == false)
            {
                question.isRead = true;
                _context.Update(question);
                await _context.SaveChangesAsync();
            }
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }
        public async Task<IActionResult> Answered()
        {
            var applicationDbContext = _context.Questions.Where(q=> q.isAnswered ==true);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Answers/Create
        public IActionResult Answer(int? id)
        {
            if (id != null)
            {
                ViewData["id"] = _context.Questions.Where(a => a.Qid == id).FirstOrDefault().email;
                ViewData["msg"] = _context.Questions.Where(a => a.Qid == id).FirstOrDefault().message;
                ViewData["qid"] = _context.Questions.Where(a => a.Qid == id).FirstOrDefault().Qid;
            }
            else
            {
                return RedirectToAction("Questions");
            }
     
            return View();
        }
        
        public async Task<IActionResult> Answers()
        {
            var applicationDbContext = _context.Answers.Include(a => a.question);
            return View(await applicationDbContext.ToListAsync());
        }
        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Answer(Answer ans)
        {
            if (ModelState.IsValid)
            {
                var question = await _context.Questions
          .FirstOrDefaultAsync(m => m.Qid == ans.Qid);
                  question.isAnswered = true;
                    _context.Update(question);
                   ans.answerDate = DateTime.Now;
                _context.Add(ans);
                await _context.SaveChangesAsync();
                return RedirectToAction("Questions");
            }
           
            return View(ans);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> EditAnswer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            ViewData["qid"] = _context.Questions.Where(a => a.Qid == id).FirstOrDefault().Qid;

            ViewData["id"] = new SelectList(_context.Questions, "Qid", "email", answer.id);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAnswer(int id,  Answer ans)
        {
            if (id != ans.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var question = await _context.Questions
        .FirstOrDefaultAsync(m => m.Qid == ans.Qid);
                    question.isAnswered = true;
                    ans.answerDate = DateTime.Now;
                    _context.Update(ans);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(ans.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Answers");
            }
            ViewData["id"] = new SelectList(_context.Questions, "Qid", "email", ans.id);
            return View(ans);
        }
        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Questions
                .FirstOrDefaultAsync(m => m.Qid == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction("Questions");
        }

        // GET: Answers/Delete/5

        private bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.id == id);
        }
    }
}
    