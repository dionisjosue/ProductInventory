using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.SharedItems.Shared
{
    public class BaseEntity<T> where T : struct
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
            Active = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }


        public DateTime CreatedDate { get; set; }

        public long CreatedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool Active { get; set; } = true;

        public bool? Deleted { get; set; }
    }
}

