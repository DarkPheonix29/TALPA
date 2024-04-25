using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public string Email { get; set; }
		public string Team { get; set; }
		public string Role { get; set; }
		public int Points { get; set; }
    }
}
