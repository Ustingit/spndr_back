using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpndRr.Models.Spends
{
	public class Spend
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[Required, Display(Name = "Сумма")]
		[JsonProperty("sum")]
		public decimal Sum { get; set; }

		[JsonIgnore]
		[Required, Display(Name = "Тип")]
		[EnumDataType(typeof(SpendType))]
		public SpendType Type { get; set; }

		[JsonProperty("highType")]
		public string StringType => (Type == SpendType.Income) 
			? "INCOME"
			: "COSTS";

		[JsonProperty("concreteTypeId")]
		[Required, Display(Name = "Подтип")]
		public int SubType { get; set; }

		[JsonProperty("date")]
		[Required, Display(Name = "Дата создания")]
		public DateTime Date { get; set; }

		[JsonProperty("comment")]
		[Required, Display(Name = "Комментарий")]
		public string Comment { get; set; }
	}
}
