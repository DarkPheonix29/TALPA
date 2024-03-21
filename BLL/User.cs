using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class User(string name, int id)
    {
        public string Name { get; set; } = name;
        public int Id { get; set; } = id;
    }
}
