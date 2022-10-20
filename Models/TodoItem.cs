using System;
using System.Collections.Generic;

namespace CRUDtest.Models
{
    public partial class TodoItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsComplete { get; set; }
    }
}
