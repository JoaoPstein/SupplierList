using System;

namespace Supplier.Domain.Entities
{
    public abstract class BaseEntity
    {
        public bool Active { get; set; }
        public Guid Id { get; set; }

        public void Disable()
        {
            Active = false;
        }
    }
}
