using Microsoft.Data.Sqlite;
using OpenIdConnectDemo.Api.Models;

namespace OpenIdConnectDemo.Api.Repositories;

public class DatabaseInitializer(BookRepository repository)
{
    private readonly BookRepository repository = repository;

    public async Task InitializeAsync()
    {
        await TryCreateDatabaseAsync();
        await SeedDataAsync();
    }

    private async Task TryCreateDatabaseAsync()
    {
        using (var connection = new SqliteConnection(repository.ConnectionString))
        {
            connection.Open();
            var createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Books (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Author TEXT NOT NULL,
                    Genre TEXT NOT NULL,
                    Year INTEGER NOT NULL
                );";
            using (var command = new SqliteCommand(createTableQuery, connection))
            {
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    private async Task SeedDataAsync()
    {
        foreach (var book in Books)
        {
            await repository.TryAddAsync(book);
        }
    }

    private static IEnumerable<Book> Books =>
    [
            new() { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Year = 1960 },
            new() { Title = "1984", Author = "George Orwell", Genre = "Dystopian", Year = 1949 },
            new() { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Fiction", Year = 1925 },
            new() { Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", Year = 1813 },
            new() { Title = "The Catcher in the Rye", Author = "J.D. Salinger", Genre = "Fiction", Year = 1951 },
            new() { Title = "The Hobbit", Author = "J.R.R. Tolkien", Genre = "Fantasy", Year = 1937 },
            new() { Title = "Fahrenheit 451", Author = "Ray Bradbury", Genre = "Dystopian", Year = 1953 },
            new() { Title = "Harry Potter and the Sorcerer's Stone", Author = "J.K. Rowling", Genre = "Fantasy", Year = 1997 },
            new() { Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Genre = "Fantasy", Year = 1954 },
            new() { Title = "Animal Farm", Author = "George Orwell", Genre = "Political Satire", Year = 1945 },
            new() { Title = "Moby-Dick", Author = "Herman Melville", Genre = "Adventure", Year = 1851 },
            new() { Title = "Jane Eyre", Author = "Charlotte Brontë", Genre = "Romance", Year = 1847 },
            new() { Title = "The Chronicles of Narnia", Author = "C.S. Lewis", Genre = "Fantasy", Year = 1950 },
            new() { Title = "Wuthering Heights", Author = "Emily Brontë", Genre = "Romance", Year = 1847 },
            new() { Title = "The Alchemist", Author = "Paulo Coelho", Genre = "Philosophical Fiction", Year = 1988 },
            new() { Title = "The Kite Runner", Author = "Khaled Hosseini", Genre = "Fiction", Year = 2003 },
            new() { Title = "Brave New World", Author = "Aldous Huxley", Genre = "Dystopian", Year = 1932 },
            new() { Title = "Of Mice and Men", Author = "John Steinbeck", Genre = "Fiction", Year = 1937 },
            new() { Title = "Crime and Punishment", Author = "Fyodor Dostoevsky", Genre = "Psychological Fiction", Year = 1866 },
            new() { Title = "The Picture of Dorian Gray", Author = "Oscar Wilde", Genre = "Philosophical Fiction", Year = 1890 },
            new() { Title = "Dracula", Author = "Bram Stoker", Genre = "Horror", Year = 1897 },
            new() { Title = "Les Misérables", Author = "Victor Hugo", Genre = "Historical Fiction", Year = 1862 },
            new() { Title = "The Catch-22", Author = "Joseph Heller", Genre = "Satire", Year = 1961 },
            new() { Title = "A Tale of Two Cities", Author = "Charles Dickens", Genre = "Historical Fiction", Year = 1859 },
            new() { Title = "The Road", Author = "Cormac McCarthy", Genre = "Post-Apocalyptic", Year = 2006 },
            new() { Title = "The Book Thief", Author = "Markus Zusak", Genre = "Historical Fiction", Year = 2005 },
            new() { Title = "Slaughterhouse-Five", Author = "Kurt Vonnegut", Genre = "Science Fiction", Year = 1969 },
            new() { Title = "The Giver", Author = "Lois Lowry", Genre = "Dystopian", Year = 1993 },
            new() { Title = "The Hunger Games", Author = "Suzanne Collins", Genre = "Dystopian", Year = 2008 },
            new() { Title = "Dune", Author = "Frank Herbert", Genre = "Science Fiction", Year = 1965 },
            new() { Title = "The Shining", Author = "Stephen King", Genre = "Horror", Year = 1977 },
            new() { Title = "It", Author = "Stephen King", Genre = "Horror", Year = 1986 },
            new() { Title = "The Fault in Our Stars", Author = "John Green", Genre = "Young Adult", Year = 2012 },
            new() { Title = "Percy Jackson & The Olympians: The Lightning Thief", Author = "Rick Riordan", Genre = "Fantasy", Year = 2005 },
            new() { Title = "A Game of Thrones", Author = "George R.R. Martin", Genre = "Fantasy", Year = 1996 },
            new() { Title = "The Girl with the Dragon Tattoo", Author = "Stieg Larsson", Genre = "Mystery", Year = 2005 },
            new() { Title = "Gone Girl", Author = "Gillian Flynn", Genre = "Thriller", Year = 2012 },
            new() { Title = "The Da Vinci Code", Author = "Dan Brown", Genre = "Thriller", Year = 2003 },
            new() { Title = "Memoirs of a Geisha", Author = "Arthur Golden", Genre = "Historical Fiction", Year = 1997 },
            new() { Title = "Life of Pi", Author = "Yann Martel", Genre = "Adventure", Year = 2001 },
            new() { Title = "Shantaram", Author = "Gregory David Roberts", Genre = "Adventure", Year = 2003 },
            new() { Title = "The Secret Life of Bees", Author = "Sue Monk Kidd", Genre = "Fiction", Year = 2001 },
            new() { Title = "The Night Circus", Author = "Erin Morgenstern", Genre = "Fantasy", Year = 2011 },
            new() { Title = "Big Little Lies", Author = "Liane Moriarty", Genre = "Fiction", Year = 2014 },
            new() { Title = "The Handmaid's Tale", Author = "Margaret Atwood", Genre = "Dystopian", Year = 1985 },
            new() { Title = "The House of the Spirits", Author = "Isabel Allende", Genre = "Magical Realism", Year = 1982 },
            new() { Title = "Norwegian Wood", Author = "Haruki Murakami", Genre = "Romance", Year = 1987 },
            new() { Title = "The Shadow of the Wind", Author = "Carlos Ruiz Zafón", Genre = "Mystery", Year = 2001 }
        ];
}