using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpndRr.Models.Spends
{
	public class Spend
	{
		public int Id { get; set; }

		[Required, Display(Name = "Сумма")]
		public decimal Sum { get; set; }

		[Required, Display(Name = "Тип")]
		[EnumDataType(typeof(SpendType))]
		public SpendType Type { get; set; }

		[Required, Display(Name = "Подтип")]
		public int SubType { get; set; }

		[Required, Display(Name = "Дата создания")]
		public DateTime Date { get; set; }

		[Required, Display(Name = "Комментарий")]
		public string Comment { get; set; }
	}
}
