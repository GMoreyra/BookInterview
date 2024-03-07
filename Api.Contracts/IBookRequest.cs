namespace Api.Contracts
{
    /// <summary>
    /// Represents a book request.
    /// </summary>
    public interface IBookRequest
    {
        string? Author { get; }
        string? Description { get; }
        string? Title { get; }
        string? Genre { get; }
        string? Price { get; }
        string? PublishDate { get; }
    }
}
