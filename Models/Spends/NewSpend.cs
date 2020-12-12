using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpndRr.Models.Spends
{
	public class NewSpend
	{
		[JsonProperty("sum")]
		public decimal Sum { get; set; }

		[JsonProperty("highType")]
		public string Type { get; set; }

		[JsonProperty("concreteTypeId")]
		public int SubType { get; set; }

		[JsonProperty("comment")]
		public string Comment { get; set; }

		public SpendType GetRealType()
		{
			switch (Type)
			{
				case "HIGH_LEVEL_TYPE_INCOME":
					return SpendType.Income;
				case "HIGH_LEVEL_TYPE_COSTS":
					return SpendType.Outcome;
				default:
					throw new ArgumentException("unknown type");
			}
		}
	}
}
