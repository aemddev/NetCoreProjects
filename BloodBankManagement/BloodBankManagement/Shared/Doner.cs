using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankManagement.Shared
{
	public class Doner
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "* Obligatory Field")]
		public string Name { get; set; }
		[Required(ErrorMessage = "* Obligatory Field")]
		public string Lastname { get; set; }

		[Required(ErrorMessage = "* Obligatory Field")]
		public string Phone { get; set; }
		[Required(ErrorMessage = "* Obligatory Field")]
		public string BloodType { get; set; }

	}
}
