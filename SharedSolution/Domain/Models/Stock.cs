using System;
using System.ComponentModel.DataAnnotations.Schema;
using SharedLibrary.SharedItems.Shared;

namespace SharedLibrary.Domain.Models
{
	public class Stock:BaseEntity<long>
	{
		public Stock()
		{
		}

		public long ProductFk { get; set; }

		public long Quantity { get; set; }

		[ForeignKey("ProductFk")]
		public Product Product { get; set; }

		public string Description { get; set; }


	}
}

