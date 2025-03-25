using Ardalis.Result;
using Ardalis.SharedKernel;
using Ardalis.Specification;
using Core;

namespace UseCases.BookEntity.Get
{
    class GetBookByTitle
    {
        public record Query(string Title) : IQuery<Result<BookDTO>>;

        public class Handler(IRepository<Book> _repository)  : IQueryHandler<Query, Result<BookDTO>>
        {
            public async Task<Result<BookDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var spec = new BookByTitleSpec(request.Title);
                var returnedBookDTO = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
                if (returnedBookDTO == null) return Result.NotFound();

                return Result.Success(returnedBookDTO);
            }
        }

    }

    public class BookByTitleSpec : Specification<Book, BookDTO>
    {
        public BookByTitleSpec(string title)
        {
            Query
                .AsNoTracking()
                .Where(c => c.Title == title)
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    IsBorrowed = b.IsBorrowed
                }
                );
        }
    }

    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; }
    }
}
