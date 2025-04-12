using System;
using System.ComponentModel.DataAnnotations;
using SharedItems.Shared;

namespace SharedLibrary.DomainLayer.ModelDTO
{
	public class ProductDTO:BaseEntityDTOWithouthId
	{
		public ProductDTO()
		{
		}

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal CurrentPrice { get; set; }

        public string SKU { get; set; }


    }
    public class ProductResponse : BaseEntityDTOWithouthId
    {
        public ProductResponse()
        {
        }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal? ConvertedPrice { get; set; }


        public string SKU { get; set; }

        //public long CurrentQuantity { get; set; }

    }

    public class ProductUpdateDTO : BaseEntityDTO<long>
    {
        public ProductUpdateDTO()
        {
        }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal CurrentPrice { get; set; }

        public string SKU { get; set; }

       // public long CurrentQuantity { get; set; }

    }
}

