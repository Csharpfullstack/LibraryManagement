using Ardalis.Result;
using Ardalis.SharedKernel;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.BookEntity.Create
{
    class CreateBook
    {
        public record CreateBookDtoCommand
           (
                  string Title ,
                  string Author 
           ) : ICommand<Result<int>>;

        public class CreateBookHandler(IRepository<Book> _repository) : ICommandHandler<CreateBookDtoCommand, Result<int>>
        {

            public async Task<Result<int>> Handle(CreateBookDtoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var bookEntity = new Book
                    {
                        Title = request.Title,
                        Author = request.Author,
                        IsBorrowed = false
                    };

                    var createdBook = await _repository.AddAsync(bookEntity);

                    if(createdBook == null) return Result<int>.NotFound();
                    return createdBook.Id;
                }
                catch (Exception ex)
                {
                    return Result.Error($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
