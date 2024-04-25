
namespace BLL.Models
{
    public class Restriction
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public Restriction(string name, string description, string category)
        {
            Name = name;
            Description = description;
            Category = category;
        }
    }
}
