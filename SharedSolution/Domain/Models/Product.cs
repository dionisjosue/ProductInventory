using System;
using System.ComponentModel.DataAnnotations;
using SharedLibrary.SharedItems.Shared;

namespace SharedLibrary.Domain.Models
{
	public class Product:BaseEntity<long>
	{
		public Product()
		{
            CurrentQuantity = 0;
		}

		[StringLength(150)]
		public string Name { get; set; }

        [StringLength(150)]
        public string Category { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(500)]
        public decimal CurrentPrice { get; set; }

        [StringLength(50)]
        public string SKU { get; set; }

        public long CurrentQuantity { get; set; } = 0;

        public List<Stock> Stocks { get; set; }

 	}
}

