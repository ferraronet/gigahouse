using GigaHouse.Data.Common;

namespace GigaHouse.Data.Domain
{
    public class UserProduct : BaseEntity
    {
        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public Guid? TaskId { get; set; }

        public Product Product { get; set; } = new Product();

        public User User { get; set; } = new User();

        public Task? Task { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? FinishedAt { get; set;}
    }
}
