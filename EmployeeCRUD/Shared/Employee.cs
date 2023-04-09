﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Shared
{
	public class Employee
	{
		public int Id { get; set; }
        [Required]
        public string Nama { get; set; } = string.Empty;
        [Required]
        public string Alamat { get; set; } = string.Empty;
        [Required]
        public float Gaji { get; set; }

	}
}
