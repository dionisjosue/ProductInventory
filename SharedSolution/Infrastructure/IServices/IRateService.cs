using System;

namespace Infrastructure.IServices
{
	public interface IRateService
	{


		Task<decimal> GetCurrencyValueAsync(string moneda);

		decimal GetPriceConverted(decimal rate, decimal price);
	}
}

