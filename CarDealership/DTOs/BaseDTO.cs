using System;

namespace CarDealership.DTOs
{
    public abstract class BaseDTO
    {
        public Guid Id { get; set; }

        public DateTimeOffset? DateCreated { get; set; }

        public DateTimeOffset? DateModified { get; set; }
    }
}