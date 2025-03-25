﻿using Ardalis.SharedKernel;

namespace Core
{
    public class BorrowingRecord : EntityBase, IAggregateRoot
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        public DateTime BorrowDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }
    }
}
