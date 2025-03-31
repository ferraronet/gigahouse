using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaHouse.Infrastructure.HandlersScraping.Tasks
{
    public class MessageTask
    {
        public Guid Id { get; set; }

        public string Link { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public Guid ProjectId { get; set; }

        public Guid ProductId { get; set; }
    }
}
