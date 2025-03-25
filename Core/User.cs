using Ardalis.SharedKernel;

namespace Core
{
    public class User : EntityBase, IAggregateRoot
    {
        public string Name { get; set; } = string.Empty;
    }
}
