using OpenIdConnectDemo.Api.Dtos;
using OpenIdConnectDemo.Api.Models;
using OpenIdConnectDemo.Api.Repositories;

namespace OpenIdConnectDemo.Api.Services;

public class BookService(BookRepository repository)
{
    private readonly BookRepository repository = repository;

    public async Task<IEnumerable<Book>> GetAllAsync() => await repository.GetAllAsync();

    public async Task<Book?> GetByIdAsync(int id) => await repository.GetByIdAsync(id);

    public async Task AddAsync(BookInputDto bookInput)
    {
        Book book = new()
        {
            Title = bookInput.Title,
            Author = bookInput.Author,
            Genre = bookInput.Genre,
            Year = bookInput.Year
        };

        await  repository.AddAsync(book);
    }

    public async Task UpdateAsync(int id, BookInputDto bookInput)
    {
        Book book = new()
        {
            Id = id,
            Title = bookInput.Title,
            Author = bookInput.Author,
            Genre = bookInput.Genre,
            Year = bookInput.Year
        };

       await repository.UpdateAsync(book);
    }

    public async Task DeleteAsync(int id) => await repository.DeleteAsync(id);
}
