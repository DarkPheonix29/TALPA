namespace BLL
{
    internal class User(string name, int id)
    {
        private string name { get; set; } = name;
        public int id {  get; set; } = id;
    }
}
