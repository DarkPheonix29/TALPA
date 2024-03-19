using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class User(string name, int id)
    {
        private string name { get; set; } = name;
        public int id {  get; set; } = id;
    }
}
