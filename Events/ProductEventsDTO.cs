using System;
using SharedItems.Shared;

namespace Events
{
	public class ProductEventsDTO: BaseEntityDTOWithouthId
    {

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal CurrentPrice { get; set; }

        public string SKU { get; set; }

        public bool WasCreated { get; set; }


    }
}

