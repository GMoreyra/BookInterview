namespace Api.Contracts.CreateBook;

/// <summary>
/// Represents a request to create a new book.
/// </summary>
/// <param name="Author"> Gets or sets the author of the book. </param>
/// <param name="Description"> Gets or sets the description of the book. </param>
/// <param name="Title"> Gets or sets the title of the book. </param>
/// <param name="Genre"> Gets or sets the genre of the book. </param>
/// <param name="Price"> Gets or sets the price of the book. </param>
/// <param name="PublishDate"> Gets or sets the publish date of the book. </param>
public record CreateBookRequest(string Author,
                                string Description,
                                string Title,
                                string Genre,
                                string Price,
                                string PublishDate) : IBookRequest;
