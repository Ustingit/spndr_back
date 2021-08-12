using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SpndRr.Data;
using SpndRr.DataSources;
using SpndRr.Models.Common;
using SpndRr.Models.Spends;

namespace SpndRr.Controllers
{
    public class SpendController : Controller
    {
        private const int FirstPage = 1; // obviously
        private readonly ApplicationDbContext _context;
        private SubTypeDataSource subTypeSource;

        public SpendController(ApplicationDbContext context)
        {
            _context = context;
            subTypeSource = new SubTypeDataSource(context);
        }

        #region outer API

        // GET: Spend/GetItems?page=1&countOnPage=10
        [EnableCors("LocalApi")]
        public async Task<string> GetItems(int page = 1, int countOnPage = 10)
        {
            var dto = await GetEntities(page, countOnPage);

            return JsonConvert.SerializeObject(dto);
        }

        // GET: Spend/GetItems?page=1
        [EnableCors("LocalApi")]
        public async Task<string> GetStartingData(int page = 1, int countOnPage = 10)
        {
	        var dto = await GetEntities(page, countOnPage);

	        var outcomeSubTypes = subTypeSource.GetOutcomes();
	        var incomeSubTypes = subTypeSource.GetIncomes();

	        var startingData = new StartingData(dto, outcomeSubTypes, incomeSubTypes);

            return JsonConvert.SerializeObject(startingData);
        }

        private async Task<PaginationDto> GetEntities(int page, int countOnPage)
        {
	        var entries = await _context.Spend.Skip((page - 1) * countOnPage).Take(countOnPage)
		        .OrderByDescending(x => x.Date)
		        .ToListAsync();
	        var count = await _context.Spend.CountAsync();

	        var totalPages = (int)Math.Ceiling(count / (float)countOnPage);

	        var lastPage = totalPages;
	        var prevPage = page > FirstPage ? page - 1 : FirstPage;
	        var nextPage = page < lastPage ? page + 1 : lastPage;

	        return new PaginationDto(entries, count, prevPage, nextPage);
        }

        [EnableCors("LocalApi")]
        [HttpPost]
        public async Task<string> AddSpend()
        {
	        try
	        {
		        using StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);
		        var body = await reader.ReadToEndAsync();
                
		        var newSpend = JsonConvert.DeserializeObject<NewSpend>(body, new JsonSerializerSettings
		        {
			        NullValueHandling = NullValueHandling.Ignore
		        });

		        if (newSpend != null)
		        {
			        var spend = new Spend()
			        {
				        Date = DateTime.Now,
				        Sum = newSpend.Sum,
				        Type = newSpend.GetRealType(),
				        SubType = newSpend.SubType,
				        Comment = newSpend.Comment
                };

                    await _context.AddAsync(spend);
			        await _context.SaveChangesAsync();

			        var result = new { id = spend.Id };

			        return JsonConvert.SerializeObject(ApiResponse.Sucessful(result));
		        }
	        }
	        catch
	        {
		        return JsonConvert.SerializeObject(ApiResponse.NotSucessful());
            }

	        return JsonConvert.SerializeObject(ApiResponse.NotSucessful());
        }

        // GET: Spend/DeleteSpend?id=1
        [EnableCors("LocalApi")]
        [HttpDelete]
        public async Task<string> DeleteSpend(int? id)
        {
	        if (id == null)
	        {
		        return JsonConvert.SerializeObject(ApiResponse.NotSucessful());
	        }

	        var spend = await _context.Spend
		        .FirstOrDefaultAsync(m => m.Id == id);
	        
	        if (spend != null)
	        {
		        _context.Spend.Remove(spend);
		        await _context.SaveChangesAsync();
                return JsonConvert.SerializeObject(ApiResponse.Sucessful());
            }

	        return JsonConvert.SerializeObject(ApiResponse.NotSucessful());
        }

        #endregion

        // GET: Spend
        public async Task<IActionResult> Index()
        {
            return View(await _context.Spend.ToListAsync());
        }

        // GET: Spend/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spend = await _context.Spend
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spend == null)
            {
                return NotFound();
            }

            return View(spend);
        }

        // GET: Spend/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spend/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sum,Type,SubType,Date,Comment")] Spend spend)
        {
	        spend.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(spend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spend);
        }

        // GET: Spend/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spend = await _context.Spend.FindAsync(id);
            if (spend == null)
            {
                return NotFound();
            }
            return View(spend);
        }

        // POST: Spend/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sum,Type,SubType,Date,Comment")] Spend spend)
        {
            if (id != spend.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spend);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpendExists(spend.Id))
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
            return View(spend);
        }

        // GET: Spend/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spend = await _context.Spend
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spend == null)
            {
                return NotFound();
            }

            return View(spend);
        }

        // POST: Spend/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spend = await _context.Spend.FindAsync(id);
            _context.Spend.Remove(spend);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpendExists(int id)
        {
            return _context.Spend.Any(e => e.Id == id);
        }
    }
}
