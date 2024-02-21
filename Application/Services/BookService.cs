﻿using Application.DTOs;
using Application.Extensions;
using Application.Interfaces;
using Data.Entities;
using Data.Interfaces;
using static Application.Enums.BookAttributeEnum;

namespace Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookEntity>> GetBooks(BookAttribute attribute, string? value)
    {
        return attribute switch
        {
            BookAttribute.Id => await _bookRepository.GetBooksById(value),
            BookAttribute.Author => await _bookRepository.GetBooksByAuthor(value),
            BookAttribute.Title => await _bookRepository.GetBooksByTitle(value),
            BookAttribute.Genre => await _bookRepository.GetBooksByGenre(value),
            BookAttribute.Description => await _bookRepository.GetBooksByDescription(value),
            BookAttribute.Price => await _bookRepository.GetBooksByPrice(value),
            BookAttribute.PublishDate => await _bookRepository.GetBooksByPublishDate(value),
            _ => await _bookRepository.GetBooks(),
        };
    }

    public async Task<BookEntity?> UpdateBook(string id, BookDto bookDto)
    {
        var bookEntity = ToEntityModelExtension.FromBookDto(bookDto, id);

        return await _bookRepository.UpdateBook(bookEntity);
    }

    public async Task<BookEntity> CreateBook(BookDto bookDto)
    {
        var bookEntity = ToEntityModelExtension.FromBookDto(bookDto);

        return await _bookRepository.AddBook(bookEntity);
    }
}