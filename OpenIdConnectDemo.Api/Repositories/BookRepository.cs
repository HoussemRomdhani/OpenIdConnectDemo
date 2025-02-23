using Microsoft.Data.Sqlite;
using OpenIdConnectDemo.Api.Models;

namespace OpenIdConnectDemo.Api.Repositories;

public class BookRepository(string connectionString)
{
    public string ConnectionString { get; } = connectionString;

    public async Task AddAsync(Book book)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var query = @"INSERT INTO Books (Title, Author, Genre, Year) VALUES (@Title, @Author, @Genre, @Year);";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Genre", book.Genre);
                command.Parameters.AddWithValue("@Year", book.Year);
                await  command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task TryAddAsync(Book book)
    {
        Book? retrievedBook = await GetAsync(book.Title, book.Author);

        if (retrievedBook == null)
        {
           await AddAsync(book);
        }
    }

    private async Task<Book?> GetAsync(string title, string author)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var query = @"SELECT Id, Title, Author, Genre, Year FROM Books WHERE Title = @Title AND Author = @Author;";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Author", author);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new Book
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Genre = reader.GetString(3),
                            Year = reader.GetInt32(4)
                        };
                    }
                }
            }
        }

        return null;
    }

    public async Task UpdateAsync(Book book)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var query = @"UPDATE Books SET Title = @Title, Author = @Author, Genre = @Genre, Year = @Year WHERE Id = @Id;";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", book.Id);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Genre", book.Genre);
                command.Parameters.AddWithValue("@Year", book.Year);
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var query = @"DELETE FROM Books WHERE Id = @Id;";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
               await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var query = @"SELECT Id, Title, Author, Genre, Year FROM Books WHERE Id = @Id;";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new Book
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Genre = reader.GetString(3),
                            Year = reader.GetInt32(4)
                        };
                    }
                }
            }
        }

        return null;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        var result = new List<Book>();
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var query = @"SELECT Id, Title, Author, Genre, Year FROM Books;";
            using (var command = new SqliteCommand(query, connection))
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    result.Add(new Book
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Author = reader.GetString(2),
                        Genre = reader.GetString(3),
                        Year = reader.GetInt32(4)
                    });
                }
            }
        }
        return result;
    }
}

