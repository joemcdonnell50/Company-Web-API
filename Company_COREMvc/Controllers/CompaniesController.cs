using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company_COREMvc.Models;
using Microsoft.AspNetCore.Authorization;


namespace Company_COREMvc.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly Company_COREMvcContext _context;

        public CompaniesController(Company_COREMvcContext context)
        {
            _context = context;
        }

        // GET: Companies/SearchParamaters
        public async Task<IActionResult> Index(String searchString)
        {
            var companies = from c in _context.Company
                            select c; 

            if (!String.IsNullOrEmpty(searchString))
            {
                companies = companies.Where(s => s.ISIN.Equals(searchString));
            }

            return View(await companies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();  
            }

            var company = await _context.Company
                .FirstOrDefaultAsync(m => m.ID == id);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Exchange,Ticker,ISIN,Website")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }
        
        // GET: Remote validation for ISIN
        [HttpGet]
        [AllowAnonymous]
        public IActionResult VerifyISIN(string isin)
        {
            var _isin = from i in _context.Company
                          where i.ISIN == isin
                          select i.ISIN.FirstOrDefault();

            if (_isin.Any())
            {
                return Json($"ISIN {isin} is already in use.");
            }

            return Json(true);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Exchange,Ticker,ISIN,Website")] Company company)
        {
            if (id != company.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.ID))
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
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .FirstOrDefaultAsync(m => m.ID == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Company.FindAsync(id);
            _context.Company.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Company.Any(e => e.ID == id);
        }
    }
}
