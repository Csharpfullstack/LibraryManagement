using Ardalis.Result;
using Ardalis.SharedKernel;
using Ardalis.Specification;
using Core;

namespace UseCases.BookEntity.List
{
    class GetAllBooks
    {
        public record Query() : IQuery<Result<List<BookDTO2>>>;

        public class Handler(IRepository<Book> _repository) : IQueryHandler<Query, Result<List<BookDTO2>>>
        {
            public async Task<Result<List<BookDTO2>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var spec = new AllBooksSpec();
                var returnedBooksDTO = await _repository.ListAsync(spec, cancellationToken);
                if (returnedBooksDTO == null || !returnedBooksDTO.Any()) return Result.NotFound();

                return Result.Success(returnedBooksDTO);
            }
        }
    }

    public class AllBooksSpec : Specification<Book, BookDTO2>
    {
        public AllBooksSpec()
        {
            Query
                .AsNoTracking()
                .Select(b => new BookDTO2
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    IsBorrowed = b.IsBorrowed
                });
        }
    }

    public class BookDTO2
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; }
    }
}
