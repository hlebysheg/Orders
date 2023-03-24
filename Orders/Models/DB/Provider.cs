using System.ComponentModel.DataAnnotations;

namespace Orders.Models.DB
{
    public class Provider
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public string Name { get; set; }

        public Provider(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
