using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Holamundo.DataAccess;
using Holamundo.Models.DataModels;
using Holamundo.Services;

namespace Holamundo.Controllers
{
    public class StudientsController : Controller
    {
        private readonly UniversityContext _context;
        private readonly IStudientService _studientService;

        public StudientsController(UniversityContext context, IStudientService studientService )
        {
            _context = context;
            _studientService = studientService;
        }

        // GET: Studients
        public async Task<IActionResult> Index()
        {
              return _context.Studients != null ? 
                          View(await _context.Studients.ToListAsync()) :
                          Problem("Entity set 'UniversityContext.Studients'  is null.");
        }

        //GET api Studients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Studient>>> GetStudients()
        {
            return await _context.Studients.ToListAsync();
        }

        //GET api Studients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Studient>> GetStudient(int id)
        {
            var studient = await _context.Studients.FindAsync(id);

            _studientService.GetStientsWithNoCourses();

            if (studient == null)
            {
                return NotFound;

            }

            return studient;

        }

        // GET: Studients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Studients == null)
            {
                return NotFound();
            }

            var studient = await _context.Studients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studient == null)
            {
                return NotFound();
            }

            return View(studient);
        }

        // GET: Studients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Dob,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,DeletedBy,DeletedAt,IsDeleted")] Studient studient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studient);
        }

        // GET: Studients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Studients == null)
            {
                return NotFound();
            }

            var studient = await _context.Studients.FindAsync(id);
            if (studient == null)
            {
                return NotFound();
            }
            return View(studient);
        }

        // POST: Studients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Dob,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,DeletedBy,DeletedAt,IsDeleted")] Studient studient)
        {
            if (id != studient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudientExists(studient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(studient);
        }

        // GET: Studients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Studients == null)
            {
                return NotFound();
            }

            var studient = await _context.Studients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studient == null)
            {
                return NotFound();
            }

            return View(studient);
        }

        // POST: Studients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Studients == null)
            {
                return Problem("Entity set 'UniversityContext.Studients'  is null.");
            }
            var studient = await _context.Studients.FindAsync(id);
            if (studient != null)
            {
                _context.Studients.Remove(studient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudientExists(int id)
        {
          return (_context.Studients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
