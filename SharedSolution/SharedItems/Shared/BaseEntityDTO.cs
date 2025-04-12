using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedItems.Shared
{
	public class BaseEntityDTO<T> where T : struct
    {
		public BaseEntityDTO()
		{
            CreatedDate = DateTime.Now;
            Active = true;
        }
        public T Id { get; set; }


        public DateTime CreatedDate { get; set; }

        public long CreatedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool Active { get; set; } = true;

    }

    public class BaseEntityDTOWithouthId
    {
        public BaseEntityDTOWithouthId()
        {
            CreatedDate = DateTime.Now;
            Active = true;
        }


        public DateTime CreatedDate { get; set; }

        public long CreatedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool Active { get; set; } = true;
    }
}

