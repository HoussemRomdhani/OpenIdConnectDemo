using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIdConnectDemo.Client.Dtos;
using OpenIdConnectDemo.Client.Services;
using OpenIdConnectDemo.Client.ViewModels;
namespace OpenIdConnectDemo.Client.Controllers;

public class BooksController : Controller
{
    private readonly BookService bookService;
    public BooksController(BookService bookService)
    {
        this.bookService = bookService;
    }

    [Authorize]
    public async Task<ActionResult> Index() => View(await bookService.GetAllAsync());
    [Authorize]
    public async Task<ActionResult> Details(int id) => View(await bookService.GetByIdAsync(id));

    [Authorize(Roles = "Admin")]
    public ActionResult Create() => View();
 
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Edit(int id) => View(await bookService.GetByIdAsync(id));
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id) => View(await bookService.GetByIdAsync(id));

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(BookForCreationViewModel vm)
    {
        try
        {
            BookForCreationDto dto = new()
            {
                Title = vm.Title,
                Author = vm.Author,
                Genre = vm.Genre,
                Year = vm.Year
            };
            await bookService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, BookForUpdateViewModel vm)
    {
        try
        {

            BookForUpdateDto dto = new()
            {
                Title = vm.Title,
                Author = vm.Author,
                Genre = vm.Genre,
                Year = vm.Year
            };
            await bookService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeletePost(int id)
    {
        try
        {
            await bookService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
