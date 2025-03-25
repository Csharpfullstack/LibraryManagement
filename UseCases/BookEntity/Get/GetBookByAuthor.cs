using Ardalis.Result;
using Ardalis.SharedKernel;
using Ardalis.Specification;
using Core;

namespace UseCases.BookEntity.Get
{
    class GetBookByAuthor
    {
        public record Query(string Author) : IQuery<Result<BookDTO1>>;

        public class Handler(IRepository<Book> _repository) : IQueryHandler<Query, Result<BookDTO1>>
        {
            public async Task<Result<BookDTO1>> Handle(Query request, CancellationToken cancellationToken)
            {
                var spec = new BookByAuthorSpec(request.Author);
                var returnedBookDTO = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
                if (returnedBookDTO == null) return Result.NotFound();

                return Result.Success(returnedBookDTO);
            }
        }
    }

    public class BookByAuthorSpec : Specification<Book, BookDTO1>
    {
        public BookByAuthorSpec(string author)
        {
            Query
                .AsNoTracking()
                .Where(c => c.Author == author)
                .Select(b => new BookDTO1
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    IsBorrowed = b.IsBorrowed
                });
        }
    }

    public class BookDTO1
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; }
    }
}
