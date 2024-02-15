using Application.Interfaces;
using Application.Services;
using Data.Interfaces;
using Data.Repositories;

namespace Api
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBookServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, BookRepository>();
        }
    }
}
