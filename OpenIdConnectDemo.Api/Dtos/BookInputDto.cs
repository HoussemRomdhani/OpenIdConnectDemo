namespace OpenIdConnectDemo.Api.Dtos;

public class BookInputDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int Year { get; set; }
}
