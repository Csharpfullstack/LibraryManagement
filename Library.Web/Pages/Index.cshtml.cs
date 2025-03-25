using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UseCases.BookEntity.List;

namespace Library.Web.Pages
{
    public class BooksModel : PageModel
    {
        private readonly IMediator _mediator;
       

        public List<BookDTO2> Books { get; private set; } = new List<BookDTO2>();

        public BooksModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync()
        {
            var resultBooks = await _mediator.Send(new GetAllBooks.Query());
            if (resultBooks.IsSuccess)
            {
                Books = resultBooks.Value;
            }
        }
    }
}
