using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDNetCore6.Shared;

public class User
{
    public int Id { get; set; }
    [Required(ErrorMessage = "* Obligatory Field")]
    public string Name { get; set; } = null!;
	[Required(ErrorMessage = "* Obligatory Field")]
	public string Lastname { get; set; } = null!;
	[Required(ErrorMessage = "* Obligatory Field")]
	public string Email { get; set; } = null!;
	[Required(ErrorMessage = "* Obligatory Field")]
	public string Phone { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? LeavingDate { get; set; }
}
