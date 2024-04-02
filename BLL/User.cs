using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;

namespace BLL
{
    public class User(string name, string id)
    {
        public string Name { get; set; } = name;
        public string Id { get; set; } = id;
    }
}
