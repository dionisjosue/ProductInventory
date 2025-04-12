using System;
using SharedLibrary.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using SharedLibrary.DomainLayer.ModelDTO;
using SharedItems.Shared;

namespace DomainLayer.ModelsDTO
{
	public class ProductPricesDTO: BaseEntityDTOWithouthId
    {
		public ProductPricesDTO()
		{
		}

        public long ProductFk { get; set; }

        public decimal Price { get; set; }

        public ProductDTO Product { get; set; }

		public decimal? ConvertedPrice { get; set; }
    }
}

