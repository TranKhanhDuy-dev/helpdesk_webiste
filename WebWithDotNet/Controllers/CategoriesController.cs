using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebWithDotNet.Data;
using WebWithDotNet.Models;
using WebWithDotNet.Resources.Message;

namespace WebWithDotNet.Controllers;

public class CategoriesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IStringLocalizer _localizer;

    public CategoriesController(ApplicationDbContext context, IStringLocalizerFactory localizerFactory)
    {
        _context = context;
        _localizer = localizerFactory.Create(
            "Message.MessageResource",
            "WebWithDotNet"
        );
    }

    #region Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    name = "Failed",
                    message = _localizer["createfailed", "category"].Value,
                });
            }
            category.CreateAt = DateTime.Now;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                result = "Succeeded",
                message = _localizer["createsucceed", "Category"].Value,
            });
        }
        catch
        {
            return BadRequest(new
            {
                success = false,
                name = "Failed",
                message = _localizer["createfailed", "category"].Value,
            });
        }
    }
    #endregion

    #region Read
    public async Task<IActionResult> Index()
    {
        var categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        return View(categories);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }
    #endregion
    
    #region Update
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Category category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }
        
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        category.UpdateAt = DateTime.Now;
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    #endregion

    #region Delete
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    #endregion
}