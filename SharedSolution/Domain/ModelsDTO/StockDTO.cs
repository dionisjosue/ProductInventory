using System;
using System.ComponentModel.DataAnnotations.Schema;
using SharedItems.Shared;

namespace SharedLibrary.DomainLayer.ModelDTO
{
	public class StockDTO:BaseEntityDTO<long>
	{
		public StockDTO()
		{
		}

        public long ProductFk { get; set; }

        public long Quantity { get; set; }

        [ForeignKey("ProductFk")]
        public ProductDTO Product { get; set; }

    }

    public class StockUpdateDTO : BaseEntityDTOWithouthId
    {
        public StockUpdateDTO()
        {
        }

        public long ProductFk { get; set; }

        public long Quantity { get; set; }


    }
}

