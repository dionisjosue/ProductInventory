using System;
using System.ComponentModel.DataAnnotations.Schema;
using SharedLibrary.SharedItems.Shared;


namespace SharedLibrary.Domain.Models
{
	public class ProductPrices:BaseEntity<long>
	{
		public ProductPrices()
		{
		}

		public long ProductFk { get; set; }

		public decimal Price { get; set; }

		[ForeignKey("ProductFk")]
		public Product Product { get; set; }
	}
}

