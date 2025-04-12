using System;
using SharedItems.Shared;

namespace Infrastructure.EventServices
{
	public class ProductEventsDTO: BaseEntityDTO<long>
    {

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal CurrentPrice { get; set; }

        public string SKU { get; set; }

        public bool WasCreated { get; set; }

        public bool WasDeleted { get; set; }


        public Guid EventId { get; set; }

    }
}

