using Ardalis.SharedKernel;

namespace Core
{
    public class Book: EntityBase, IAggregateRoot
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public bool IsBorrowed { get; set; } = false;
    }
}
