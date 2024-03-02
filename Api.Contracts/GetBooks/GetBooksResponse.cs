namespace Api.Contracts.GetBooks;

/// <summary>
/// Represents the response for creating a book.
/// </summary>
/// <param name="Id"> Gets or sets the id of the book. </param>
/// <param name="Author"> Gets or sets the author of the book. </param>
/// <param name="Description"> Gets or sets the description of the book. </param>
/// <param name="Title"> Gets or sets the title of the book. </param>
/// <param name="Genre"> Gets or sets the genre of the book. </param>
/// <param name="Price"> Gets or sets the price of the book. </param>
/// <param name="PublishDate"> Gets or sets the publish date of the book. </param>
public record GetBooksResponse(string Id,
                                 string Author,
                                 string Description,
                                 string Title,
                                 string Genre,
                                 double Price,
                                 DateTime PublishDate);
