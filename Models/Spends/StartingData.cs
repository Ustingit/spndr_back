using Newtonsoft.Json;
using SpndRr.Controllers;

namespace SpndRr.Models.Spends
{
	public class StartingData
	{
		public StartingData(PaginationDto data, IdTextPair[] outcome, IdTextPair[] income)
		{
			Data = data;
			OutcomeSubTypes = outcome;
			IncomeSubTypes = income;
		}

		[JsonProperty("data")]
		public PaginationDto Data { get; set; }

		[JsonProperty("outcomeSubTypes")]
		public IdTextPair[] OutcomeSubTypes { get; set; }

		[JsonProperty("incomeSubTypes")]
		public IdTextPair[] IncomeSubTypes { get; set; }
	}
}
