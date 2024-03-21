using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Limit(int id, string description, string type)
    {
        public int Id { get; set; } = id;

        public string Description { get; set; } = description;

        public string Type { get; set; } = type;


    }
}
