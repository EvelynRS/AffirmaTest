using System;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public abstract class ModelBase
    {
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }

        public ModelBase()
        {
            this.DateCreated = DateTimeOffset.UtcNow;
            this.DateModified = DateTimeOffset.UtcNow;
        }
    }

    public abstract class ModelBaseWithGuid : ModelBase
    {
        [Key]
        public Guid Id { get; set; }

        public ModelBaseWithGuid()
        {
            this.Id = Guid.NewGuid();
        }

        public ModelBaseWithGuid(Guid guid)
        {
            this.Id = guid;
        }
    }
}
