using OpenIdConnectDemo.Client.Dtos;
using OpenIdConnectDemo.Client.Models;
using System.Text;
using System.Text.Json;

namespace OpenIdConnectDemo.Client.Services;

public class BookService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    public BookService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        var httpClient = httpClientFactory.CreateClient("APIClient");

        var request = new HttpRequestMessage(HttpMethod.Get, "/books/");

        var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();

        using (var responseStream = await response.Content.ReadAsStreamAsync())
        {
            var books = await JsonSerializer.DeserializeAsync<IEnumerable<Book>>(responseStream, jsonSerializerOptions);
            return books ?? [];
        }
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        var httpClient = httpClientFactory.CreateClient("APIClient");

        var request = new HttpRequestMessage(HttpMethod.Get, $"/books/{id}");

        var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();

        using (var responseStream = await response.Content.ReadAsStreamAsync())
        {
            return await JsonSerializer.DeserializeAsync<Book>(responseStream, jsonSerializerOptions);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var httpClient = httpClientFactory.CreateClient("APIClient");

        var request = new HttpRequestMessage(HttpMethod.Delete, $"/books/{id}");

        var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();
    }

    public async Task CreateAsync(BookForCreationDto book)
    {
        var httpClient = httpClientFactory.CreateClient("APIClient");

        var json = JsonSerializer.Serialize(book);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, $"/books/")
        {
            Content = data
        };

        var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(int id, BookForUpdateDto book)
    {
        var httpClient = httpClientFactory.CreateClient("APIClient");

        var json =  JsonSerializer.Serialize(book);
      
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Put, $"/books/{id}")
        {
            Content = data
        };

        var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();
    }
}
